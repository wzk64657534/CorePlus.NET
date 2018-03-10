using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core
{
    public class StringHelper
    {
        public static string GetArrayAtLast(string strParam)
        {
            string[] typeParts = strParam.Split('.');
            return typeParts[typeParts.Length - 1];
        }

        public const string FieldRegex = @"{(\S+)}";

        public static string AnalyseField(string context, Dictionary<string, object> dataDict)
        {
            if (!string.IsNullOrEmpty(context))
            {
                var regex = new Regex(FieldRegex);

                return regex.Replace(context, match =>
                {
                    string fieldKey = match.Groups[1].Value;
                    object value = null;
                    if (dataDict.TryGetValue(fieldKey, out value))
                    {
                        return value.ToString();
                    }
                    return match.Groups[0].Value;
                });
            }
            return context;
        }

        public static string[] Split(string items, char spliter)
        {
            return items.Split(new char[] { spliter }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}