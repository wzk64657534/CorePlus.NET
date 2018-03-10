using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PasswordAttribute : StringLengthAttribute
    {
        public PasswordAttribute(int minLength = 6, int maxLength = 20)
            : base(maxLength)
        {
            this.MinimumLength = minLength;
        }

        public override bool IsValid(object value)
        {
            var result = base.IsValid(value);
            if (result)
            {
                Regex regex = new Regex(ConstHelper.RegexPassword);
                return regex.IsMatch(Convert.ToString(value));
            }
            return result;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0}必须是{1}-{2}位数字,字母和特殊字符组成", name, this.MinimumLength, this.MaximumLength);
        }
    }
}
