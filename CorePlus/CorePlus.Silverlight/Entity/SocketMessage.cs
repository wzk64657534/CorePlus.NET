using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CorePlus.Silverlight
{
    public class SocketMessage
    {
        public SocketMessage()
        {
            MsgType = "00001";
        }
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
        /// <summary>
        /// 角色，SERVER-服务器, SERVANT-客服, C_PC-PC访客, C_WEIXIN-微信访客
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 内容类型：00001-字符串/文本
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