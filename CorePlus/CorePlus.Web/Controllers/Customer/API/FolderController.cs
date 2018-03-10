using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

using Core;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Web
{
    public class FolderController : ApiBaseController<FolderRepository, FolderInfoEntity, ViewFolderInfoEntity>
    {
        protected override List<FolderInfoEntity> GetEntityList(string[] sqls, out int records)
        {
            return Repository.GetByPagerQuery(sqls, out records).ToList();
        }

        protected override void FillStatistics(FolderInfoEntity entity, ViewFolderInfoEntity statistics)
        {
            statistics.ID = entity.ID;
            statistics.FolderName = entity.FolderName;
            statistics.AccountName = entity.AccountName;
        }

        public ActionResult GetObjIds(long id)
        {
            MonitorRepository repository = new MonitorRepository();
            List<string> list = new List<string>();
            var entitys = repository.FindByExpression(x => x.FolderId == id).Select(x => x.ObjId);
            return Json(entitys, JsonRequestBehavior.AllowGet);
        }
    }
}