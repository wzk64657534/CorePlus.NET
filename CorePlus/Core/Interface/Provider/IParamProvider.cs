using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface IParamProvider
    {
        string GetParam(string param);
    }
}
