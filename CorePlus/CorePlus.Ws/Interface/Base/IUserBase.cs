using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CorePlus.Entity;
using Core;
using Core.Entity;

namespace CorePlus.Ws
{
    public interface IUserBase<TEntity> : IBase<TEntity>
        where TEntity : UserEntity
    {
        TEntity Login(string loginName, string loginPwd);
        TEntity GetByUserName(string userName);
    }
}