using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UserNameAttribute : RegularExpressionAttribute
    {
        public UserNameAttribute()
            : base(ConstHelper.RegexUserName)
        {

        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0}必须是数字字母下划线组成", name);
        }
    }
}