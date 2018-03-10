using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    [Table("SysCodeRule")]
    public class CodeRuleEntity : ICodeRuleEntity
    {
        [Key]
        [Column(Order = 0)]
        [Length(50)]
        [MustInput]
        public string TableName { get; set; }

        [Key]
        [Column(Order = 1)]
        [Length(50)]
        [MustInput]
        public string FieldName { get; set; }

        [Length(200)]
        [MustInput]
        public string GenerateRule { get; set; }

        [Length(50)]
        public string DateRule { get; set; }

        [Length(10)]
        public string AutoIncreaceRule { get; set; }

        public Nullable<int> Type { get; set; }

        public Nullable<bool> IsValid { get; set; }
    }
}