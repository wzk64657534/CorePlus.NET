using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;

namespace CorePlus.Entity
{
    [Serializable]
    public class SocketP2PMessageEntity : BaseSocketMessageEntity
    {
        public SocketP2PMessageEntity()
        {
            MsgType = "TEXT";
        }

        /// <summary>
        /// 角色，SERVER-服务器, SERVANT-客服, CUSTOMER-访客
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 内容类型：TEXT-字符串/文本
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 对话编号，多开窗口时使用
        /// </summary>
        public string DialogId { get; set; }
        /// <summary>
        /// 第3方编号
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 微信服务编号
        /// </summary>
        public string WeiXinNo { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public string Owner { get; set; }
    }
}