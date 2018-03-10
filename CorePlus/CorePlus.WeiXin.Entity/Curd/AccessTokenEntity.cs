using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.WeiXin.Entity
{
    public class AccessTokenEntity
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}