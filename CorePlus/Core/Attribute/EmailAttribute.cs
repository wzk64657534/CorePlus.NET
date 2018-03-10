using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute()
            : base(ConstHelper.RegexEmail)
        {

        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0}不是有效的邮箱格式！", name);
        }
    }
}