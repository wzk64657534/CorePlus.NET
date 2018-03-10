using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core
{
    public class SimpleController<TRepository, TEntity> : BaseController<TRepository, TEntity>
        where TRepository : SimpleRepository<TEntity>, new()
        where TEntity : BaseEntity, new()
    {
        [HttpGet]
        public virtual ActionResult Create()
        {
            var entity = Repository.NewEntity(null);
            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Create(TEntity entity)
        {
            if (CheckError())
            {
                Repository.Add(entity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(entity);
            }
        }

        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            var entity = Repository.FindByID(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Edit(long id, TEntity uiEntity)
        {
            if (CheckError())
            {
                var entity = Repository.Update(id, uiEntity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(uiEntity);
            }
        }

        [HttpGet]
        public virtual ActionResult Delete(long id)
        {
            var model = Repository.FindByID(id);

            if (model != null)
            {
                Repository.Delete(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public void SaveBatch(List<TEntity> saveData, long[] removedData)
        {
            CheckError();
            Repository.SaveBatch(saveData, removedData);
        }
    }
}