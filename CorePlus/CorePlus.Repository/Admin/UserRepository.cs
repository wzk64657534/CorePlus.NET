using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class UserRepository : SimpleRepository<UserInfoEntity>
    {
        protected override void BeforeAdd(UserInfoEntity entity)
        {
            entity.UserPwd = CryptHelper.MD5("123456");
            entity.RoleId = 2;
            entity.RoleName = "customer";
        }
    }
}