using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.WeiXin.Entity;
using CorePlus.WeiXin.Repository;
using CorePlus.Entity;
using Core;

namespace CorePlus.WeiXin.Service
{
    public class MenuEventMessage : BaseMessage<WxMenuEventRepository, WxMenuEventEntity>
    {
        protected override string GetKeyword(WxMenuEventEntity entity)
        {
            return entity.EventKey;
        }

        protected override string SendingMessage(WxMenuEventEntity entity, WxAccountEntity user, OutReplyTextHandler outReplyTextHandler, OutReplyImageTextHandler outReplyImageTextHandler)
        {
            switch (entity.EventKey.ToLower())
            {
                case "exitall":
                    ServantService.ServantServiceSoapClient servant = new ServantService.ServantServiceSoapClient();
                    ServantService.ServantInfoEntity model = servant.GetByOpenId(entity.FromUserName);
                    if (model != null)
                    {
                        // 回复访客
                        SocketService.SocketServiceSoapClient socket = new SocketService.SocketServiceSoapClient();
                        SocketService.SocketP2PMessageEntity message = new SocketService.SocketP2PMessageEntity();
                        message.Sender = model.UserName;
                        message.Receiver = "exitall";
                        message.Identity = "SERVANT";
                        message.Data = "exitall";
                        message.WeiXinNo = entity.ToUserName;
                        message.OpenId = entity.FromUserName;
                        message.Owner = model.UserId.ToString();
                        socket.SendMessageToP2PServer(message);
                    }
                    return string.Empty;
            }

            return base.SendingMessage(entity, user, outReplyTextHandler, outReplyImageTextHandler);
        }
    }
}