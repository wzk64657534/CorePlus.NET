using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MustInputAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.Format("请输入{0}", name);
        }
    }
}