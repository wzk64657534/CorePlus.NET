using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;

namespace CorePlus.PrimaryKey
{
    [Serializable]
    public class PrimaryKeyProvider
    {
        Dictionary<int, IPrimaryKey> provider = null;

        public PrimaryKeyProvider()
        {
            provider = new Dictionary<int, IPrimaryKey>();
            provider.Add(1, new GuidMaryKey());
            provider.Add(2, new YmdhmsfPrimaryKey());
            provider.Add(3, new IdentityPrimaryKey());
        }

        public string Get(PrimaryKeyEntity entity)
        {
            if (provider.ContainsKey(entity.Type))
            {
                return provider[entity.Type].Get(entity);
            }

            return string.Empty;
        }
    }
}