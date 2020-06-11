
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Common_Module.StringTool
{
    public class Encrypt3DEHelper
    {
        //构造一个对称算法
        private SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

        /// <summary>
        /// 字符串的3DES加密
        /// 于挺
        /// 日期：2017年3月14日15:38:32
        /// </summary>
        /// <param name="Value">要加密的字符串</param>
        /// <param name="sKey">密钥，必须24位</param>
        /// <param name="sIV">向量，必须是8个字符</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptString(string Value, string sKey, string sIV)
        {
            //try
            //{
            //    ICryptoTransform ct;
            //    MemoryStream ms;
            //    CryptoStream cs;
            //    byte[] byt;
            //    mCSP.Key = Encoding.UTF8.GetBytes(sKey);// Convert.FromBase64String(sKey);
            //    mCSP.IV = Encoding.UTF8.GetBytes(sIV); //Convert.FromBase64String(sIV);
            //    //指定加密的运算模式
            //    //mCSP.Mode = System.Security.Cryptography.CipherMode.ECB; // 为与Java加解密互通，要将CipherMode.ECB改为CipherMode.CBC
            //    mCSP.Mode = System.Security.Cryptography.CipherMode.CBC;
            //    //获取或设置加密算法的填充模式
            //    mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            //    ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);//创建加密对象
            //    byt = Encoding.UTF8.GetBytes(Value);
            //    ms = new MemoryStream();
            //    cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            //    cs.Write(byt, 0, byt.Length);
            //    cs.FlushFinalBlock();
            //    cs.Close();

            //    return Convert.ToBase64String(ms.ToArray());
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return ("Error in Encrypting " + ex.Message);
            //}


            //分组加密算法
            SymmetricAlgorithm des = Rijndael.Create();
            //得到需要加密的字节数组 
            byte[] inputByteArray = Encoding.UTF8.GetBytes(Value);
            //设置密钥及密钥向量                                                         
            des.Key = Encoding.UTF8.GetBytes(sKey);
            des.IV = Encoding.UTF8.GetBytes(sIV);
            byte[] cipherBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    //得到加密后的字节数组
                    cipherBytes = ms.ToArray();
                    cs.Close();
                    ms.Close();
                }
            }
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// 字符串3DES解密
        /// 于挺
        /// 日期：2017年3月14日15:39:17
        /// </summary>
        /// <param name="Value">加密后的字符串</param>
        /// <param name="sKey">密钥，必须24位</param>
        /// <param name="sIV">向量，必须是8个字符</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptString(string Value, string sKey, string sIV)
        {
            //try
            //{
            //    ICryptoTransform ct;//加密转换运算
            //    MemoryStream ms;//内存流
            //    CryptoStream cs;//数据流连接到数据加密转换的流
            //    byte[] byt;

            //    mCSP.Key = Encoding.UTF8.GetBytes(sKey);// Convert.FromBase64String(sKey);
            //    mCSP.IV = Encoding.UTF8.GetBytes(sIV); //Convert.FromBase64String(sIV);

            //    //mCSP.Mode = System.Security.Cryptography.CipherMode.ECB; // 为与Java加解密互通，要将CipherMode.ECB改为CipherMode.CBC
            //    mCSP.Mode = System.Security.Cryptography.CipherMode.CBC;
            //    mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            //    ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);//创建对称解密对象
            //    byt = Convert.FromBase64String(Value);
            //    ms = new MemoryStream();
            //    cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            //    cs.Write(byt, 0, byt.Length);
            //    cs.FlushFinalBlock();
            //    cs.Close();

            //    return Encoding.UTF8.GetString(ms.ToArray());
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return ("Error in Decrypting " + ex.Message);
            //}

            //string showText = "1ZkwOCZhPKc+4q2S+4SSQQ==";
            byte[] cipherText = Convert.FromBase64String(Value);

            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(sKey);
            des.IV = Encoding.UTF8.GetBytes(sIV);
            byte[] decryptBytes = new byte[cipherText.Length];
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            //将字符串后尾的'\0'去掉
            return Encoding.UTF8.GetString(decryptBytes).Replace("\0", "");
        }


        /// <summary>  
        /// AES加密(无向量)  
        /// </summary>  
        /// <param name="plainBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>密文</returns>  
        public string AESEncrypt(string value, string key)
        {
            MemoryStream mStream = new MemoryStream();
            RijndaelManaged aes = new RijndaelManaged();

            byte[] plainBytes = Encoding.UTF8.GetBytes(value);
            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            //aes.Key = _key;  
            aes.Key = bKey;
            //aes.IV = _iV;  
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }

        /// AES解密(无向量)  
        /// </summary>  
        /// <param name="encryptedBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>明文</returns>  
        public string AESDecrypt(string value, string key)
        {
            Byte[] encryptedBytes = Convert.FromBase64String(value);
            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

            MemoryStream mStream = new MemoryStream(encryptedBytes);
            //mStream.Write( encryptedBytes, 0, encryptedBytes.Length );  
            //mStream.Seek( 0, SeekOrigin.Begin );  
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = bKey;
            //aes.IV = _iV;  
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[encryptedBytes.Length + 32];
                int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
                byte[] ret = new byte[len];
                Array.Copy(tmp, 0, ret, 0, len);
                return Encoding.UTF8.GetString(ret);
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }


        /// <summary>
        /// AES加密算法 NET , JAVA , PHP 加密互通
        /// 日期：2017年11月6日17:13:08
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <returns>将加密后的密文转换为Base64编码，以便显示</returns>
        public string AESEncrypt_HuTong(string plainText, string skey, string siv)
        {
            //分组加密算法
            SymmetricAlgorithm des = Rijndael.Create();
            //得到需要加密的字节数组 
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
            //设置密钥及密钥向量                                                         
            des.Key = Encoding.UTF8.GetBytes(skey);
            des.IV = Encoding.UTF8.GetBytes(siv);
            byte[] cipherBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    //得到加密后的字节数组
                    cipherBytes = ms.ToArray();
                    cs.Close();
                    ms.Close();
                }
            }
            return Convert.ToBase64String(cipherBytes);
        }


        /// <summary>
        /// AES解密算法 NET , JAVA , PHP 加密互通
        /// 日期：2017年11月6日17:13:08
        /// </summary>
        /// <param name="cipherText">待解密的密文字符串</param>
        /// <returns>返回解密后的明文字符串</returns>
        public string AESDecrypt_HuTong(string showText, string skey, string siv)
        {
            //string showText = "1ZkwOCZhPKc+4q2S+4SSQQ==";
            byte[] cipherText = Convert.FromBase64String(showText);

            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(skey);
            des.IV = Encoding.UTF8.GetBytes(siv);
            byte[] decryptBytes = new byte[cipherText.Length];
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            //将字符串后尾的'\0'去掉
            return Encoding.UTF8.GetString(decryptBytes).Replace("\0", "");
        }
    }
}
