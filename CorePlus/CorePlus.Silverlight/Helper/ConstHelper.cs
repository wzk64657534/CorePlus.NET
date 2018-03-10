using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Silverlight
{
    public class ConstHelper
    {
        #region ==== Regex ====
        public const string RegexEmail = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
        public const string RegexUserName = @"^[a-zA-Z0-9_]+$";
        public const string RegexPassword = @"[-\da-zA-Z`=\\\[\];',./~!@#$%^&*()_+|{}:<>?]*";
        #endregion

        #region ==== Socket ====
        public const int BUFFER_SIZE = 1024 * 500;
        public const char SOCKET_SPLIT_CHAR = '^';
        public const string BROADCAST_ENDPOINT = "0.0.0.0";
        public const string BROADCAST_NAME = "所有人";
        public const string SERVER_ID = "0000000000";
        public const string DEFAULT_IP = "127.0.0.1";
        public const string DEFAULT_DIALOGNO = "00000000-0000-0000-0000-000000000000";

        public const string ConnectedWithServer = "与服务器连接成功...";
        public const string ConnectedBreak = "与服务器断开连接...";
        public const string NonConnectedWithServer = "无法连接到服务器，请刷新后再试...";
        #endregion
    }
}