using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Core;
using CorePlus.Entity;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using CorePlus.Common;

namespace CorePlus.Repository
{
    public class VisitAshxRepository : AshxSimpleRepository<VisitInfoEntity>
    {
        public override VisitInfoEntity FillEntity(HttpContext context, string data)
        {
            // 先执行反序列化
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            VisitInfoEntity entity = (VisitInfoEntity)serializer.Deserialize(data, typeof(VisitInfoEntity));

            // 来访公网IP
            string ip = context.Request.UserHostAddress;
            entity.LocationIP = ip;

            // 判断传入的关键词是否是加密的，是就解密
            if (string.IsNullOrEmpty(entity.RefererKeyword) == false)
            {
                if (DecodeHelper.IsUtf8(entity.RefererKeyword))
                {
                    entity.RefererKeyword = context.Server.UrlDecode(entity.RefererKeyword);

                }
                else
                {
                    entity.RefererKeyword = HttpUtility.UrlDecode(entity.RefererKeyword, Encoding.GetEncoding("GB2312"));
                }
            }

            #region 测试代码
            //string s =
            //    "%C7%EB%B0%D1%C4%E3%D0%E8%D2%AA%B1%E0%C2%EB%BD%E2%C2%EB%B5%C4%C4%DA%C8%DD%D5%B3%CC%F9%D4%DA%D5%E2%C0%EF%A3%A1";
            //if (IsUtf8(s))
            //{
            //    string result = context.Server.UrlDecode(s);
            //}
            //else
            //{
            //    string result = HttpUtility.UrlDecode(s, Encoding.GetEncoding("GB2312"));
            //}
            #endregion

            return entity;
        }

        private static bool IsUtf8(string url)
        {
            byte[] buf = GetUrlCodingToBytes(url);
            int i;
            byte cOctets; // octets to go in this UTF-8 encoded character    
            bool bAllAscii = true;
            long iLen = buf.Length;
            cOctets = 0;
            for (i = 0; i < iLen; i++)
            {
                if ((buf[i] & 0x80) != 0) bAllAscii = false;

                if (cOctets == 0)
                {
                    if (buf[i] >= 0x80)
                    {
                        do
                        {
                            buf[i] <<= 1;
                            cOctets++;
                        }
                        while ((buf[i] & 0x80) != 0);

                        cOctets--;
                        if (cOctets == 0)
                            return false;
                    }
                }
                else
                {
                    if ((buf[i] & 0xC0) != 0x80)
                        return false;
                    cOctets--;
                }
            }
            if (cOctets > 0)
                return false;
            if (bAllAscii)
                return false;
            return true;
        }

        private static byte[] GetUrlCodingToBytes(string url)
        {
            StringBuilder sb = new StringBuilder();

            int i = url.IndexOf('%');
            while (i >= 0)
            {
                if (url.Length < i + 3)
                {
                    break;
                }
                sb.Append(url.Substring(i, 3));
                url = url.Substring(i + 3);
                i = url.IndexOf('%');
            }

            string urlCoding = sb.ToString();
            if (string.IsNullOrEmpty(urlCoding))
                return new byte[0];

            urlCoding = urlCoding.Replace("%", string.Empty);
            int len = urlCoding.Length / 2;
            byte[] result = new byte[len];
            len *= 2;
            for (int index = 0; index < len; index++)
            {
                string s = urlCoding.Substring(index, 2);
                int b = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
                result[index / 2] = (byte)b;
                index++;
            }
            return result;
        }
    }
}
