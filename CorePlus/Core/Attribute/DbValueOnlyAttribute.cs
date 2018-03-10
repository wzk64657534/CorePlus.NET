using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class DbValueOnlyAttribute : Attribute
    {
        public DbValueOnlyAttribute()
        {

        }

        public DbValueOnlyAttribute(params string[] dependOnProperty)
            : this()
        {
            this.DependOnProperty = dependOnProperty;
        }

        public string[] DependOnProperty
        {
            get;
            private set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }
    }
}
