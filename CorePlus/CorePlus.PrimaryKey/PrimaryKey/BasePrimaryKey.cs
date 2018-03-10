using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;

namespace CorePlus.PrimaryKey
{
    [Serializable]
    public class BasePrimaryKey : IPrimaryKey
    {
        public virtual string Get(PrimaryKeyEntity entity)
        {
            return string.Empty;
        }
    }
}