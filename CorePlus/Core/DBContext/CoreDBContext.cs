using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Infrastructure;
using System.Web;
using System;
using Spring.Context;
using Spring.Context.Support;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;

namespace Core
{
    public class CoreDBContext : DbContext
    {
        public CoreDBContext()
            : base("CoreDBContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public CoreDBContext(string nameOrConnectionString = null)
            : base(nameOrConnectionString ?? "CoreDBContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public IList<IMapping> Mappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            if (Mappings != null)
            {
                foreach (var mapping in Mappings)
                {
                    mapping.RegistTo(modelBuilder.Configurations);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public static CoreDBContext GetContext()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            return ctx.GetObject("dbContext") as CoreDBContext;
        }

        public int UpdateBatch<TEntity>(Expression<Func<TEntity, TEntity>> evaluator, Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            string tableName = AttributesHelper.GetTableName<TEntity>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            int memberInitCount = 1;
            string setString = null;
            evaluator.Visit<MemberInitExpression>(delegate(MemberInitExpression expression)
            {
                if (memberInitCount > 1)
                {
                    throw new NotImplementedException("Currently only one MemberInitExpression is allowed for the evaluator parameter.");
                }
                memberInitCount++;
                setString = GetDbSetStatement<TEntity>(expression, parameters);
                return expression;
            });
            string whereString = null;
            if (filter != null)
            {
                whereString = GetBatchConditionQuery(filter, parameters);
            }
            string sql = string.Format("UPDATE {0}\r\n{1}\r\n\r\n{2}", tableName, setString, whereString);
            return this.Database.ExecuteSqlCommand(sql, parameters.ToArray());
        }

        /// <summary>
        /// 获得Set表达式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="memberInitExpression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string GetDbSetStatement<TEntity>(MemberInitExpression memberInitExpression, List<SqlParameter> parameters)
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            var sb = new StringBuilder();
            foreach (var binding in memberInitExpression.Bindings)
            {
                var assignment = binding as MemberAssignment;
                var constant = Expression.Lambda(assignment.Expression, null).Compile().DynamicInvoke();
                string name = binding.Member.Name;
                if (constant == null)
                {
                    sb.AppendFormat("[{0}] = null, ", name);
                }
                else
                {
                    sb.AppendFormat("[{0}] = @p{1}, ", name, parameters.Count);
                    parameters.Add(new SqlParameter(string.Format("@p{0}", parameters.Count), constant));
                }
            }
            var setStatements = sb.ToString();
            return "SET " + setStatements.Substring(0, setStatements.Length - 2); // remove ', '
        }

        /// <summary>
        /// 获得Where表达式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string GetBatchConditionQuery<TEntity>(Expression<Func<TEntity, bool>> predicate, List<SqlParameter> parameters)
            where TEntity : class
        {
            ConditionBuilder conditionBuilder = new ConditionBuilder();
            conditionBuilder.Build(predicate.Body);
            if (!string.IsNullOrEmpty(conditionBuilder.Condition))
            {
                string condition = conditionBuilder.Condition;
                for (int i = 0; i < conditionBuilder.Arguments.Length; i++)
                {
                    condition = condition.Replace(string.Format("{{{0}}}", i), string.Format("@p{0}", parameters.Count));
                    parameters.Add(new SqlParameter(string.Format("@p{0}", parameters.Count), conditionBuilder.Arguments[i]));
                }
                return "Where " + condition;
            }
            return null;
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            string tableName = AttributesHelper.GetTableName<T>();
            if (!string.IsNullOrEmpty(tableName))
            {
                string command = String.Format("DELETE FROM {0}", tableName);
                ConditionBuilder conditionBuilder = new ConditionBuilder();
                conditionBuilder.Build(predicate.Body);
                if (!String.IsNullOrEmpty(conditionBuilder.Condition))
                {
                    command += " WHERE " + conditionBuilder.Condition;
                }

                return this.Database.ExecuteSqlCommand(command, conditionBuilder.Arguments);
            }

            return 0;
        }
    }
}