using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class DealWithManager
    {
        Dictionary<int, IDealWithType> manager = null;
        public DealWithManager()
        {
            manager = new Dictionary<int, IDealWithType>();
            manager.Add(1, new DealWithAll());
            manager.Add(2, new DealWithChange());
            manager.Add(3, new DealWithStatistics());
        }

        public void CheckedData(SynCheckedDataInfoEntity entity, SynDataInfoEntity model)
        {
            if (manager.ContainsKey(model.DealWithType ?? 0))
            {
                manager[model.DealWithType ?? 0].CheckedData(entity, model);
            }
        }

        public void SynData(SynCheckedDataInfoEntity entity)
        {
            if (manager.ContainsKey(entity.DealWithType ?? 0))
            {
                manager[entity.DealWithType ?? 0].SynData(entity);
            }
        }

        public void GetDealWithId(DateTime dt, SynDataInfoEntity entity)
        {
            if (manager.ContainsKey(entity.DealWithType ?? 0))
            {
                manager[entity.DealWithType ?? 0].GetDealWithId(dt, entity);
            }
        }
    }
}