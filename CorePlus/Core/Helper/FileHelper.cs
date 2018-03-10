using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Core
{
    public partial class FileHelper
    {
        public static bool DownLoadFile(string url, string directoryName, string fileName)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            WebRequest request = WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            int length = (int)response.ContentLength;
            string filePath = Path.Combine(directoryName, fileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                BinaryReader br = new BinaryReader(stream);
                fs.Write(br.ReadBytes(length), 0, length);
                br.Close();
                return true;
            }
        }
    }
}