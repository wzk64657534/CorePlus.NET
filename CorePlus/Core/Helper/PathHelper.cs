using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class PathHelper
    {
        public static string GetProjectPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetDirectoryName(string directory)
        {
            return string.Concat(GetProjectPath(), directory + "\\");
        }
    }
}