using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using Core.Entity;
using CorePlus.Entity;
using System.Text;

namespace CorePlus.Web
{
    public class FrontBaseController<TRepository, TEntity> : SimpleController<TRepository, TEntity>
        where TRepository : SimpleRepository<TEntity>, new()
        where TEntity : BaseEntity, new()
    {
        public override ActionResult Index(long? id, long? arg)
        {
            return View();
        }

        public override ActionResult Create()
        {
            var entity = Repository.NewEntity(User.Identity.Name);
            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public override ActionResult Edit(long id, TEntity model)
        {
            if (ModelState.IsValid)
            {
                Repository.Update(id, model);
                return Content("ok");
            }

            return Content("修改失败");
        }

        public virtual List<TEntity> GetSelectedData(List<TEntity> query)
        {
            return query;
        }

        public virtual ActionResult GetDataByPager()
        {
            int page = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int rows = Request["rows"] == null ? 15 : int.Parse(Request["rows"]);
            int totalPageCnt = 0;
            int recordCount = 0;

            var models = Repository.GetByPager(page, rows, out totalPageCnt, out  recordCount);

            models = GetSelectedData(models);

            recordCount = models.Count;
            totalPageCnt = (recordCount % rows) > 0 ? ((recordCount / rows) + 1) : (recordCount / rows);

            var result = new { total = recordCount, rows = models };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Add()
        {
            return Create();
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Add(TEntity model)
        {
            try
            {
                if (CheckError())
                {
                    Create(model);
                    return Content("ok");
                }
                else
                {
                    StringBuilder errors = new StringBuilder();
                    foreach (var key in ModelState.Keys)
                    {
                        var state = ModelState[key];
                        if (state.Errors.Any())
                        {
                            errors.Append(state.Errors.First().ErrorMessage);
                        }
                    }

                    return Content(errors.ToString());
                }
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }

        public ActionResult RemoveServant(string ids)
        {
            string[] str = ids.Split(',');
            Repository.Delete(str);
            return Content("ok");
        }

        public ActionResult GetMessageTypeList()
        {
            var list = EntityWebHelper.GetMessageTypeList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetReplyTypeList()
        {
            var list = EntityWebHelper.GetReplyTypeList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWxMenuTypeList()
        {
            var list = EntityWebHelper.GetWxMenuTypeList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}