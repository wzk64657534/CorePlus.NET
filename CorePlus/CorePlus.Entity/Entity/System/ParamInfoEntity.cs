using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("ParamInfo")]
    public class ParamInfoEntity : SimpleDetailEntity<ParamDtsInfoEntity>
    {
        [Display(Name = "大类名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = ("输入不正确，{0}最大长度不超过{1}个英文"))]
        public string ParamName { get; set; }
    }
}