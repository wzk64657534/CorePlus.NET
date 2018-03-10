using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using CorePlus.Entity;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net;
using System.Windows.Forms;
using CorePlus.Common;

namespace CorePlus.P2P.Server
{
    public class P2PServer
    {
        private static void OutPutSystemMessage(string message)
        {
            Console.WriteLine(message);
        }

        #region ==== 服务器部分变量 ====
        private static List<SocketP2PEntity> Queue = new List<SocketP2PEntity>();
        private static Thread tQueue;
        private static Thread tAutoCut;
        #endregion

        #region ==== 策略请求部分 ====
        private static Socket Policy; // 服务端监听的 socket        
        private static byte[] arrPolicyClient; // 收到访客端发送来的策略字符串请求 缓冲区
        private static int policyReceiveByteCount; // 接收到的信息字节数(用来辅助判断字符串最否接收完整)  
        private static byte[] arrPolicyServerFile; // 策略文件转化成的字节数组 
        private static Thread tPolicy;
        #endregion

        #region ==== 访客端部分变量 ====
        // 服务端监听的 Socket
        private static Socket Server;
        // 访客端 Socket 列表   
        private static List<SocketP2PEntity> Clients = new List<SocketP2PEntity>();

        private static int Port { get { return 4518; } }

        private static int QueueLength { get { return 100; } }

        private static int SendBufferSize { get { return ConstHelper.BUFFER_SIZE; } }

        private static int ReceiveBufferSize { get { return ConstHelper.BUFFER_SIZE; } }

        private static Thread tMain;
        #endregion

        #region ==== 策略请求943端口监听 ====
        /// <summary>
        /// 启动 PolicyServer
        /// </summary>
        static void StartPolicyListen()
        {
            OutPutSystemMessage("(943端口)策略请求监听开始...");
            string policyFile = Path.Combine(Application.StartupPath, "Policy.xml");
            arrPolicyServerFile = File.ReadAllBytes(policyFile);

            // 初始化 socket ， 然后与端口绑定， 然后对端口进行监听
            Policy = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Policy.Bind(new IPEndPoint(IPAddress.Any, 943)); // socket 请求策略文件使用 943 端口
            Policy.Listen(QueueLength);

            // 开始接受访客端传入的连接
            Policy.BeginAccept(new AsyncCallback(PolicyConnectComplate), null);
        }

        /// <summary>
        /// （循环调用）连接回调处理
        /// </summary>
        /// <param name="result"></param>
        static void PolicyConnectComplate(IAsyncResult result)
        {
            Socket sktClient; // 访客端发过来的 socket
            try
            {
                // 完成接受访客端传入的连接的这个异步操作，并返回访客端连入的 socket
                sktClient = Policy.EndAccept(result);
            }
            catch (SocketException)
            {
                return;
            }

            arrPolicyClient = new byte[ConstHelper.POLICY_STRING.Length];//设置策略请求接收缓冲区大小

            policyReceiveByteCount = 0;

            try
            {
                // 开始接收访客端传入的数据
                sktClient.BeginReceive(arrPolicyClient, 0, ConstHelper.POLICY_STRING.Length, SocketFlags.None, new AsyncCallback(PolicyReceiveComplte), sktClient);
            }
            catch (SocketException)
            {
                // socket 出错则关闭访客端 socket
                sktClient.Close();
            }

            // 继续开始接受访客端传入的连接
            Policy.BeginAccept(new AsyncCallback(PolicyConnectComplate), null);
        }

        /// <summary>
        /// 访客端策略字符串接收回调函数
        /// </summary>
        /// <param name="result"></param>
        static void PolicyReceiveComplte(IAsyncResult result)
        {
            Socket sktClient = result.AsyncState as Socket;

            try
            {
                // 完成接收数据的这个异步操作，并计算累计接收的数据的字节数
                policyReceiveByteCount += sktClient.EndReceive(result);

                if (policyReceiveByteCount < ConstHelper.POLICY_STRING.Length)
                {
                    // 没有接收到完整的数据，则继续开始接收(循环调用)
                    sktClient.BeginReceive(arrPolicyClient, policyReceiveByteCount, ConstHelper.POLICY_STRING.Length - policyReceiveByteCount, SocketFlags.None, new AsyncCallback(PolicyReceiveComplte), sktClient);
                    return;
                }

                // 把接收到的数据转换为字符串
                string _clientPolicyString = Encoding.UTF8.GetString(arrPolicyClient, 0, policyReceiveByteCount);
#if DEBUG
                OutPutSystemMessage("收到来自" + sktClient.RemoteEndPoint + " 的策略请求字符：" + _clientPolicyString);
#endif

                if (_clientPolicyString.ToLower().Trim() != ConstHelper.POLICY_STRING.ToLower().Trim())
                {
                    // 如果接收到的数据不是“<policy-file-request/>”，则关闭访客端 socket
                    sktClient.Close();
                    return;
                }

                // 开始向访客端发送策略文件的内容
                sktClient.BeginSend(arrPolicyServerFile, 0, arrPolicyServerFile.Length, SocketFlags.None, new AsyncCallback(PolicySendComplete), sktClient);
            }
            catch (SocketException)
            {
                // socket 出错则关闭访客端 socket
                sktClient.Close();
            }
        }

        /// <summary>
        /// 策划文件发送回调处理
        /// </summary>
        /// <param name="result"></param>
        static void PolicySendComplete(IAsyncResult result)
        {
            Socket sktClient = result.AsyncState as Socket;
            try
            {
                // 完成将信息发送到访客端的这个异步操作
                sktClient.EndSend(result);

#if DEBUG
                // 获取访客端的ip地址及端口号，并显示
                OutPutSystemMessage("已回发策略文件到 " + sktClient.RemoteEndPoint.ToString());
#endif
            }
            finally
            {
                // 关闭访客端 socket
                sktClient.Close();
            }
        }
        #endregion

        #region ==== 访客端4518端口监听 ====
        // 初始化Socket,对端口进行监听
        private static void Listen()
        {
            OutPutSystemMessage(string.Format("{0}端口的监听服务已经开始...", Port));

            Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Server.NoDelay = true;
            Server.SendBufferSize = SendBufferSize;
            Server.ReceiveBufferSize = ReceiveBufferSize;
            Server.Bind(new IPEndPoint(IPAddress.Any, 4518));
            Server.Listen(QueueLength);
            Server.BeginAccept(new AsyncCallback(ClientConnectComplete), null);
        }

        // 访客端连接完成的回调函数
        private static void ClientConnectComplete(IAsyncResult async)
        {
            SocketP2PEntity client = new SocketP2PEntity();
            client.Socket = Server.EndAccept(async);
            // 在各自项目中自行处理
            // clients.Add(client);

            // 开始接收访客端数据
            try
            {
                client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, new AsyncCallback(ClientReceiveComplete), client);
            }
            catch (SocketException ex)
            {
                ExecuteException(client, ex);
            }

            Server.BeginAccept(new AsyncCallback(ClientConnectComplete), null);
        }

        // 访客端数据接收的回调函数
        private static void ClientReceiveComplete(IAsyncResult async)
        {
            SocketP2PEntity client = async.AsyncState as SocketP2PEntity;
            int _receiveLength = 0;
            try
            {
                if (client.Socket.Connected)
                {
                    _receiveLength = client.Socket.EndReceive(async);
                    // 自定义处理
                    OperationSocketData(_receiveLength, client);
                }
                else
                {
                    client.Socket.Close();
                    Clients.Remove(client);
                }
            }
            catch (SocketException ex)
            {
                ExecuteException(client, ex);
            }

            try
            {
                if (client.Socket.Connected)
                {
                    client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, 0, new AsyncCallback(ClientReceiveComplete), client);
                }
                else
                {
                    client.Socket.Close();
                    Clients.Remove(client);
                }
            }
            catch (SocketException ex)
            {
                ExecuteException(client, ex);
            }
        }

        // 结束发送
        private static void SendToClientComplete(IAsyncResult async)
        {
            SocketP2PEntity client = async.AsyncState as SocketP2PEntity;

            try
            {
                // 完成将信息发送到访客端的这个异步操作
                if (client.Socket.Connected)
                {
                    client.Socket.EndSend(async);
                }
            }
            catch (SocketException ex)
            {
                ExecuteException(client, ex);
            }
        }
        #endregion

        // 处理异常
        private static void ExecuteException(SocketP2PEntity client, SocketException ex)
        {
            // 在服务端记录异常信息，关闭导致异常的 Socket，并将其清除出访客端 Socket 列表           
            OutPutSystemMessage(string.Format("异常：ID[{0}],IP[{1}],Msg[{2}]", client.ID, client.Socket.RemoteEndPoint.ToString(), ex.Message));
            client.Socket.Close();
            Clients.Remove(client);
            Thread.Sleep(100);
        }

        // 服务器关闭
        private static void ServerShutDown()
        {
            foreach (var client in Clients)
            {
                try
                {
                    if (client.Socket.Connected)
                    {
                        byte[] data = GetWillSendDataByServer(client, "服务器关闭，已经断开连接...");
                        client.Socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendToClientComplete), client);
                    }
                }
                catch (SocketException ex)
                {
                    ExecuteException(client, ex);
                }
                finally
                {
                    client.Socket.Close();

                }
            }
        }

        private static byte[] GetWillSendDataByServer(SocketP2PEntity client, string data)
        {
            SocketP2PMessageEntity message = new SocketP2PMessageEntity();
            message.Sender = ConstHelper.SERVER_ID;
            message.Receiver = ConstHelper.SERVER_ID;
            message.DialogId = ConstHelper.DIALOG_NO;
            message.Identity = "SERVER";
            message.Data = data;

            string buffer = JsonHelper.Serialize(message);

            return Encoding.Default.GetBytes(buffer);
        }

        private static void OperationSocketData(int receiveLength, SocketP2PEntity client)
        {
            try
            {
                SendToClientCompleteHandler sendToClientCompleteHandler = new SendToClientCompleteHandler(SendToClientComplete);
                OutPutSystemMessageHandler outPutSystemMessageHandler = new OutPutSystemMessageHandler(OutPutSystemMessage);

                string json = Encoding.UTF8.GetString(client.Buffer, 0, receiveLength);

                if (string.IsNullOrEmpty(json))
                {
                    LogCommonHelper.WriteLog("p2p服务接收到数据：Null");
                }
                else
                {
                    LogCommonHelper.WriteLog("p2p服务接收到数据：" + json);

                    SocketP2PMessageEntity message = JsonHelper.Deserialize<SocketP2PMessageEntity>(json);

                    ManagerIdentity.Execute(client, message, Clients, Queue, sendToClientCompleteHandler, outPutSystemMessageHandler);
                }
            }
            catch (Exception ex)
            {
                LogCommonHelper.WriteLog("OperationSocketData：" + ex.InnerException.Message);
            }
        }


        #region ==== 队列，5分钟轮询一次 ====
        private static void QueueListen()
        {
            while (true)
            {
                for (int i = 0; i < Queue.Count; i++)
                {
                    try
                    {
                        var customer = Queue[i];

                        SocketP2PMessageEntity message = new SocketP2PMessageEntity();
                        message.Sender = customer.Sender;
                        message.Identity = customer.Identity;
                        message.Owner = customer.Owner;
                        message.WeiXinNo = customer.WeiXinNo;
                        message.OpenId = customer.OpenId;
                        message.Data = "CHECKIDENTITY";

                        IIdentity id = new CustomerIdentity();
                        id.Comunicate(customer, message, Clients, Queue, SendToClientComplete, OutPutSystemMessage);
                    }
                    catch
                    {
                        break;
                    }
                }

                Thread.Sleep(1000 * 60 * 5);
            }
        }
        #endregion

        #region ==== 自动断线，3分钟未发言 ====
        private static void AutoCutLine()
        {
            while (true)
            {
                try
                {
                    var clients = from x in Clients
                                  where x.Identity == "CUSTOMER"
                                  && x.LastTalkTime != null
                                  && (DateTime.Now - (x.LastTalkTime ?? DateTime.Now)).TotalSeconds > (60 * 3)
                                  && !(from z in Queue
                                       where z.Sender == x.Sender
                                       select z).Any()
                                  select x;

                    foreach (var client in clients)
                    {
                        // 通知客服
                        var servant = Clients.Where(x => x.Sender == client.Receiver).FirstOrDefault();
                        if (servant != null)
                        {
                            try
                            {
                                SocketP2PMessageEntity message = new SocketP2PMessageEntity();
                                message.Sender = null;
                                message.Receiver = servant.Sender;
                                message.Identity = "SERVER";
                                message.Data = string.Format("访客({0})超过3分钟未发言，系统自动断线，请等待其他访客的服务请求", servant.Receiver);
                                message.Owner = servant.Owner;
                                message.WeiXinNo = servant.WeiXinNo;
                                message.OpenId = servant.OpenId;

                                servant.Receiver = null;

                                ServantIdentity identity = new ServantIdentity();
                                identity.Comunicate(servant, message, Clients, Queue, SendToClientComplete, OutPutSystemMessage);
                            }
                            catch (SocketException ex)
                            {
                                servant.Socket.Close();
                                Clients.Remove(servant);
                                OutPutSystemMessage(ex.Message);
                            }
                        }

                        Thread.Sleep(500);
                        // 通知访客
                        try
                        {
                            SocketP2PMessageEntity message = new SocketP2PMessageEntity();
                            message.Sender = null;
                            message.Receiver = client.Sender;
                            message.Identity = "SERVER";
                            message.Data = "您有3分钟未发言，自动为您退出";
                            message.Owner = client.Owner;
                            message.WeiXinNo = client.WeiXinNo;
                            message.OpenId = client.OpenId;
                            CustomerIdentity identity = new CustomerIdentity();
                            identity.Comunicate(client, message, Clients, Queue, SendToClientComplete, OutPutSystemMessage);
                        }
                        catch (SocketException ex)
                        {
                            OutPutSystemMessage(ex.Message);
                        }
                        finally
                        {
                            client.Socket.Close();
                            Clients.Remove(client);
                        }
                    }
                }
                catch
                {

                }

                Thread.Sleep(500);
            }
        }
        #endregion

        public static void Run()
        {
            tPolicy = new Thread(StartPolicyListen);
            tPolicy.Start();

            tMain = new Thread(Listen);
            tMain.Start();

            tQueue = new Thread(QueueListen);
            tQueue.Start();

            tAutoCut = new Thread(AutoCutLine);
            tAutoCut.Start();
        }

        public static void Stop()
        {
            if (tPolicy != null)
            {
                tPolicy.Abort();
                OutPutSystemMessage("943端口的Socket监听服务结束...");
            }

            if (tMain != null)
            {
                tMain.Abort();
                ServerShutDown();
                Server.Close();

                OutPutSystemMessage(string.Format("{0}端口的Socket监听服务结束...", Port));
            }

            if (tQueue != null)
            {
                tQueue.Abort();
                OutPutSystemMessage("队列服务结束...");
            }

            if (tAutoCut != null)
            {
                tAutoCut.Abort();
                OutPutSystemMessage("自动清理服务结束...");
            }
        }
    }
}