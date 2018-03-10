using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface ICoreRepositoty
    {
        bool Exists(IEnumerable<Tuple<string, ExpressionOperator, object>> expressions);

        int Count(IEnumerable<Tuple<string, ExpressionOperator, object>> expressions);
    }
}
