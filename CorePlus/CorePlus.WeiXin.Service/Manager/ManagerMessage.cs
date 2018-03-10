using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class ManagerMessage
    {
        Dictionary<string, IMessage> manager = null;

        public ManagerMessage()
        {
            manager = new Dictionary<string, IMessage>();
            manager.Add("image", new ImageMessage());
            manager.Add("link", new LinkMessage());
            manager.Add("location", new LocationMessage());
            manager.Add("video", new VideoMessage());
            manager.Add("text", new TextMessage());
            manager.Add("voice", new VoiceMessage());

            manager.Add("eventsubscribe", new SubscribeEventMessage());
            manager.Add("eventunsubscribe", new SubscribeEventMessage());
            manager.Add("eventsubscribeqrscene", new ScanEventMessage());
            manager.Add("eventscan", new ScanEventMessage());
            manager.Add("eventlocation", new LocationEventMessage());
            manager.Add("eventclick", new MenuEventMessage());

            manager.Add("voicerecognition", new RecognitionMessage());
        }

        public static ManagerMessage CreateInstance()
        {
            return new ManagerMessage();
        }

        public string Save(string key, string postStr, string signature, string timestamp, string nonce)
        {
            key = key.ToLower();
            if (manager.ContainsKey(key))
            {
                return manager[key].Save(postStr, signature, timestamp, nonce);
            }

            return string.Empty;
        }
    }
}