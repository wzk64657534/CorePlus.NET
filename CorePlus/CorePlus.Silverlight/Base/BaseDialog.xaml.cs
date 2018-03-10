using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CorePlus.Silverlight
{
    public partial class BaseDialog : UserControl
    {
        public BaseDialog()
        {
            InitializeComponent();
            this.btnClear.Click += new RoutedEventHandler(btnClear_Click);
            this.btnSaveReport.Click += new RoutedEventHandler(btnSaveReport_Click);
            this.btnSend.Click += new RoutedEventHandler(btnSend_Click);
            this.txbMsg.KeyDown += new KeyEventHandler(txbMsg_KeyDown);
            this.Loaded += new RoutedEventHandler(Page_Loaded);
        }

        #region ==== 全局变量 ====
        StringBuilder sbReport = new StringBuilder();

        Socket client;
        SynchronizationContext syn;

        protected string Sender = null;
        protected string Owner = null;
        protected string Identity = null;
        protected string WeiXinNo = null;
        SocketMessage receivedMessage = null;
        #endregion

        private void Send(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                MessageBox.Show("请输入内容后发送", "提示", MessageBoxButton.OK);
                return;
            }

            if (Encoding.UTF8.GetBytes(msg).Length > 300)
            {
                MessageBox.Show("超过长度限制，最长输入150个字", "提示", MessageBoxButton.OK);
                return;
            }

            if (client != null && client.Connected)
            {
                try
                {
                    SocketMessage SendMessage = new SocketMessage();
                    SendMessage.Sender = this.Sender;
                    SendMessage.Receiver = receivedMessage.Sender;
                    SendMessage.Identity = this.Identity;
                    SendMessage.WeiXinNo = receivedMessage.WeiXinNo;
                    SendMessage.OpenId = receivedMessage.OpenId;
                    SendMessage.Data = msg;
                    SendMessage.Owner = this.Owner;
                    // 发送到面板
                    SynOutPut(null, SendMessage);
                    this.txbMsg.Text = string.Empty;
                    // 发送到服务器的数据
                    byte[] buffer = this.TransferMessageToBytes(SendMessage);

                    List<ArraySegment<byte>> _lst = new List<ArraySegment<byte>>();
                    _lst.Add(new ArraySegment<byte>(buffer));
                    SocketAsyncEventArgs SendArgs = new SocketAsyncEventArgs();
                    SendArgs.RemoteEndPoint = client.RemoteEndPoint;
                    SendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SendArgs_Completed);
                    SendArgs.BufferList = _lst;
                    client.SendAsync(SendArgs);
                    //每次发送完停一会儿，避免下次发送时“粘包”
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    SynOutPut(ex.InnerException.Message);
                }
            }
            else
            {
                SynOutPut(ConstHelper.NonConnectedWithServer);
            }
        }

        private byte[] TransferMessageToBytes(SocketMessage message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"Sender\":\"{0}\",", message.Sender);
            sb.AppendFormat("\"Receiver\":\"{0}\",", message.Receiver);
            sb.AppendFormat("\"Identity\":\"{0}\",", message.Identity);
            sb.AppendFormat("\"MsgType\":\"{0}\",", message.MsgType);
            sb.AppendFormat("\"DialogId\":\"{0}\",", message.DialogId);
            sb.AppendFormat("\"Data\":\"{0}\",", message.Data);
            sb.AppendFormat("\"OpenId\":\"{0}\",", message.OpenId);
            sb.AppendFormat("\"WeiXinNo\":\"{0}\",", message.WeiXinNo);
            sb.AppendFormat("\"Owner\":\"{0}\"", message.Owner);
            sb.Append("}");

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private SocketMessage InitEntity(byte[] buffer, int dataLength)
        {
            string strJson = Encoding.UTF8.GetString(buffer, 0, dataLength).Replace("\0", "");
            SocketMessage entity = new SocketMessage();
            string[] kvs = strJson.Replace("{", "").Replace("}", "").Split(new char[] { ',' });
            entity.Sender = GetValueFromJson(kvs, "Sender");
            entity.Receiver = GetValueFromJson(kvs, "Receiver");
            entity.Identity = GetValueFromJson(kvs, "Identity");
            entity.MsgType = GetValueFromJson(kvs, "MsgType");
            entity.Data = GetValueFromJson(kvs, "Data");
            entity.OpenId = GetValueFromJson(kvs, "OpenId");
            entity.WeiXinNo = GetValueFromJson(kvs, "WeiXinNo");
            entity.Owner = GetValueFromJson(kvs, "Owner");

            return entity;
        }

        private string GetValueFromJson(string[] kvs, string key)
        {
            foreach (var kv in kvs)
            {
                string[] pair = kv.Split(new char[] { ':' });
                string k = pair[0].Trim(new char[] { '"' });
                if (k == key)
                {
                    string v = pair[1].Trim(new char[] { '"' });
                    return v == "null" ? null : v;
                }
            }

            return null;
        }

        private void CheckIdentity()
        {
            if (client != null && client.Connected)
            {
                try
                {
                    SocketMessage message = new SocketMessage();
                    message.Sender = Sender;
                    message.Identity = Identity;
                    message.Owner = Owner;
                    message.Data = "CHECKIDENTITY";

                    byte[] buffer = this.TransferMessageToBytes(message);

                    List<ArraySegment<byte>> _lst = new List<ArraySegment<byte>>();
                    _lst.Add(new ArraySegment<byte>(buffer));
                    SocketAsyncEventArgs SendArgs = new SocketAsyncEventArgs();
                    SendArgs.RemoteEndPoint = client.RemoteEndPoint;
                    SendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SendArgs_Completed);
                    SendArgs.BufferList = _lst;
                    client.SendAsync(SendArgs);
                    // 每次发送完停一会儿，避免下次发送时“粘包”
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    SynOutPut(ex.InnerException.Message);
                }
            }
            else
            {
                SynOutPut(ConstHelper.ConnectedBreak);
            }
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        protected void Connection()
        {
            try
            {
                syn = SynchronizationContext.Current;

                if (client == null)
                {
                    // 实例化 Socket
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    client.ReceiveBufferSize = ConstHelper.BUFFER_SIZE;
                    client.SendBufferSize = ConstHelper.BUFFER_SIZE;
                    client.NoDelay = true;
                }

                if (client.Connected)
                {
                    return;
                }

                // 实例化 SocketAsyncEventArgs ，用于对 Socket 做异步操作，很方便
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                // 服务器的 EndPoint
                args.RemoteEndPoint = new DnsEndPoint(Application.Current.Host.Source.DnsSafeHost, 4518);
                // 异步操作完成后执行的事件
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnSocketConnectCompleted);

                // 异步连接服务端
                client.ConnectAsync(args);
            }
            catch (Exception ex)
            {
                SynOutPut(ex.InnerException.Message, null);
            }
        }

        #region ==== Socket 回调函数 ====
        /// <summary>
        /// 连接完成回调处理
        /// </summary>
        private void OnSocketConnectCompleted(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                //显示连接结果
                if (client == null || !client.Connected)
                {
                    SynOutPut(ConstHelper.NonConnectedWithServer);
                }
                else
                {
                    SynOutPut(ConstHelper.ConnectedWithServer);
                    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                    args.RemoteEndPoint = e.RemoteEndPoint;

                    // 设置数据缓冲区
                    byte[] buffer = new byte[ConstHelper.BUFFER_SIZE];
                    args.SetBuffer(buffer, 0, buffer.Length);

                    args.Completed += new EventHandler<SocketAsyncEventArgs>(OnSocketReceiveCompleted);
                    // 异步地从服务端 Socket 接收数据
                    client.ReceiveAsync(args);

                    // 发送身份信息
                    this.CheckIdentity();
                }
            }
            catch (Exception ex)
            {
                SynOutPut(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 接受服务端发来的数据-回调处理
        /// </summary>
        private void OnSocketReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                if (e.BytesTransferred > 0)
                {
                    // 服务器发送过来的数据
                    receivedMessage = null;
                    receivedMessage = InitEntity(e.Buffer, e.BytesTransferred);
                    // 发送到记录显示板
                    SynOutPut(null, receivedMessage);
                }
            }
            catch (Exception ex)
            {
                SynOutPut(ex.InnerException.Message);
            }

            try
            {
                // 继续异步地从服务端 Socket 接收数据(类似长连接)
                if (client != null && client.Connected)
                {
                    client.ReceiveAsync(e);
                }
                else
                {
                    SynOutPut(ConstHelper.ConnectedBreak);
                }
            }
            catch (Exception ex)
            {
                SynOutPut(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 发送完成后的回调函数
        /// </summary>
        private void SendArgs_Completed(object sender, SocketAsyncEventArgs e)
        {

        }
        #endregion

        #region ==== 显示信息 ====
        private void SynOutPut(string msg, SocketMessage message = null)
        {
            string report = string.Empty;
            if (string.IsNullOrEmpty(msg) == false)
            {
                report += string.Format("系统：{0}\r\n", msg);
            }
            else if (message != null && message.Identity == "SERVER")
            {
                report += string.Format("系统：{0}\r\n", message.Data);
            }
            else if (message != null && message.Identity != "SERVER")
            {
                string name = (message.Identity == "SERVANT") ? "客服" : "访客";
                report += string.Format("{2}({0}) {1} \r\n", message.Sender, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), name);
                report += string.Format("\t\t{0}", message.Data);
            }
            else
            {
                report += string.Format("系统：未知错误\r\n");
            }

            syn.Post(OutPut, report);

            if (message != null)
            {
                syn.Post(OutPutHtml, message);
            }
        }

        protected void OutPut(object msg)
        {
            this.sbReport.Append(msg.ToString());
            this.lbxReport.Items.Add(new TextBlock()
            {
                Text = msg.ToString(),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 12,
                Width = 750,
                Margin = new Thickness(0, 3, 0, 0)
            });
            this.lbxReport.UpdateLayout();
            this.lbxReport.ScrollIntoView(this.lbxReport.Items[this.lbxReport.Items.Count - 1]);
        }

        private void OutPutHtml(object obj)
        {
            //SocketMessage message = obj as SocketMessage;

            //if (message != null && (message.Sender == receivedMessage.Receiver))
            //{
            //    // 提示新消息
            //    HtmlPage.Window.Invoke("editTitle", 1);
            //}
            //else if (message != null && (message.Sender != receivedMessage.Receiver))
            //{
            //    // 关闭新消息
            //    HtmlPage.Window.Invoke("editTitle", 0);
            //}
        }
        #endregion

        #region ==== 事件 ====
        protected virtual void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Connection();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            this.Send(this.txbMsg.Text);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.lbxReport.Items.Clear();
            this.sbReport.Clear();
        }

        private void btnSaveReport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            if (sfg.ShowDialog() == true)
            {
                using (Stream fs = sfg.OpenFile())
                {
                    //相关文件写入操作
                    byte[] buffer = Encoding.UTF8.GetBytes(this.sbReport.ToString());
                    fs.Write(buffer, 0, buffer.Length);
                }

                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK);
            }
        }

        private void txbMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Send(this.txbMsg.Text);
            }
        }

        #endregion
    }
}