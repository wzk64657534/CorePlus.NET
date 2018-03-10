﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;

namespace CorePlus.Web
{
    public class ManagerLogin : BaseLogin<ManagerInfoEntity>
    {
        protected override string[] RedirectToAction()
        {
            return new string[] { "Index", "Admin" };
        }
    }
}