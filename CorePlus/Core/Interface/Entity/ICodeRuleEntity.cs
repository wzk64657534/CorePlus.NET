using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface ICodeRuleEntity
    {
        string TableName { get; set; }

        string FieldName { get; set; }

        string GenerateRule { get; set; }

        string DateRule { get; set; }

        string AutoIncreaceRule { get; set; }

        Nullable<int> Type { get; set; }
    }
}