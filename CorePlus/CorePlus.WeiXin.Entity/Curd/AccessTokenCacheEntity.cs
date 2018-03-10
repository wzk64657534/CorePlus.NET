using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.WeiXin.Entity
{
    public class AccessTokenCacheEntity
    {
        public string AccessToken { get; set; }
        public DateTime RequstTime { get; set; }
    }
}
