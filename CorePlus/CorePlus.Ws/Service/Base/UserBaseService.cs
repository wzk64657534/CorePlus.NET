using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Entity;
using Core;
using System.Web.Services;

namespace CorePlus.Ws
{
    public class UserBaseService<TEntity> : BaseWebService<TEntity> where TEntity : UserEntity
    {
        [WebMethod]
        public virtual TEntity Login(string loginName, string loginPwd)
        {
            var db = CoreDBContext.GetContext();

            var query = (from x in db.Set<TEntity>()
                         where x.UserName == loginName
                          && x.UserPwd == loginPwd
                         select x).FirstOrDefault();

            return query;
        }
        [WebMethod]
        public virtual TEntity GetByUserName(string username)
        {
            var db = CoreDBContext.GetContext();

            var entity = (from x in db.Set<TEntity>()
                          where x.UserName == username
                          select x).FirstOrDefault();

            return entity;
        }
    }
}