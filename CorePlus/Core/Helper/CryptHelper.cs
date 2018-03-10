using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Core
{
    public partial class CryptHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="md5String">要加密的字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string MD5(string md5String)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5String, "MD5").ToLower();
        }
        /// <summary>
        /// SHA1加密
        /// </summary>
        public static string SHA1(string value)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "SHA1");
        }
        /// <summary>
        /// 简单加密函数
        /// </summary>
        /// <param name="simpleString">要加密的字符串</param>
        /// <returns>返回加密后的字符串</returns> 
        public static string SimpleEncode(string simpleString)
        {
            string s = "";
            try
            {
                for (int i = 0; i < simpleString.Length; i++)
                {
                    s += (char)(simpleString[i] + 10 - 1 * 2);
                }
                return s;
            }
            catch
            {
                return simpleString;
            }
        }
        /// <summary>
        /// 简单解密函数
        /// </summary>
        /// <param name="simpleString">要解密的字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string SimpleDecode(string simpleString)
        {
            string s = "";
            try
            {
                for (int i = 0; i < simpleString.Length; i++)
                {
                    s += (char)(simpleString[i] - 10 + 1 * 2);
                }
                return s;
            }
            catch
            {
                return simpleString;
            }
        }
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string desIV = "~!*#$%^&";
        /// <summary>
        /// 对称加密法加密函数
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string DESEncode(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        /// <summary>
        /// 对称加密法加密函数
        /// </summary>
        /// <param name="encryptString">待加密的字符串，使用默认密钥加密</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string DESEncode(string encryptString)
        {
            return DESEncode(encryptString, desIV);
        }
        /// <summary>
        /// 对称加密法解密函数
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DESDecode(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        /// <summary>
        /// 对称加密法解密函数
        /// </summary>
        /// <param name="decryptString">待解密的字符串，使用默认密钥加密</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DESDecode(string decryptString)
        {
            return DESDecode(decryptString, desIV);
        }

        #region RSA加密解密
        #region RSA 的密钥产生
        /// <summary>
        /// 生成公钥和私钥
        /// </summary>
        /// <param name="XMLPrivateKey">私钥</param>
        /// <param name="XMLPublicKey">公钥</param>
        public static void CreateRSAKey(out string XMLPrivateKey, out string XMLPublicKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                XMLPrivateKey = rsa.ToXmlString(true);
                XMLPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA的加密函数
        //##############################################################################
        //RSA 方式加密
        //说明KEY必须是XML的行式,返回的是字符串
        //在有一点需要说明！！该加密方式有 长度 限制的！！
        //##############################################################################
        //RSA的加密函数
        public static string RSAEncrypt(string XMLPublicKey, string m_strEncryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(XMLPublicKey);
                PlainTextBArray = (new UnicodeEncoding()).GetBytes(m_strEncryptString);
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //RSA的加密函数
        public static string RSAEncrypt(string XMLPublicKey, byte[] EncryptString)
        {
            try
            {
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(XMLPublicKey);
                CypherTextBArray = rsa.Encrypt(EncryptString, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA的解密函数
        //RSA的解密函数
        public static string RSADecrypt(string XMLPrivateKey, string m_strDecryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(XMLPrivateKey);
                PlainTextBArray = Convert.FromBase64String(m_strDecryptString);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //RSA的解密函数
        public static string RSADecrypt(string XMLPrivateKey, byte[] DecryptString)
        {
            try
            {
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(XMLPrivateKey);
                DypherTextBArray = rsa.Decrypt(DecryptString, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region RSA数字签名
        #region 获取Hash描述表
        //获取Hash描述表
        public static bool GetHash(string m_strSource, ref byte[] HashData)
        {
            try
            {
                //从字符串中取得Hash描述
                byte[] Buffer;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = System.Text.Encoding.Default.GetBytes(m_strSource);
                HashData = MD5.ComputeHash(Buffer);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //获取Hash描述表
        public static bool GetHash(string m_strSource, ref string strHashData)
        {
            try
            {
                //从字符串中取得Hash描述
                byte[] Buffer;
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = System.Text.Encoding.Default.GetBytes(m_strSource);
                HashData = MD5.ComputeHash(Buffer);
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //获取Hash描述表
        public static bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {
            try
            {
                //从文件中取得Hash描述
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //获取Hash描述表
        public static bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {
            try
            {
                //从文件中取得Hash描述
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA签名
        //RSA签名
        public static bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //RSA签名
        public static bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] EncryptedSignatureData;
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //RSA签名
        public static bool SignatureFormatter(string strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;
                HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //RSA签名
        public static bool SignatureFormatter(string strKeyPrivate, string m_strHashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;
                byte[] EncryptedSignatureData;
                HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA签名验证
        public static bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter
                    = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter
                    = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion
    }
}