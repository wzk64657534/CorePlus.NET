using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;

namespace CorePlus.Web
{
    public class WxReplyTextController : WxUserNameController<WxReplyTextRepository, WxReplyTextEntity>
    {
        public override List<WxReplyTextEntity> GetSelectedData(List<WxReplyTextEntity> query)
        {
            return base.GetSelectedData(query).Where(x => x.Keyword != "subscribe").ToList();
        }
    }
}