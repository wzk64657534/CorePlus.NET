using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;
using Memcached.ClientLibrary;

namespace CorePlus.PrimaryKey
{
    [Serializable]
    public class YmdIdentityPrimaryKey : BasePrimaryKey
    {
        public override string Get(PrimaryKeyEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Prefix)) { return string.Empty; }
            long i = 0;
            MemcachedClient client = new MemcachedClient();
            if (client.KeyExists(entity.Prefix))
            {
                i = (long)client.Get(entity.Prefix);
            }

            i = client.Increment(entity.Prefix, 1);

            string id = string.Format("{0}{1}{2}",
                entity.IsMustPrefix ? entity.Prefix : string.Empty,
                DateTime.Now.ToString("yyyyMMdd"),
                entity.IsMustFillWithChar ? i.ToString().PadLeft(entity.NumberLength, entity.FillChar) : i.ToString());

            return id;
        }
    }
}