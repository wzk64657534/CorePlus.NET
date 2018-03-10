using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;

namespace CorePlus.PrimaryKey
{
    [Serializable]
    public class YmdhmsfPrimaryKey : BasePrimaryKey
    {
        public override string Get(PrimaryKeyEntity entity)
        {
            return string.Format("{0}{1}",
                               entity.IsMustPrefix ? entity.Prefix : string.Empty,
                               DateTime.Now.ToString("yyyyMMddHHmmssfff"));
        }
    }
}