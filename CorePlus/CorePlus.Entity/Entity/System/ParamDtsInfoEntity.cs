using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("ParamDtsInfo")]
    public class ParamDtsInfoEntity : DetailEntity
    {
        [Display(Name = "参数名称")]
        [Required(ErrorMessage = "请输入{0}")]
        public string ParamDtsName { get; set; }

        [Display(Name = "参数值")]
        [Required(ErrorMessage = "请输入{0}，只能是数字，不可以是0")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "请重新输入，只能是数字，不可以是0")]
        public string ParamValue { get; set; }
    }
}