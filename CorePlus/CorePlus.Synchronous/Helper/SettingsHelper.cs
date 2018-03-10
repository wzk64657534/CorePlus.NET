using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Core;

namespace CorePlus.Synchronous
{
    public class SettingsHelper
    {
        public static DateTime GetPreviousDate()
        {
            string directoryName = PathHelper.GetDirectoryName("Settings");
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            string filePath = Path.Combine(directoryName, "config.txt");
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    string content = sr.ReadToEnd();
                    if (string.IsNullOrEmpty(content))
                    {
                        return DateTime.Parse("2012-1-1");
                    }

                    return DateTime.Parse(content);
                }
            }
        }

        public static void SetPreviousDate(DateTime dtCurrent)
        {
            string directoryName = PathHelper.GetDirectoryName("Settings");
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            string filePath = Path.Combine(directoryName, "config.txt");
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                byte[] buffer = Encoding.Default.GetBytes(dtCurrent.ToString("yyyy-MM-dd"));
                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
