using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.Entity;
using System.Data.Objects.SqlClient;
using System.Net.Sockets;
using System.Threading;
using CorePlus.Common;

namespace CorePlus.P2P.Server
{
    public class ServantIdentity : BaseIdentity<P2PServerRepository, DialogInfoEntity>
    {
        protected override void ValidateIdentity(SocketP2PEntity client, SocketP2PMessageEntity message,
            List<SocketP2PEntity> clients, List<SocketP2PEntity> queue,
            SendToClientCompleteHandler sendToClientCompleteHandler, OutPutSystemMessageHandler outPutSystemMessageHandler)
        {
            try
            {
                LogCommonHelper.WriteLog("我是客服，等待访客请求");

                var PreparingMessageEntity = NewP2PMessage();
                PreparingMessageEntity.Sender = null;
                PreparingMessageEntity.Receiver = client.Sender;
                PreparingMessageEntity.Identity = "SERVER";
                PreparingMessageEntity.Data = string.Format("您好，客服({0})，请您等待访客请求...", client.Sender);
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

        protected override void SendingByRuler(DialogInfoEntity entity,
            SocketP2PEntity client, SocketP2PMessageEntity message,
            List<SocketP2PEntity> clients, List<SocketP2PEntity> queue,
            SendToClientCompleteHandler sendToClientCompleteHandler, OutPutSystemMessageHandler outPutSystemMessageHandler)
        {
            if (message.Data.Replace(" ", "").ToLower() == "exitall")
            {
                LogCommonHelper.WriteLog("客服全部退出");
                clients.RemoveAll(x => x.Sender == message.Sender && (x.Owner == message.Owner || x.WeiXinNo == message.WeiXinNo));
            }
            else if (message.Data.Replace(" ", "").ToLower() == "exit")
            {
                LogCommonHelper.WriteLog(string.Format("客服({0})退出", message.Sender));
                clients.RemoveAll(x => x.Sender == message.Sender && x.Receiver == message.Receiver);
            }
            else
            {
                LogCommonHelper.WriteLog("由客服发来的消息，发给访客" + message.Receiver);

                var customer = clients.Where(x => x.Sender == message.Receiver).FirstOrDefault();
                if (customer != null)
                {
                    try
                    {
                        LogCommonHelper.WriteLog("开始向访客发送消息");
                        // 记录最后通话时间
                        customer.LastTalkTime = DateTime.Now;

                        var PreparingMessageEntity = NewP2PMessage();
                        PreparingMessageEntity.Sender = customer.Receiver;
                        PreparingMessageEntity.Receiver = customer.Sender;
                        PreparingMessageEntity.Data = message.Data;
                        PreparingMessageEntity.Owner = customer.Owner;
                        PreparingMessageEntity.Identity = "SERVANT";
                        PreparingMessageEntity.WeiXinNo = customer.WeiXinNo;
                        PreparingMessageEntity.OpenId = customer.OpenId;

                        SendMessageToTerminal(PreparingMessageEntity, customer, sendToClientCompleteHandler);
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
                    var servant = clients.Where(x => x.Receiver == message.Receiver).FirstOrDefault();
                    if (servant != null)
                    {
                        try
                        {
                            LogCommonHelper.WriteLog("向客服发送访客断线的消息");

                            var PreparingMessageEntity = NewP2PMessage();
                            PreparingMessageEntity.Sender = null;
                            PreparingMessageEntity.Receiver = servant.Sender;
                            PreparingMessageEntity.Identity = "SERVER";
                            PreparingMessageEntity.Data = string.Format("访客({0})已经断线，请您等待其他的访客请求...", message.Receiver);
                            PreparingMessageEntity.Owner = servant.Owner;
                            PreparingMessageEntity.WeiXinNo = servant.WeiXinNo;
                            PreparingMessageEntity.OpenId = servant.OpenId;

                            servant.Receiver = null;
                            SendMessageToTerminal(PreparingMessageEntity, servant, sendToClientCompleteHandler);
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
    }
}