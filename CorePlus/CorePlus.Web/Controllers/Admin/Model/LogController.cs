using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.Entity;
using Core;
using CorePlus.Repository;

namespace CorePlus.Web
{
    [Authorize(Roles = "administrator")]
    public class LogController : BaseController<LogRepository, LogInfoEntity>
    {

    }
}