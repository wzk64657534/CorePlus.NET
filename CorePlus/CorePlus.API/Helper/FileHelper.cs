using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.API
{
    public partial class FileHelper
    {
        public static string GetFileNameFromUrlForInfo(string path)
        {
            // 基础数据
            string query = new Uri(path).Query;
            int beginIndex = query.IndexOf("&f") + 2;
            int endIndex = query.IndexOf("&h");
            string result = query.Substring(beginIndex, endIndex - beginIndex);
            string[] parts = result.Split(new string[] { "%2F" }, StringSplitOptions.RemoveEmptyEntries);
            string fileName = parts[parts.Length - 1];
            return fileName;
        }

        public static string GetFileNameFromUrlForStatistics(string path)
        {
            // 基础数据
            string query = new Uri(path).Query;
            int beginIndex = query.IndexOf("&file") + 5;
            int endIndex = query.IndexOf("&host");
            string result = query.Substring(beginIndex, endIndex - beginIndex);
            string[] parts = result.Split(new string[] { "%2F" }, StringSplitOptions.RemoveEmptyEntries);
            string fileName = parts[parts.Length - 1];
            return fileName;
        }
    }
}