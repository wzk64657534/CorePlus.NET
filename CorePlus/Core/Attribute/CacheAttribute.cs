using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CacheAttribute : Attribute
    {
        public string Name { get; private set; }
        public CacheAttribute(string moduleName)
        {
            this.Name = moduleName;
        }
    }
}
