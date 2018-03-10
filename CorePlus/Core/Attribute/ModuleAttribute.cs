using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute
    {
        public string Name { get; private set; }
        public ModuleAttribute(string moduleName)
        {
            this.Name = moduleName;
        }
    }
}