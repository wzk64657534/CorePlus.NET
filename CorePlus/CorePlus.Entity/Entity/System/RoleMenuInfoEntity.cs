using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Core;

namespace CorePlus.Entity
{
    [Table("RoleMenuInfo")]
    public class RoleMenuInfoEntity : BaseEntity
    {
        [Display(Name = "角色编号")]
        public virtual long? RoleId { get; set; }
        [Display(Name = "菜单编号")]
        public virtual long? MenuId { get; set; }
    }
}