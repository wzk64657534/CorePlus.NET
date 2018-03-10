using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class ApiRepository<TEntity> : SimpleRepository<TEntity>
        where TEntity : MaterialEntity, new()
    {
        public override TEntity NewEntity(object anyObj = null)
        {
            var entity = base.NewEntity(anyObj);
            entity.AccountName = anyObj.ToString();
            return entity;
        }

        protected override void BeforeAdd(TEntity entity)
        {
            entity.ID = DateTimeHelper.ConvertDateTimeInt(DateTime.Now);
            base.BeforeAdd(entity);
        }
    }
}
