using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.AiXin.Web
{
    public class AiXinEntity
    {
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// MD5运算后的登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 运营单位编码
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 营业厅编码
        /// </summary>
        public string ShopCode { get; set; }
        /// <summary>
        /// 终端编码
        /// </summary>
        public string PosCode { get; set; }
        /// <summary>
        /// 终端识别码
        /// </summary>
        public string PosMark { get; set; }

    }
}