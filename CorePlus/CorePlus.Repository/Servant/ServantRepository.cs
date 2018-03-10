using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class ServantRepository : SimpleRepository<ServantInfoEntity>
    {
        public override ServantInfoEntity NewEntity(object anyObj = null)
        {
            var entity = base.NewEntity(anyObj);
            entity.UserId = (long)anyObj;
            return entity;
        }

        protected override void BeforeAdd(ServantInfoEntity entity)
        {
            entity.ChnName = string.IsNullOrWhiteSpace(entity.ChnName) ? entity.UserName : entity.ChnName;
            entity.UserPwd = CryptHelper.MD5("123456");
            entity.RoleId = 3;
            entity.RoleName = "servant";
        }

        protected override void BeforeUpdate(ServantInfoEntity entity, ServantInfoEntity uiEntity)
        {
            base.BeforeUpdate(entity, uiEntity);
        }
    }
}