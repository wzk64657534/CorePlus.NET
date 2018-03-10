using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class LengthAttribute : StringLengthAttribute
    {
        public LengthAttribute(int maximumLength, int minmumLength = 0)
            : base(maximumLength)
        {
            this.MinimumLength = minmumLength;
        }

        public override bool IsValid(object value)
        {
            if (value == null) { return true; }
            byte[] data = Encoding.Default.GetBytes(value.ToString());
            return data.Length >= this.MinimumLength && data.Length <= this.MaximumLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0}的长度超过限制，区间{1}-{2}位", name, this.MinimumLength, this.MaximumLength);
        }
    }
}