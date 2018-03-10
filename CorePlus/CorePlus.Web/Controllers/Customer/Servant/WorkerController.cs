using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Web
{
    [Authorize(Roles = "servant")]
    public class WorkerController : FrontUserController<ServantRepository, ServantInfoEntity>
    {
        // 客服窗口页
        public ActionResult Online()
        {
            // 客服窗口连接
            ViewBag.ServantUrl = ConfigurationHelper.Get("ServantUrl");
            return View();
        }
    }
}