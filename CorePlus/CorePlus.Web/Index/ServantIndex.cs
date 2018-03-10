using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;
using CorePlus.Repository;
using System.Web.Mvc;
using System.Text;

namespace CorePlus.Web
{
    public class ServantIndex : BaseIndex
    {
        protected override int GetMenuId()
        {
            return 11;
        }
    }
}