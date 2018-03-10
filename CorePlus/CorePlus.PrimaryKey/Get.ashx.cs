using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using CorePlus.Entity;
using Memcached.ClientLibrary;

namespace CorePlus.PrimaryKey
{
    public class Get : IHttpHandler
    {
        static MemcachedClient client = new MemcachedClient();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string key = context.Request.Params["key"] ?? string.Empty;
                PrimaryKeyEntity entity = null;
                if (string.IsNullOrWhiteSpace(key))
                {
                    entity = new PrimaryKeyEntity();
                    entity.Type = 1;
                }
                else
                {
                    key = CryptHelper.DESDecode(key);
                    entity = JsonHelper.Deserialize<PrimaryKeyEntity>(key);
                }

                string id = string.Empty;

                lock (client)
                {
                    PrimaryKeyProvider provider = (PrimaryKeyProvider)client.Get("PrimaryKeyProvider");
                    id = provider.Get(entity);
                }

                context.Response.Write(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log4Net.Error(ex.Message);
                context.Response.Write(string.Empty);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}