using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;
using Core;
using CorePlus.WeiXin.Entity;
using CorePlus.WeiXin.Repository;
using CorePlus.Common;

namespace CorePlus.WeiXin.Service
{
    public class BaseMessage<TRepository, TEntity> : IMessage
        where TRepository : WxMessageBaseRepository<TEntity>, new()
        where TEntity : WxEntity, new()
    {
        public string Save(string postStr, string signature, string timestamp, string nonce)
        {
            try
            {
                #region ==== 微信數據處理 ====
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(postStr);

                TEntity entity = new TEntity();

                Type type = typeof(TEntity);
                PropertyInfo[] propertyinfos = type.GetProperties();
                foreach (var item in propertyinfos)
                {
                    var list = xml.GetElementsByTagName(item.Name);
                    if (item.Name == "ID")
                    {
                        item.SetValue(entity, 0, null);
                    }
                    else
                    {
                        string v = string.Empty;
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].ChildNodes[0].NodeType == System.Xml.XmlNodeType.CDATA)
                            {
                                v = list[i].ChildNodes[0].Value;
                            }
                            else
                            {
                                v = list[i].InnerText;
                            }

                            item.SetValue(entity, GetValueOfType(item, v), null);
                        }
                    }
                }

                LogCommonHelper.WriteLog("ToUserName：" + entity.ToUserName);

                WxAccountRepository cr = new WxAccountRepository();
                var user = cr.FindByExpression(m => m.WeiXinNo == entity.ToUserName).FirstOrDefault();
                if (user == null)
                {
                    LogCommonHelper.WriteLog("开始处理微信标识");
                    var users = cr.FindByExpression(m => m.TokenStatus == 0).ToList();
                    if (users == null) { return string.Empty; }

                    foreach (var item in users)
                    {
                        string[] ArrTmp = { item.Token, timestamp, nonce };
                        Array.Sort(ArrTmp);
                        string tmpStr = string.Join("", ArrTmp);
                        tmpStr = CryptHelper.SHA1(tmpStr);
                        tmpStr = tmpStr.ToLower();

                        if (tmpStr == signature)
                        {
                            LogCommonHelper.WriteLog("标识处理成功，开始更新信息");
                            item.WeiXinNo = entity.ToUserName;
                            item.TokenStatus = 2;
                            cr.Update(item.ID, item);
                            LogCommonHelper.WriteLog("更新信息完成");
                            break;
                        }
                    }
                }

                if (user.IsSaveRecord)
                {
                    TRepository respository = new TRepository();
                    respository.Add(entity);
                }
                #endregion

                OutReplyTextHandler outReplyTextHandler = new OutReplyTextHandler(OutReplyText);
                OutReplyImageTextHandler outReplyImageTextHandler = new OutReplyImageTextHandler(OutReplyImageText);

                return SendingMessage(entity, user, outReplyTextHandler, outReplyImageTextHandler);
            }
            catch (Exception ex)
            {
                LogCommonHelper.WriteLog("Exception：" + ex.InnerException.Message);
                return string.Empty;
            }
        }

        protected virtual string GetKeyword(TEntity entity)
        {
            return null;
        }

        protected virtual string SendingMessage(TEntity entity, WxAccountEntity user,
            OutReplyTextHandler outReplyTextHandler, OutReplyImageTextHandler outReplyImageTextHandler)
        {
            LogCommonHelper.WriteLog("开始回复");

            var db = CoreDBContext.GetContext();
            string keywords = this.GetKeyword(entity);
            LogCommonHelper.WriteLog("Keywords：" + keywords);

            if (string.IsNullOrEmpty(keywords)) { return string.Empty; }
            LogCommonHelper.WriteLog("读取文本配置");

            string[] kws = keywords.Split(new char[] { ' ' });

            var tReply = (from x in db.Set<WxReplyTextEntity>()
                          where x.UserName == user.UserName
                          && x.Keyword != "subscribe"
                          && x.MatchType == 1 ? (from y1 in
                                                     (from x1 in db.Set<WxReplyTextEntity>()
                                                      where x1.ID == x.ID
                                                      select x1)
                                                         .First()
                                                         .Keyword
                                                         .Split(new char[] { ' ' }).ToList()
                                                 where y1 == keywords
                                                 select y1).Any()
                                             : (from x2 in db.Set<WxReplyTextEntity>()
                                                where x2.ID == x.ID
                                                select x2)
                                                    .First()
                                                    .Keyword
                                                    .Split(new char[] { ' ' })
                                                    .Contains(keywords)
                          select x).FirstOrDefault();

            if (tReply != null)
            {
                return this.OutReplyText(entity, tReply);
            }
            else
            {
                var itReply = (from x in db.Set<WxReplyImageTextEntity>()
                               where x.UserName == user.UserName
                               && x.Keyword != "subscribe"
                               && x.MatchType == 1 ? (from y1 in
                                                          (from x1 in db.Set<WxReplyImageTextEntity>()
                                                           where x1.ID == x.ID
                                                           select x1)
                                                              .First()
                                                              .Keyword
                                                              .Split(new char[] { ' ' }).ToList()
                                                      where y1 == keywords
                                                      select y1).Any()
                                                  : (from x2 in db.Set<WxReplyImageTextEntity>()
                                                     where x2.ID == x.ID
                                                     select x2)
                                                         .First()
                                                         .Keyword
                                                         .Split(new char[] { ' ' })
                                                         .Contains(keywords)
                               select x).FirstOrDefault();

                if (itReply != null)
                {
                    List<WxReplyImageTextEntity> itReplyList = new List<WxReplyImageTextEntity>();
                    if (!string.IsNullOrEmpty(itReply.WithIds))
                    {
                        string[] withIds = itReply.WithIds.Split(new char[] { '|' });
                        itReplyList = (from m in db.Set<WxReplyImageTextEntity>()
                                       where m.UserName == user.UserName
                                       && m.Keyword != "subscribe"
                                       && withIds.Contains(SqlFunctions.StringConvert((decimal)m.ID).Trim())
                                       select m).ToList();
                    }

                    itReplyList.Insert(0, itReply);
                    return this.OutReplyImageText(entity, itReplyList);
                }
            }

            return string.Empty;
        }

        private string OutReplyText(TEntity entity, WxReplyTextEntity reply)
        {
            string[] array = new string[] 
                    { 
                        entity.FromUserName, 
                        entity.ToUserName, 
                        DateTimeHelper.ConvertDateTimeInt(DateTime.Now).ToString(), 
                        reply.Content
                    };

            return string.Format(ConstHelper.FORMAT_TEXT, array);
        }

        private string OutReplyImageText(TEntity entity, List<WxReplyImageTextEntity> reply)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", reply.Count);
            sb.Append("<Articles>");
            foreach (var item in reply)
            {
                sb.Append("<item>");
                sb.AppendFormat("<Title><![CDATA[{0}]]></Title>", item.Title);
                sb.AppendFormat("<Description><![CDATA[{0}]]></Description>", item.Description);
                sb.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", item.PicUrl);
                sb.AppendFormat("<Url><![CDATA[{0}]]></Url>", this.AppendParamters(item.Url, entity));
                sb.Append("</item>");
            }
            sb.Append("</Articles>");

            string[] array = new string[]
                    {
                        entity.FromUserName, 
                        entity.ToUserName, 
                        DateTimeHelper.ConvertDateTimeInt(DateTime.Now).ToString(),
                        sb.ToString()
                    };

            return string.Format(ConstHelper.FORMAT_IMAGE_TEXT, array);
        }

        private string AppendParamters(string url, TEntity entity)
        {
            Uri uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Query))
            {
                return url + "?openid=" + entity.FromUserName;
            }
            else
            {
                return url + "&openid=" + entity.FromUserName;
            }
        }

        private object GetValueOfType(PropertyInfo property, string v)
        {
            string name = property.PropertyType.ToString();
            switch (name)
            {
                case "System.Int32":
                    return int.Parse(v);
                case "System.Int64":
                    return long.Parse(v);
                case "System.Double":
                    return double.Parse(v);
                default:
                    return v;
            }
        }

        public delegate string OutReplyTextHandler(TEntity entity, WxReplyTextEntity reply);
        public delegate string OutReplyImageTextHandler(TEntity entity, List<WxReplyImageTextEntity> reply);
    }
}