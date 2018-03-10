using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class ConstHelper
    {
        #region ==== Regex ====
        public const string RegexEmail = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
        public const string RegexUserName = @"^[a-zA-Z0-9_]+$";
        public const string RegexPassword = @"[-\da-zA-Z`=\\\[\];',./~!@#$%^&*()_+|{}:<>?]*";
        public const string RegexUrl = @"^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)?((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.[a-zA-Z]{2,4})(\:[0-9]+)?(/[^/][a-zA-Z0-9\.\,\?\'\\/\+&amp;%\$#\=~_\-@]*)*$";
        public const string RegexJson = "^{(\"[a-zA-Z0-9_]+\":\"[^,]*\",*)+}$"; // "\".+?\":\"[^\"]+?\"";
        public const string RegexHtml = @"^<(.*)>.*<\/\1>$";
        #endregion

        #region ==== Socket ====
        public const int BUFFER_SIZE = 1024 * 500;
        public const char SOCKET_SPLIT_CHAR = '^';
        public const string BROADCAST_ENDPOINT = "0.0.0.0";
        public const string BROADCAST_NAME = "所有人";
        public const string SERVER_ID = "0000000000";
        public const string POLICY_STRING = "<policy-file-request/>";
        public const string DIALOG_NO = "00000000-0000-0000-0000-000000000000";
        #endregion
    }
}