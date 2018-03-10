using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;
using Core;
using System.IO;
using System.Xml;
using CorePlus.Entity;
using CorePlus.Common;

namespace CorePlus.WeiXin.Service
{
    public class TextMessage : BaseMessage<WxTextRepository, WxTextEntity>
    {
        protected override string GetKeyword(WxTextEntity entity)
        {
            return entity.Content;
        }

        protected override string SendingMessage(WxTextEntity entity, WxAccountEntity user,
            OutReplyTextHandler outReplyTextHandler, OutReplyImageTextHandler outReplyImageTextHandler)
        {
            try
            {
                LogHelper.WriteLog("这是Text消息");
                if (entity.Content.StartsWith("我是客服"))
                {
                    string[] loginItems = entity.Content.Split(new char[] { '#' });
                    if (loginItems.Length == 3 && Equals(loginItems[0], "我是客服"))
                    {
                        LogHelper.WriteLog("这是客服登录请求");
                        string loginName = loginItems[1];
                        string loginPwd = CryptHelper.MD5(loginItems[2]);

                        ServantService.ServantServiceSoapClient servant = new ServantService.ServantServiceSoapClient();
                        ServantService.ServantInfoEntity model = servant.Login(loginName, loginPwd);
                        if (model != null)
                        {
                            LogHelper.WriteLog("登录成功");
                            bool b = servant.UpdateWeiXin(model.ID, entity.FromUserName, entity.ToUserName);
                            LogHelper.WriteLog(b ? "更新微信信息成功" : "更新微信信息失败");

                            if (b)
                            {
                                // 发Socket消息，缓存到P2PServer
                                SocketService.SocketServiceSoapClient socket = new SocketService.SocketServiceSoapClient();
                                SocketService.SocketP2PMessageEntity message = new SocketService.SocketP2PMessageEntity();
                                message.Sender = model.UserName;
                                message.Identity = "SERVANT";
                                message.Data = "CHECKIDENTITY";
                                message.WeiXinNo = entity.ToUserName;
                                message.OpenId = entity.FromUserName;
                                message.Owner = model.UserId.ToString();
                                socket.SendMessageToP2PServer(message);

                                return string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    string[] replys = entity.Content.Split(new char[] { '#' });
                    long cid = 0;
                    if (long.TryParse(replys[0], out cid))
                    {
                        ServantService.ServantServiceSoapClient servant = new ServantService.ServantServiceSoapClient();
                        ServantService.ServantInfoEntity model = servant.GetByOpenId(entity.FromUserName);
                        if (model != null)
                        {
                            // 回复访客
                            SocketService.SocketServiceSoapClient socket = new SocketService.SocketServiceSoapClient();
                            SocketService.SocketP2PMessageEntity message = new SocketService.SocketP2PMessageEntity();
                            message.Sender = model.UserName;
                            message.Receiver = replys[0];
                            message.Identity = "SERVANT";
                            message.Data = replys[1];
                            message.WeiXinNo = entity.ToUserName;
                            message.OpenId = entity.FromUserName;
                            message.Owner = model.UserId.ToString();
                            socket.SendMessageToP2PServer(message);

                            return string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Text SendingMessage Error：" + ex.InnerException.Message);
            }

            return base.SendingMessage(entity, user, outReplyTextHandler, outReplyImageTextHandler);
        }
    }
}