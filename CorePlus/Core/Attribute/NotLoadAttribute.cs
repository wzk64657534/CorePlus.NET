using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotLoadAttribute : Attribute
    {

    }
}
