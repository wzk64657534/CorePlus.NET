using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.Entity;
using Core;

namespace CorePlus.P2P.Server
{
    public class MessageHelper
    {
        public static byte[] SetWillSendData(string sender, string reciever, string msgType, string dialogId, string data)
        {
            SocketP2PMessageEntity message = new SocketP2PMessageEntity();
            message.Sender = sender;
            message.Receiver = reciever;
            message.MsgType = msgType;
            message.DialogId = dialogId;
            message.Data = data;

            string json = JsonHelper.Serialize(message);
            return Encoding.UTF8.GetBytes(json);
        }

        public static byte[] SetWillSendData(string sender, string reciever, string data)
        {
            return SetWillSendData(sender, reciever, "00001", "00000000-0000-0000-0000-000000000000", data);
        }

        public static byte[] SetWillSendData(string sender, string reciever, string dialogId, string data)
        {
            return SetWillSendData(sender, reciever, "00001", dialogId, data);
        }

        public static string SetMessage(string sender, string reciever, string msgType, string dialogId, string data)
        {
            SocketP2PMessageEntity message = new SocketP2PMessageEntity();
            message.Sender = sender;
            message.Receiver = reciever;
            message.MsgType = msgType;
            message.DialogId = dialogId;
            message.Data = data;

            string json = JsonHelper.Serialize(message);
            return json;
        }

        public static string SetMessage(string sender, string reciever, string data)
        {
            return SetMessage(sender, reciever, "00001", "00000000-0000-0000-0000-000000000000", data);
        }

        public static string SetMessage(string sender, string reciever, string dialogId, string data)
        {
            return SetMessage(sender, reciever, "00001", dialogId, data);
        }

        public static string SetMessage(SocketP2PMessageEntity entity, string data)
        {
            SocketP2PMessageEntity message = new SocketP2PMessageEntity();
            message.Sender = entity.Sender;
            message.Receiver = entity.Receiver;
            message.MsgType = entity.MsgType;
            message.DialogId = entity.DialogId;
            message.Data = data;
            message.Owner = entity.Owner;

            string json = JsonHelper.Serialize(message);
            return json;
        }
    }
}