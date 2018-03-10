using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Core
{
    public class LogHelper
    {
        public static void WriteLog(string strRecord, bool isNeed = true, string prefix = null)
        {
            if (isNeed)
            {
                string directory = "C:/logs/" + (string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + "/");
                string fileName = string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd"));
                string filePath = directory + fileName;

                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                System.IO.StreamWriter sr = null;

                try
                {
                    if (!System.IO.File.Exists(filePath))
                    {
                        sr = System.IO.File.CreateText(filePath);
                    }
                    else
                    {
                        sr = System.IO.File.AppendText(filePath);
                    }

                    sr.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strRecord));
                }
                catch
                {

                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                }
            }
        }

        public static log4net.ILog Log4Net = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}