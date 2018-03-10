using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;
using System.Web.Mvc;

namespace CorePlus.API.Web
{
    public interface IIndex
    {
        string GetMenuHtml(string[] args);
    }
}