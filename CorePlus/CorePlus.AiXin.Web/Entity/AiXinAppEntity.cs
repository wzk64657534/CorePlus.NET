using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.AiXin.Web
{
    public class AiXinAppEntity : AiXinEntity
    {
        /// <summary>
        /// 登录签名
        /// </summary>
        public string LoginSign { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; set; }
    }
}