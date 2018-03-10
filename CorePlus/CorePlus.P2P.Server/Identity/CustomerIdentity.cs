using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.Entity;
using System.Data.Objects.SqlClient;
using System.Net.Sockets;
using System.Threading;
using Core;
using CorePlus.Common;

namespace CorePlus.P2P.Server
{
    public class CustomerIdentity : BaseIdentity<P2PServerRepository, DialogInfoEntity>
    {
        protected override void ValidateIdentity(SocketP2PEntity client, SocketP2PMessageEntity message, List<SocketP2PEntity> clients,
            List<SocketP2PEntity> queue, SendToClientCompleteHandler sendToClientCompleteHandler,
            OutPutSystemMessageHandler outPutSystemMessageHandler)
        {
            LogCommonHelper.WriteLog("开始分配客服");
            LogCommonHelper.WriteLog("客户端数量:" + clients.Count);

            var servants = clients.Where(x => x.Identity == "SERVANT"
                                                            && (x.Owner == message.Owner || x.WeiXinNo == message.WeiXinNo)
                                                            && x.Receiver == null);

            LogCommonHelper.WriteLog("空闲的客服数量:" + servants.Count());

            var servant = servants.FirstOrDefault();

            bool isSuccess = false;
            if (servant != null)
            {
                #region ==== 随机分配客服 ====
                LogCommonHelper.WriteLog("随机分配客服");
                servant.Receiver = client.Sender;
                client.Receiver = servant.Sender;
                try
                {
                    if (servant.Socket.Connected)
                    {
                        var PreparingMessageEntity = NewP2PMessage();
                        PreparingMessageEntity.Sender = servant.Receiver;
                        PreparingMessageEntity.Receiver = servant.Sender;
                        PreparingMessageEntity.Identity = "SERVER";
                        PreparingMessageEntity.Data = string.Format("您好，访客({0})，需要您的服务", servant.Receiver);
                        PreparingMessageEntity.Owner = servant.Owner;
                        PreparingMessageEntity.WeiXinNo = servant.WeiXinNo;
                        PreparingMessageEntity.OpenId = servant.OpenId;

                        SendMessageToTerminal(PreparingMessageEntity, servant, sendToClientCompleteHandler);

                        isSuccess = true;

                        if (queue.Exists(x => x.Sender == client.Sender))
                        {
                            queue.Remove(client);
                        }
                    }
                }
                catch (SocketException ex)
                {
                    servant.Socket.Close();
                    clients.Remove(servant);
                    outPutSystemMessageHandler(ex.Message);
                    return;
                }
                #endregion

                Thread.Sleep(500);

                if (isSuccess)
                {
                    #region ==== 分配成功, 向访客发送消息 ====
                    try
                    {
                        LogCommonHelper.WriteLog("分配成功，向访客发送消息");
                        // 更新发言时间
                        client.LastTalkTime = DateTime.Now;
                        // 发送消息
                        if (client.Socket.Connected)
                        {
                            var PreparingMessageEntity = NewP2PMessage();
                            PreparingMessageEntity.Sender = client.Receiver;
                            PreparingMessageEntity.Receiver = client.Sender;
                            PreparingMessageEntity.Identity = "SERVER";
                            PreparingMessageEntity.Data = string.Format("您好，客服({0})，正在为您服务，如果超过3分钟未发言，系统自动将您断线", client.Receiver);
                            PreparingMessageEntity.Owner = client.Owner;
                            PreparingMessageEntity.WeiXinNo = client.WeiXinNo;
                            PreparingMessageEntity.OpenId = client.OpenId;

                            SendMessageToTerminal(PreparingMessageEntity, client, sendToClientCompleteHandler);
                        }
                    }
                    catch (SocketException ex)
                    {
                        client.Socket.Close();
                        clients.Remove(client);
                        outPutSystemMessageHandler(ex.Message);
                    }
                    #endregion
                }
                else
                {
                    #region ==== 客服已经断线 ====
                    try
                    {
                        LogCommonHelper.WriteLog("客服已经断线，请刷新后重新连接");
                        client.Receiver = null;
                        client.DialogId = null;
                        var PreparingMessageEntity = NewP2PMessage();
                        PreparingMessageEntity.Sender = null;
                        PreparingMessageEntity.Receiver = client.Sender;
                        PreparingMessageEntity.Identity = "SERVER";
                        PreparingMessageEntity.Data = "客服已经断线，请刷新后重新连接";
                        PreparingMessageEntity.Owner = client.Owner;
                        PreparingMessageEntity.WeiXinNo = client.WeiXinNo;
                        PreparingMessageEntity.OpenId = client.OpenId;

                        SendMessageToTerminal(PreparingMessageEntity, client, sendToClientCompleteHandler);
                    }
                    catch (SocketException ex)
                    {
                        client.Socket.Close();
                        clients.Remove(client);
                        outPutSystemMessageHandler(ex.Message);
                    }
                    #endregion
                }
            }
            else
            {
                #region ==== 客服全忙 ====
                LogCommonHelper.WriteLog("客服全忙");
                try
                {
                    if (client.Socket.Connected)
                    {
                        LogCommonHelper.WriteLog("正在加入队列");
                        if (!queue.Exists(x => x.Sender == message.Sender))
                        {
                            queue.Add(client);
                        }

                        int i = queue.IndexOf(client);
                        int n = i + 1;

                        var PreparingMessageEntity = NewP2PMessage();
                        PreparingMessageEntity.Sender = null;
                        PreparingMessageEntity.Receiver = client.Sender;
                        PreparingMessageEntity.Identity = "SERVER";
                        PreparingMessageEntity.Data = (i == 0)
                            ? string.Format("抱歉，客服全忙。系统自动为您排队，队列序号{0}，客服即将为您服务，请耐心等待", n, i)
                            : string.Format("抱歉，客服全忙。系统自动为您排队，队列序号{0}，您前面还有{1}人", n, i);
                        PreparingMessageEntity.Owner = client.Owner;
                        PreparingMessageEntity.WeiXinNo = client.WeiXinNo;
                        PreparingMessageEntity.OpenId = client.OpenId;

                        SendMessageToTerminal(PreparingMessageEntity, client, sendToClientCompleteHandler);
                    }
                }
                catch (SocketException ex)
                {
                    client.Socket.Close();
                    clients.Remove(client);
                    outPutSystemMessageHandler(ex.Message);
                }
                #endregion
            }
        }

        protected override void SendingByRuler(DialogInfoEntity entity, SocketP2PEntity client, SocketP2PMessageEntity message,
            List<SocketP2PEntity> clients, List<SocketP2PEntity> queue,
            SendToClientCompleteHandler sendToClientCompleteHandler,
            OutPutSystemMessageHandler outPutSystemMessageHandler)
        {
            LogCommonHelper.WriteLog("由访客发来的消息，发给客服" + message.Receiver);

            var servant = clients.Where(x => x.Receiver == message.Sender).FirstOrDefault();

            if (servant != null && servant.Socket.Connected)
            {
                try
                {
                    client.LastTalkTime = DateTime.Now;

                    var PreparingMessageEntity = NewP2PMessage();
                    PreparingMessageEntity.Sender = servant.Receiver;
                    PreparingMessageEntity.Receiver = servant.Sender;
                    PreparingMessageEntity.Data = message.Data;
                    PreparingMessageEntity.Owner = servant.Owner;
                    PreparingMessageEntity.Identity = "CUSTOMER";
                    PreparingMessageEntity.WeiXinNo = servant.WeiXinNo;
                    PreparingMessageEntity.OpenId = servant.OpenId;

                    SendMessageToTerminal(PreparingMessageEntity, servant, sendToClientCompleteHandler);
                }
                catch (SocketException ex)
                {
                    client.Socket.Close();
                    clients.Remove(client);
                    outPutSystemMessageHandler(ex.Message);
                }
            }
            else if (queue.Where(x => x.Sender == message.Sender).Any())
            {
                try
                {
                    client.LastTalkTime = DateTime.Now;
                    int i = queue.IndexOf(client) + 1;

                    var PreparingMessageEntity = NewP2PMessage();
                    PreparingMessageEntity.Sender = null;
                    PreparingMessageEntity.Receiver = client.Sender;
                    PreparingMessageEntity.Identity = "SERVER";
                    PreparingMessageEntity.Data = string.Format("您正在排队，队列序号{0}，您前面还有{1}人", i, i - 1);
                    PreparingMessageEntity.Owner = client.Owner;
                    PreparingMessageEntity.WeiXinNo = client.WeiXinNo;
                    PreparingMessageEntity.OpenId = client.OpenId;

                    SendMessageToTerminal(PreparingMessageEntity, client, sendToClientCompleteHandler);
                }
                catch (SocketException ex)
                {
                    client.Socket.Close();
                    clients.Remove(client);
                    outPutSystemMessageHandler(ex.Message);
                }
            }
            else
            {
                try
                {
                    var PreparingMessageEntity = NewP2PMessage();
                    PreparingMessageEntity.Sender = null;
                    PreparingMessageEntity.Receiver = client.Sender;
                    PreparingMessageEntity.Identity = "SERVER";
                    PreparingMessageEntity.Data = "客服已经断线，请刷新后重新连接";
                    PreparingMessageEntity.Owner = client.Owner;
                    PreparingMessageEntity.WeiXinNo = client.WeiXinNo;
                    PreparingMessageEntity.OpenId = client.OpenId;

                    SendMessageToTerminal(PreparingMessageEntity, client, sendToClientCompleteHandler);
                }
                catch (SocketException ex)
                {
                    client.Socket.Close();
                    clients.Remove(client);
                    outPutSystemMessageHandler(ex.Message);
                }
            }
        }
    }
}