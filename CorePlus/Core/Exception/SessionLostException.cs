using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class SessionLostException : Exception
    {
        public SessionLostException() :
            base("服务器会话丢失,将要跳转到登陆页面!")
        {

        }
    }
}
