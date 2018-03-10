using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace CorePlus.WeiXin.Entity
{
    public class WxEntity : BaseEntity
    {
        public WxEntity()
        {
            RecieveTime = DateTime.Now;
        }
        /// <summary>
        /// ToUserName
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// FromUserName
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public int CreateTime { get; set; }
        /// <summary>
        /// MsgType
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// RecieveTime
        /// </summary>
        public DateTime RecieveTime { get; set; }
    }
}