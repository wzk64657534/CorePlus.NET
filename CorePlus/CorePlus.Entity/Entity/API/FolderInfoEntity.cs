using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("FolderInfo")]
    public class FolderInfoEntity : MaterialEntity
    {
        [Display(Name = "名称")]
        public string FolderName { get; set; }
    }
}