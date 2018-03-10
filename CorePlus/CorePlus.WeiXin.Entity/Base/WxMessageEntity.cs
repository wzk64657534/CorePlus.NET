using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.WeiXin.Entity
{
    public class WxMessageEntity : WxEntity
    {
        public WxMessageEntity()
            : base()
        {

        }
        /// <summary>
        /// MsgId
        /// </summary>
        public long MsgId { get; set; }
    }
}
