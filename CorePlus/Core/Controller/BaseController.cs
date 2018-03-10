using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Core
{
    public class BaseController<TRepository> : System.Web.Mvc.Controller
        where TRepository : BaseRepository, new()
    {
        private TRepository repository;

        public TRepository Repository
        {
            get { return this.repository; }
        }

        public BaseController()
        {
            repository = new TRepository();
        }

        public virtual bool CheckError()
        {
            return ModelState.CheckModel();
        }
    }

    public class BaseController<TRepository, TEntity> : BaseController<TRepository>, ICoreController
        where TRepository : BaseRepository<TEntity>, new()
        where TEntity : BaseEntity
    {
        public virtual ActionResult Index(long? id, long? arg)
        {
            int currentPageIndex = Convert.ToInt32(id ?? 1);
            int pagecnt = 0;
            int recordcnt = 0;
            var query = Repository.GetByPager(currentPageIndex, 10, out pagecnt, out recordcnt).ToList();
            ViewBag.PageCount = pagecnt;
            ViewBag.PageIndex = currentPageIndex;
            ViewBag.RecordCount = recordcnt;

            Type type = typeof(TEntity);
            ViewBag.Href = "/" + type.Name.Replace("Entity", "").Replace("Info", "") + "/Index";
            return View(query);
        }

        ICoreRepositoty ICoreController.CoreRepository { get { return this.Repository; } }
    }
}