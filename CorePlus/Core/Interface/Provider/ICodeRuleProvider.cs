using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface ICodeRuleProvider
    {
        IEnumerable<ICodeRuleEntity> GetCodeRules(string tableName, int type);

        string GetCode(ICodeRuleEntity codeRule, object entity, bool isTemp);
    }
}