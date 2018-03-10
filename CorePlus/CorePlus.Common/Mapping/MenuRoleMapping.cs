using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using CorePlus.Entity;

namespace CorePlus.Common
{
    public class MenuRoleMapping : BaseMapping<RoleMenuInfoEntity>
    {
        public MenuRoleMapping()
        {
            HasKey(x => new { x.RoleId, x.MenuId });
        }
    }
}