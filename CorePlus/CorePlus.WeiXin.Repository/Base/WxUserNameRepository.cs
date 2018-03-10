using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.WeiXin.Entity;
using Core;

namespace CorePlus.WeiXin.Repository
{
    public class WxUserNameRepository<TEntity> : SimpleRepository<TEntity>
         where TEntity : WxUserEntity, new()
    {
        public override TEntity NewEntity(object anyObj = null)
        {
            var entity = base.NewEntity(anyObj);
            entity.UserName = anyObj.ToString();
            return entity;
        }
    }
}