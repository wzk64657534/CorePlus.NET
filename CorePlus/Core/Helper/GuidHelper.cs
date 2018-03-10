using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class GuidHelper
    {
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString().ToLower();
        }
    }
}
