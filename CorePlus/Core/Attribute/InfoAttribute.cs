using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InfoAttribute : Attribute
    {
        public string Name { get; private set; }
        public InfoAttribute(string moduleName)
        {
            this.Name = moduleName;
        }
    }
}
