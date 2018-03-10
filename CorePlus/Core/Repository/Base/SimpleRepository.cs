using System;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using System.Linq.Expressions;
using System.Data;

namespace Core
{
    /// <summary>
    /// 处理最简单的增删改查
    /// </summary>
    public class SimpleRepository<TEntity> : BaseRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        #region NewEntity
        public virtual TEntity NewEntity(object anyObj = null)
        {
            var entity = new TEntity();
            return entity;
        }
        #endregion

        #region Add
        public virtual void Add(TEntity entity)
        {
            BeforeAdd(entity);
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    OnAdd(entity);
                    trans.Complete();
                }
                catch (Exception ex)
                {
                    ex = ExceptionHelper.GetInnerException(ex);
                    throw new WarningException(entity, "添加失败,原因:{0}", ex.Message);
                }
            }
            AfterAdd(entity);
        }

        protected virtual void BeforeAdd(TEntity entity)
        {

        }

        public virtual void OnAdd(TEntity entity)
        {
            // 新增的时候，刷新实体的自动生成字段值方法，但是这个方法是审核未完成的 所以Type = 0
            // CodeRuleManager.Fill(entity);
            this.DbSet.Add(entity);
            this.Save();
        }

        protected virtual void AfterAdd(TEntity entity)
        {

        }
        #endregion

        #region Update
        /// <summary>
        /// 根据界面上的entity 覆盖数据库中的entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uiEntity"></param>
        /// <returns></returns>
        public virtual TEntity Update(long id, TEntity uiEntity)
        {
            var entity = FindByID(id);
            CheckNotNull(entity);
            CheckNotNull(uiEntity);
            Update(entity, uiEntity);
            return entity;
        }

        protected virtual void UpdateModel(TEntity entity, TEntity uiEntity)
        {
            if (uiEntity != null)
            {
                entity.LoadData(uiEntity);
            }
        }

        public virtual void Update(TEntity entity, TEntity uiEntity = null)
        {
            BeforeUpdate(entity, uiEntity);
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    UpdateModel(entity, uiEntity);
                    OnUpdate(entity);
                    trans.Complete();
                }
                catch (Exception ex)
                {
                    ex = ExceptionHelper.GetInnerException(ex);
                    throw new WarningException(entity, "更新失败,原因:{0}", ex.Message);
                }
            }
            AfterUpdate(entity);
        }

        protected virtual void BeforeUpdate(TEntity entity, TEntity uiEntity)
        {

        }

        protected virtual void OnUpdate(TEntity entity)
        {
            this.Save();
        }

        protected virtual void AfterUpdate(TEntity entity)
        {

        }

        public int UpdateMany(Expression<Func<TEntity, TEntity>> evaluator, Expression<Func<TEntity, bool>> filter = null)
        {
            return this.DB.UpdateBatch(evaluator, filter);
        }
        #endregion

        #region Delete
        public virtual void Delete(long id)
        {
            var entity = FindByID(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(string[] ids)
        {
            string tableName = AttributesHelper.GetTableName<TEntity>();
            if (!string.IsNullOrEmpty(tableName))
            {
                string command = string.Format("DELETE FROM {0}", tableName);
                if (ids.Length > 0)
                {
                    string ary = string.Empty;
                    foreach (var item in ids)
                    {
                        ary += item + ",";
                    }
                    ary = ary.Trim(new char[] { ',' });
                    command += string.Format(" WHERE ID IN({0})", ary);
                }

                this.DB.Database.ExecuteSqlCommand(command, new string[0]);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            BeforeDelete(entity);
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    OnDelete(entity);
                    trans.Complete();
                }
                catch (Exception ex)
                {
                    ex = ExceptionHelper.GetInnerException(ex);
                    throw new WarningException(entity, "删除失败,原因:{0}", ex.Message);
                }
            }
            AfterDelete(entity);
        }

        protected virtual void BeforeDelete(TEntity entity)
        {

        }

        protected virtual void OnDelete(TEntity entity)
        {
            this.DbSet.Remove(entity);
            this.Save();
        }

        protected virtual void AfterDelete(TEntity entity)
        {

        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            this.DB.Delete(predicate);
        }
        #endregion

        #region 批量保存
        public void SaveBatch(List<TEntity> saveData, long[] removedData)
        {
            this.BeforeSaveBatch(ref saveData, ref removedData);

            using (TransactionScope scope = new TransactionScope())
            {
                var table = this.DB.Set<TEntity>();
                if (removedData != null)
                {
                    var removeList = table.Where(x => removedData.Contains(x.ID)).ToList();
                    foreach (var item in removeList)
                    {
                        table.Remove(item);
                    }
                }

                if (saveData != null)
                {
                    foreach (var item in saveData)
                    {
                        this.InitBatchSaveEntity(item);
                        if (item.ID > 0)
                        {
                            var entry = this.DB.Entry(item);
                            if (entry.State == EntityState.Detached)
                                entry.State = EntityState.Modified;
                        }
                        else
                            table.Add(item);
                    }
                }
                this.Save();
                scope.Complete();
            }
        }

        protected virtual void InitBatchSaveEntity(TEntity item)
        {

        }

        protected virtual void BeforeSaveBatch(ref List<TEntity> saveData, ref long[] removedData)
        {

        }
        #endregion
    }
}