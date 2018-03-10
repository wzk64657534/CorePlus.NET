using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CorePlus.AiXin.Web
{
    public class CryptHelper : Core.CryptHelper
    {
        public static string HMAC_DES(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
            des.Key = Encoding.UTF8.GetBytes(sKey.Substring(0, 8));
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.Zeros;
            //des.IV = ASCIIEncoding.ASCII.GetBytes(sKey.Substring(0,8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write); 
            cs.Write(inputByteArray, 0, inputByteArray.Length); 
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray()) 
            { 
                ret.AppendFormat("{0:X2}", b); 
            }
            return ret.ToString();
        }

        public static String HMAC_MD5(string input, string password)
        {
            byte[] b_tmp;
            byte[] b_tmp1;
            if (password == null)
            {
                return null;
            }
            byte[] digest = new byte[512];
            byte[] k_ipad = new byte[64];
            byte[] k_opad = new byte[64];
            byte[] source = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            System.Security.Cryptography.MD5 shainner = new MD5CryptoServiceProvider();
            for (int i = 0; i < 64; i++)
            {
                k_ipad[i] = 0 ^ 0x36;
                k_opad[i] = 0 ^ 0x5c;
            }

            try
            {
                if (source.Length > 64)
                {
                    shainner = new MD5CryptoServiceProvider();
                    source = shainner.ComputeHash(source);
                }
                for (int i = 0; i < source.Length; i++)
                {
                    k_ipad[i] = (byte)(source[i] ^ 0x36);
                    k_opad[i] = (byte)(source[i] ^ 0x5c);
                }
                b_tmp1 = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
                b_tmp = Adding(k_ipad, b_tmp1);
                shainner = new MD5CryptoServiceProvider();
                digest = shainner.ComputeHash(b_tmp);
                b_tmp = Adding(k_opad, digest);
                shainner = new MD5CryptoServiceProvider();
                digest = shainner.ComputeHash(b_tmp);
                string ret = "";
                for (int i = 0; i < digest.Length; i++)
                {
                    ret += Convert.ToString(digest[i], 16).PadLeft(2, '0');
                }
                return ret.PadLeft(32, '0');
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static byte[] Adding(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }
    }
}