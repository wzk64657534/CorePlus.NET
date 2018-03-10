using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Core;
using CorePlus.Entity;
using CorePlus.Common;

namespace CorePlus.P2P.Server
{
    public abstract class BaseIdentity<TRepository, TEntity> : IIdentity
        where TRepository : SimpleRepository<DialogInfoEntity>, new()
        where TEntity : DialogInfoEntity, new()
    {
        protected virtual SocketP2PMessageEntity NewP2PMessage()
        {
            return new SocketP2PMessageEntity();
        }

        public void Comunicate(SocketP2PEntity client, SocketP2PMessageEntity message,
            List<SocketP2PEntity> clients, List<SocketP2PEntity> queue,
            SendToClientCompleteHandler sendToClientCompleteHandler, OutPutSystemMessageHandler outPutSystemMessageHandler)
        {
            if (string.IsNullOrEmpty(message.Receiver))
            {
                if (Equals(message.Data, "CHECKIDENTITY"))
                {
                    if (client == null)
                    {
                        client = new SocketP2PEntity();
                    }
                    // 初始化访客端
                    client.Sender = message.Sender;
                    client.Receiver = null;
                    client.Owner = message.Owner;
                    client.Identity = message.Identity;
                    client.WeiXinNo = message.WeiXinNo;
                    client.OpenId = message.OpenId;
                    // 放入访客端集合
                    //if (!clients.Exists(x => x.Sender == client.Sender))
                    //{
                    // 允许多窗口对话
                    clients.Add(client);
                    //}

                    ValidateIdentity(client, message, clients, queue, sendToClientCompleteHandler, outPutSystemMessageHandler);
                }
                else
                {
                    var PreparingMessageEntity = NewP2PMessage();
                    PreparingMessageEntity.Sender = null;
                    PreparingMessageEntity.Receiver = client.Sender;
                    PreparingMessageEntity.Identity = "SERVER";
                    PreparingMessageEntity.Data = (message.Identity == "SERVANT")
                        ? string.Format("您好，客服({0})，请您等待访客请求后发言", client.Sender)
                        : string.Format("您好，访客({0})，请您等待客服响应后发言", client.Sender);
                    PreparingMessageEntity.Owner = message.Owner;
                    PreparingMessageEntity.WeiXinNo = message.WeiXinNo;
                    PreparingMessageEntity.OpenId = message.OpenId;

                    SendMessageToTerminal(PreparingMessageEntity, client, sendToClientCompleteHandler);
                }
            }
            else
            {
                // 记录消息
                var entity = SaveEntity(client, message);
                // 回发消息
                Sending(entity, client, message, clients, queue, sendToClientCompleteHandler, outPutSystemMessageHandler);
            }
        }

        protected abstract void ValidateIdentity(SocketP2PEntity client, SocketP2PMessageEntity message,
            List<SocketP2PEntity> clients, List<SocketP2PEntity> queue,
            SendToClientCompleteHandler sendToClientCompleteHandler,
            OutPutSystemMessageHandler outPutSystemMessageHandler);

        protected abstract void SendingByRuler(TEntity entity, SocketP2PEntity client, SocketP2PMessageEntity message,
            List<SocketP2PEntity> clients, List<SocketP2PEntity> queue,
            SendToClientCompleteHandler sendToClientCompleteHandler, OutPutSystemMessageHandler outPutSystemMessageHandler);

        protected virtual void SendMessageToTerminal(SocketP2PMessageEntity message, SocketP2PEntity client,
            SendToClientCompleteHandler sendToClientCompleteHandler)
        {
            try
            {
                if (!string.IsNullOrEmpty(client.OpenId))
                {
                    LogCommonHelper.WriteLog("Identity : " + message.Identity);

                    string data = message.Data;
                    if (Equals(message.Identity, "CUSTOMER"))
                    {
                        data = string.Format("访客({0}) {1} : {2}", message.Sender, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message.Data);
                    }

                    string msg = "{\"touser\":\"" + client.OpenId + "\",\"msgtype\":\"text\", \"text\":{\"content\":\"" + data + "\"}}";
                    LogCommonHelper.WriteLog("msg:" + msg);

                    if (!string.IsNullOrEmpty(msg))
                    {
                        WxAccountService.WxAccountServiceSoapClient WxAccount = new WxAccountService.WxAccountServiceSoapClient();
                        WxAccountService.WxAccountEntity WxAccountEntity = WxAccount.GetByWeiXinNo(message.WeiXinNo);
                        if (WxAccountEntity == null) { LogCommonHelper.WriteLog("WxAccount为NULL"); return; }

                        LogCommonHelper.WriteLog("已经获取WxAccount信息");
                        LogCommonHelper.WriteLog("UserName : " + WxAccountEntity.UserName);
                        LogCommonHelper.WriteLog("Token : " + WxAccountEntity.Token);
                        LogCommonHelper.WriteLog("AppId : " + WxAccountEntity.AppId);
                        LogCommonHelper.WriteLog("Secret : " + WxAccountEntity.Secret);

                        string un = WxAccountEntity.UserName;
                        string unkey = CryptHelper.MD5(string.Format("{0}{1}", un, WxAccountEntity.Token));
                        string url = string.Format("{0}?key=C011&un={1}&unkey={2}", ConfigurationHelper.Get("WxUrl"), un, unkey);
                        LogCommonHelper.WriteLog("URL：" + url);
                        string strReply = WebHelper.GetFormWebRequest(url, "POST", "body=" + msg);
                    }
                }
                else
                {
                    string json = JsonHelper.Serialize(message);
                    LogCommonHelper.WriteLog("发送Socket消息，内容：" + json);
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    client.Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(sendToClientCompleteHandler), client);
                }
            }
            catch (Exception ex)
            {
                LogCommonHelper.WriteLog("SendMessageToTerminal Exception：" + ex.InnerException.Message);
            }
        }

        private void Sending(TEntity entity, SocketP2PEntity client, SocketP2PMessageEntity message,
            List<SocketP2PEntity> clients, List<SocketP2PEntity> queue,
            SendToClientCompleteHandler sendToClientCompleteHandler, OutPutSystemMessageHandler outPutSystemMessageHandler)
        {
            if (message.Identity == "SERVER")
            {
                LogCommonHelper.WriteLog(string.Format("由系统发给{0}({1})的消息",
                                                                            message.Receiver.Length == 10 ? "访客" : "客服",
                                                                            message.Receiver));

                SendMessageToTerminal(message, client, sendToClientCompleteHandler);
            }
            else
            {
                SendingByRuler(entity, client, message, clients, queue, sendToClientCompleteHandler, outPutSystemMessageHandler);
            }
        }

        private TEntity SaveEntity(SocketP2PEntity client, SocketP2PMessageEntity message)
        {
            TEntity entity = new TEntity();
            entity.DialogContent = message.Data;
            entity.DialogTime = DateTime.Now;
            entity.FromId = message.Sender;
            entity.ToId = message.Receiver;

            //TRepository repository = new TRepository();
            //repository.Add(entity);

            return entity;
        }
    }
}