using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Transactions;
using System.Data.Entity.Validation;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Core
{
    public class BaseRepository
    {
        #region DbContext
        private CoreDBContext dbContext;
        public BaseRepository()
        {

        }

        protected CoreDBContext DB
        {
            get
            {
                if (this.dbContext == null)
                {
                    var scope = DbContextScope.Current;
                    if (scope != null)
                        this.dbContext = scope.Context;
                    else
                        this.dbContext = CoreDBContext.GetContext();
                }

                return this.dbContext;
            }
        }
        #endregion

        #region Save
        public virtual void Save()
        {
            try
            {
                this.DB.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var item in dbEx.EntityValidationErrors)
                {
                    foreach (var error in item.ValidationErrors)
                    {
                        throw new WarningException(error.ErrorMessage);
                    }
                }
            }
        }
        #endregion
    }

    public class BaseRepository<TEntity> : BaseRepository, ICoreRepositoty
        where TEntity : BaseEntity
    {
        #region DbSet
        public DbSet<TEntity> DbSet
        {
            get { return DB.Set<TEntity>(); }
        }

        protected virtual IQueryable<TEntity> GetQueryable()
        {
            return DbSet;
        }

        public virtual List<TEntity> FindAll()
        {
            return DbSet.ToList();
        }
        #endregion

        #region FindByID

        protected virtual IQueryable<TEntity> FindByIDExpression()
        {
            return this.DbSet;
        }

        public virtual TEntity FindByID(long id)
        {
            var entity = FindByIDExpression().Where(x => x.ID == id).FirstOrDefault();
            return entity;
        }

        public virtual IQueryable<TEntity> FindByExpression(Expression<Func<TEntity, bool>> expression)
        {
            var query = FindByIDExpression().Where(expression);
            return query;
        }

        #endregion

        #region GetPagerData
        public virtual List<TEntity> GetByPager(IQueryable<TEntity> query, int currentPageIndex, int pageSize)
        {
            int totalPageCnt = 0;
            int recordCount = 0;

            return GetByPager(query, currentPageIndex, pageSize, out totalPageCnt, out recordCount);
        }

        public virtual List<TEntity> GetByPager(IQueryable<TEntity> query, int currentPageIndex, int pageSize, out int totalPageCnt, out int recordCount)
        {
            recordCount = query.Count();

            totalPageCnt = (recordCount % pageSize) > 0 ? ((recordCount / pageSize) + 1) : (recordCount / pageSize);

            query = query.Skip((currentPageIndex - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }

        public virtual List<TEntity> GetByPager(int currentPageIndex, int pageSize, out int totalPageCnt, out int recordCount)
        {
            recordCount = this.DbSet.Count();

            totalPageCnt = (recordCount % pageSize) > 0 ? ((recordCount / pageSize) + 1) : (recordCount / pageSize);

            var query = (from x in this.DbSet
                         orderby x.ID descending
                         select x).Skip((currentPageIndex - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }

        /// <summary>
        /// 通过Sql语句查询分页数据
        /// </summary>
        /// <param name="sql">分页Sql数组，第1个为分页数据，第2个为总记录数，不可乱序</param>
        public virtual List<TEntity> GetByPagerQuery(string[] sqls, out int records)
        {
            List<TEntity> entities = DB.Database.SqlQuery<TEntity>(CryptHelper.DESDecode(sqls[0])).ToList();

            int? count = DB.Database.SqlQuery<int>(CryptHelper.DESDecode(sqls[1])).FirstOrDefault();

            records = count ?? 0;

            return entities;
        }
        #endregion

        #region Exists
        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            if (expression != null)
                return GetQueryable().Any(expression);
            return GetQueryable().Any();
        }

        public bool Exists(IEnumerable<Tuple<string, ExpressionOperator, object>> expressions)
        {
            var type = typeof(TEntity);
            var dataSource = (this.DbSet as IQueryable<TEntity>);

            foreach (var item in expressions)
            {
                var parameter = Expression.Parameter(type, "x");
                Expression left = Expression.Property(parameter, item.Item1);
                Expression right = Expression.Constant(item.Item3, left.Type);
                Expression expression = null;
                switch (item.Item2)
                {
                    case ExpressionOperator.Equal: expression = Expression.Equal(left, right); break;
                    case ExpressionOperator.NotEqual: expression = Expression.NotEqual(left, right); break;
                }
                expression = Expression.Lambda(expression, parameter);
                dataSource = dataSource.Where(expression as Expression<Func<TEntity, bool>>);
            }

            return dataSource.Any();
        }
        #endregion

        #region Count
        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            if (expression != null)
                return DbSet.Count(expression);
            return DbSet.Count();
        }

        public int Count(IEnumerable<Tuple<string, ExpressionOperator, object>> expressions)
        {
            var type = typeof(TEntity);
            var dataSource = (this.DbSet as IQueryable<TEntity>);

            List<Expression> list = new List<Expression>();
            list.Add(dataSource.Expression);
            foreach (var item in expressions)
            {
                var property = type.GetProperty(item.Item1);
                var parameter = Expression.Parameter(type, "x");
                var expressionLeft = Expression.MakeMemberAccess(parameter, property);
                var expressionRight = Expression.Constant(item.Item3);
                Expression expression = null;
                switch (item.Item2)
                {
                    case ExpressionOperator.Equal: expression = Expression.Equal(expressionLeft, expressionRight); break;
                    case ExpressionOperator.NotEqual: expression = Expression.NotEqual(expressionLeft, expressionRight); break;
                }
                if (expression != null)
                {
                    list.Add(Expression.Quote(Expression.Lambda(expression, parameter)));
                }
            }

            return (int)dataSource.Provider.Execute(
                Expression.Call(
                    typeof(Queryable), "Count",
                    new Type[] { dataSource.ElementType }, list.ToArray()));
        }
        #endregion

        #region Check
        protected virtual void CheckNotNull(object entity)
        {
            if (entity == null)
            {
                throw new WarningException("操作的记录未找到!");
            }
        }
        #endregion
    }
}