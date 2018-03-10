using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;

namespace CorePlus.Entity
{
    public class SocketP2PEntity : BaseSocketEntity
    {
        /// <summary>
        /// 角色，SERVER-服务器, SERVANT-客服, CTM_PC-PC访客, CTM_WEIXIN-微信访客
        /// </summary>
        public string Identity { get; set; }
        // 发送者
        public string Sender { get; set; }
        // 接收者
        public string Receiver { get; set; }
        // 最后一次对话时间
        public Nullable<DateTime> LastTalkTime { get; set; }
        // 对话编号
        public string DialogId { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// 第3方编号
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 微信服务编号
        /// </summary>
        public string WeiXinNo { get; set; }
    }
}