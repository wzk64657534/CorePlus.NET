using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace Core
{
    public class SimpleDetailRepository<TEntity, TDetail> : SimpleRepository<TEntity>
        where TEntity : SimpleDetailEntity<TDetail>, new()
        where TDetail : DetailEntity
    {
        public DbSet<TDetail> DbSetDetail { get { return this.DB.Set<TDetail>(); } }

        protected override void BeforeAdd(TEntity entity)
        {
            base.BeforeAdd(entity);
            if (entity.Details != null)
            {
                int seq = 1;
                foreach (var item in entity.Details)
                {
                    item.SEQ = seq++;
                }
            }
        }

        protected override void BeforeUpdate(TEntity entity, TEntity uiEntity)
        {
            base.BeforeUpdate(entity, uiEntity);
            if (uiEntity.Details != null)
            {
                int seq = 0;
                foreach (var item in uiEntity.Details)
                {
                    if (item.SEQ > seq && item.SEQ < 1000)
                        seq = item.SEQ;
                }

                foreach (var item in uiEntity.Details)
                {
                    if (item.SEQ == 0 || item.SEQ >= 1000)
                        item.SEQ = ++seq;
                }
            }
        }

        protected override IQueryable<TEntity> FindByIDExpression()
        {
            return base.FindByIDExpression().Include("Details");
        }

        public IEnumerable<TDetail> GetDetail(Nullable<long> id)
        {
            return this.DbSetDetail.Where(x => x.ID == id);
        }
    }
}
