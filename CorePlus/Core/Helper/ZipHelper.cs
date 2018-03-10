using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

using Ionic.Zip;

namespace Core
{
    public class ZipHelper
    {
        public static string[] Suffixs = new string[] { "zip" };

        public static void Zip(string[] filePaths, string saveFilePath)
        {
            using (ZipFile zip = new ZipFile(saveFilePath))
            {
                zip.AddFiles(filePaths);
                zip.Save();
            }
        }

        public static void UnZip(string unZipFilePath, string outputFilePath)
        {
            using (ZipFile zip = new ZipFile(unZipFilePath))
            {
                zip.ExtractAll(outputFilePath, ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}