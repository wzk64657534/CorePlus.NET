using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;
using Core;
using System.Data.Objects.SqlClient;
using CorePlus.Common;

namespace CorePlus.WeiXin.Service
{
    public class SubscribeEventMessage : BaseMessage<WxSubscribeEventRepository, WxSubscribeEventEntity>
    {
        protected override string GetKeyword(WxSubscribeEventEntity entity)
        {
            return entity.Event;
        }

        protected override string SendingMessage(WxSubscribeEventEntity entity, WxAccountEntity user,
            OutReplyTextHandler OutReplyTextHandler, OutReplyImageTextHandler OutReplyImageTextHandler)
        {
            LogCommonHelper.WriteLog("开始处理关注/取消数据");
            var db = CoreDBContext.GetContext();
            //讀取配置
            var config = (from x in db.Set<WxSubscribeConfigEntity>()
                          where x.UserName == user.UserName
                          select x).FirstOrDefault();

            if (config != null)
            {
                if (config.IsSaveUser)
                {
                    // 记录到服务的数据库
                    WxSubUserEntity subUser = new WxSubUserEntity();
                    subUser.UserName = user.UserName;
                    subUser.OpenId = entity.FromUserName;
                    WxSubUserRepository repository = new WxSubUserRepository();
                    repository.Add(subUser);

                    // 记录到别人的数据库
                    if (string.IsNullOrWhiteSpace(config.SaveUrl) == false)
                    {
                        Uri uri = new Uri(config.SaveUrl);
                        string url = string.Empty;
                        if (string.IsNullOrEmpty(uri.Query))
                        {
                            url = string.Format("{0}?openid={1}", config.SaveUrl, entity.FromUserName);
                        }
                        else
                        {
                            url = string.Format("{0}&openid={1}", config.SaveUrl, entity.FromUserName);
                        }

                        if (!string.IsNullOrEmpty(url))
                        {
                            string html = WebHelper.GetFormWebRequest(url);
                        }
                    }
                }

                // 发送消息
                WxReplyTextEntity textReply = null;
                List<WxReplyImageTextEntity> imageReply = null;
                switch (config.ReplyType.ToLower())
                {
                    case "text":
                        textReply = (from x in db.Set<WxReplyTextEntity>()
                                     where x.UserName == user.UserName
                                     && x.Keyword == "subscribe"
                                     select x).FirstOrDefault();
                        break;
                    case "imagetext":
                        var itReply = (from x in db.Set<WxReplyImageTextEntity>()
                                       where x.UserName == user.UserName
                                       && x.Keyword == "subscribe"
                                       select x).FirstOrDefault();

                        if (itReply != null && !string.IsNullOrEmpty(itReply.WithIds))
                        {
                            string[] withIds = itReply.WithIds.Split(new char[] { '|' });
                            imageReply = (from x in db.Set<WxReplyImageTextEntity>()
                                          where x.UserName == user.UserName
                                          && withIds.Contains(SqlFunctions.StringConvert((decimal)x.ID).Trim())
                                          select x).ToList();
                        }

                        imageReply.Insert(0, itReply);
                        break;
                }

                if (textReply != null)
                {
                    return OutReplyTextHandler(entity, textReply);
                }

                if (imageReply != null)
                {
                    return OutReplyImageTextHandler(entity, imageReply);
                }
            }

            return string.Empty;
        }
    }
}