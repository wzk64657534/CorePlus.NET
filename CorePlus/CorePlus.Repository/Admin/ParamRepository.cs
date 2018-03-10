using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;
using System.Data;

namespace CorePlus.Repository
{
    public class ParamRepository : SimpleDetailRepository<ParamInfoEntity, ParamDtsInfoEntity>
    {
        public void DeleteDts(long id, int seq)
        {
            var db = CoreDBContext.GetContext();
            var entity = from x in db.Set<ParamDtsInfoEntity>()
                         where x.ID == id && x.SEQ == seq
                         select x;

            if (entity.Any())
            {
                db.Delete<ParamDtsInfoEntity>(x => x.ID == id && x.SEQ == seq);
            }
        }

        public void SaveDts(ParamDtsInfoEntity entity)
        {
            var db = CoreDBContext.GetContext();
            db.Set<ParamDtsInfoEntity>().Add(entity);
            db.SaveChanges();
        }
    }
}