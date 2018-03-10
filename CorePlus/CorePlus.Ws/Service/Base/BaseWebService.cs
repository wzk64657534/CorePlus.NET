using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using Core;
using System.Web.Services;
using CorePlus.Ws;

namespace CorePlus.Ws
{
    public class BaseWebService<TEntity> : WebService, IBase<TEntity>
        where TEntity : BaseEntity
    {
        [WebMethod]
        public virtual long Add(TEntity entity)
        {
            var db = CoreDBContext.GetContext();

            using (TransactionScope scope = new TransactionScope())
            {
                db.Set<TEntity>().Add(entity);
                db.SaveChanges();
                scope.Complete();

                return entity.ID;
            }
        }

        [WebMethod]
        public virtual bool Update(TEntity entity)
        {
            var db = CoreDBContext.GetContext();

            using (TransactionScope scope = new TransactionScope())
            {
                db.Entry<TEntity>(entity).State = EntityState.Modified;
                db.SaveChanges();
                scope.Complete();
                return true;
            }
        }

        [WebMethod]
        public virtual bool Delete(TEntity entity)
        {
            var db = CoreDBContext.GetContext();

            using (TransactionScope scope = new TransactionScope())
            {
                db.Set<TEntity>().Remove(entity);
                db.SaveChanges();
                scope.Complete();
                return true;
            }
        }

        [WebMethod]
        public virtual bool DeleteById(long id)
        {
            var db = CoreDBContext.GetContext();

            using (TransactionScope scope = new TransactionScope())
            {
                db.Delete<TEntity>(x => x.ID == id);
                db.SaveChanges();
                scope.Complete();
                return true;
            }
        }

        [WebMethod]
        public virtual List<TEntity> GetAll()
        {
            var db = CoreDBContext.GetContext();
            var query = from x in db.Set<TEntity>()
                        select x;
            return query.ToList();

        }

        [WebMethod]
        public virtual TEntity GetById(long id)
        {
            var db = CoreDBContext.GetContext();
            var query = (from x in db.Set<TEntity>()
                         where x.ID == id
                         select x).FirstOrDefault();
            return query;

        }
    }
}