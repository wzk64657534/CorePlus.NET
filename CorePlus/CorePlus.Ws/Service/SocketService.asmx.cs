using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using Core;
using CorePlus.Common;
using CorePlus.Entity;
using CorePlus.Ws;

namespace CorePlus.Ws.Service
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SocketService : System.Web.Services.WebService, ISocket
    {
        Socket client = null;
        SynchronizationContext syn = null;
        SocketP2PMessageEntity message = null;

        string resultMsg = string.Empty;

        [WebMethod]
        public void SendMessageToP2PServer(SocketP2PMessageEntity entity)
        {
            LogCommonHelper.WriteLog("Ws SendMessageToP2PServer");
            this.Connection();
            message = entity;
        }

        private void Connection()
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

                // 实例化 SocketAsyncEventArgs ，用于对 Socket 做异步操作，很方便
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                // 服务器的 EndPoint
                args.RemoteEndPoint = new DnsEndPoint(HttpContext.Current.Request.UserHostAddress, 4518);
                // 异步操作完成后执行的事件
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnSocketConnectCompleted);

                // 异步连接服务端
                client.ConnectAsync(args);
            }
            catch (Exception ex)
            {
                LogCommonHelper.WriteLog("Connection:" + ex.InnerException.Message);
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
                    LogCommonHelper.WriteLog("Ws 无法连接到服务器，请刷新后再试...");
                }
                else
                {
                    LogCommonHelper.WriteLog("Ws 与服务器连接成功...");

                    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                    args.RemoteEndPoint = e.RemoteEndPoint;

                    // 设置数据缓冲区
                    byte[] buffer = new byte[ConstHelper.BUFFER_SIZE];
                    args.SetBuffer(buffer, 0, buffer.Length);

                    args.Completed += new EventHandler<SocketAsyncEventArgs>(OnSocketReceiveCompleted);
                    // 异步地从服务端 Socket 接收数据
                    client.ReceiveAsync(args);

                    // 请求客服
                    this.Send(message);
                }
            }
            catch (Exception ex)
            {
                LogCommonHelper.WriteLog("Ws OnSocketConnectCompleted：" + ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 接受服务端发来的数据-回调处理
        /// </summary>
        private void OnSocketReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            // 只管发送，所以不需要接收
        }
        /// <summary>
        /// 发送完成后的回调函数
        /// </summary>
        private void SendArgs_Completed(object sender, SocketAsyncEventArgs e)
        {

        }
        #endregion

        #region ==== 方法 ====
        private byte[] MessageToBytes(SocketP2PMessageEntity message)
        {
            return Encoding.UTF8.GetBytes(JsonHelper.Serialize(message));
        }

        private SocketP2PMessageEntity InitEntity(byte[] buffer, int dataLength)
        {
            string strJson = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            SocketP2PMessageEntity entity = JsonHelper.Deserialize<SocketP2PMessageEntity>(strJson);
            return entity;
        }

        private void Send(SocketP2PMessageEntity message)
        {
            if (client != null && client.Connected)
            {
                try
                {
                    LogCommonHelper.WriteLog("Ws 开始发送到服务器的数据");

                    byte[] buffer = this.MessageToBytes(message);

                    List<ArraySegment<byte>> _lst = new List<ArraySegment<byte>>();
                    _lst.Add(new ArraySegment<byte>(buffer));
                    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                    args.RemoteEndPoint = client.RemoteEndPoint;
                    args.Completed += new EventHandler<SocketAsyncEventArgs>(SendArgs_Completed);
                    args.BufferList = _lst;
                    client.SendAsync(args);

                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    LogCommonHelper.WriteLog("Ws Sending Message：" + ex.InnerException.Message);
                }
            }
            else
            {
                LogCommonHelper.WriteLog("Ws 已与服务器断开链接...");
            }
        }
        #endregion
    }
}