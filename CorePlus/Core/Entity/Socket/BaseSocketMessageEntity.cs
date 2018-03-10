using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class BaseSocketMessageEntity
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 主题内容
        /// </summary>
        public string Data { get; set; }
    }
}