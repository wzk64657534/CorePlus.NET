using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Memcached.ClientLibrary;
using CorePlus.Entity;

namespace CorePlus.PrimaryKey
{
    [Serializable]
    public class IdentityPrimaryKey : BasePrimaryKey
    {
        public override string Get(PrimaryKeyEntity entity)
        {
            MemcachedClient client = new MemcachedClient();
            if (client.KeyExists(entity.Prefix))
            {
                long id = client.Increment(entity.Prefix, 1);
                return string.Format("{0}{1}",
                    entity.IsMustPrefix ? entity.Prefix : string.Empty,
                    entity.IsMustFillWithChar ? id.ToString().PadLeft(entity.NumberLength, entity.FillChar) : id.ToString());
            }
            else
            {
                return string.Empty;
            }
        }
    }
}