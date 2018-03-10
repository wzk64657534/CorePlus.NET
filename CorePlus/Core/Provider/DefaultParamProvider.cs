using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class DefaultParamProvider : IParamProvider
    {
        public virtual string GetParam(string param)
        {
            switch (param)
            {
                case "@UserName": return SessionHelper.UserName;
                case "@ChnName": return SessionHelper.ChnName;
                default: return null;
            }
        }
    }
}