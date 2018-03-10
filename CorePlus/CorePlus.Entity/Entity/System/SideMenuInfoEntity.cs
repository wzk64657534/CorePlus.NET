using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("SideMenuInfo")]
    public class SideMenuInfoEntity : BaseEntity
    {
        public string MenuName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public long? MenuID { get; set; }
        public string ParamValue { get; set; }
        public int ParentMenuID { get; set; }

        [NotMapped]
        public bool Selected { get; set; }

        [NotMapped]
        public string Parameters { get; set; }
    }
}