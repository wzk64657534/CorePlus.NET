using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Common
{
    public class DecodeHelper
    {
        public static bool IsUtf8(string url)
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
