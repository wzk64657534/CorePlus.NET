using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using CorePlus.Entity;

namespace CorePlus.Common
{
    public class EntityCommonHelper
    {
        public static string GetNameById<TEntity>(long? id)
            where TEntity : BaseEntity, INameEntity, new()
        {
            var db = CoreDBContext.GetContext();
            var query = (from x in db.Set<TEntity>()
                         where x.ID == id
                         select x.Name).FirstOrDefault();

            return query;
        }
    }
}