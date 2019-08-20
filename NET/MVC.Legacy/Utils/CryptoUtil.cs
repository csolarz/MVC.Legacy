using System;
using System.Security.Cryptography;

namespace MVC.Legacy.Utils
{
    public class CryptoUtil
    {
        private RijndaelManaged myRijndael = new RijndaelManaged();
        private int iterations;
        private byte[] salt;

        public CryptoUtil()
        {
            myRijndael.BlockSize = 128;
            myRijndael.KeySize = 128;

            string passwordKey = System.Configuration.ConfigurationManager.AppSettings["CryptoUtil.Password"];
            string ivKey = System.Configuration.ConfigurationManager.AppSettings["CryptoUtil.IV"];
            string saltKey = System.Configuration.ConfigurationManager.AppSettings["CryptoUtil.Salt"];

            if (string.IsNullOrEmpty(passwordKey) || string.IsNullOrWhiteSpace(ivKey) || string.IsNullOrWhiteSpace(saltKey)) {
                throw new ArgumentNullException("CryptoUtil parametros de inicializacion requeridos: [CryptoUtil.Password], [CryptoUtil.IV], [CryptoUtil.Salt]");
            }

            myRijndael.IV = HexStringToByteArray(ivKey);

            myRijndael.Padding = PaddingMode.PKCS7;
            myRijndael.Mode = CipherMode.CBC;

            iterations = 1000;
            salt = System.Text.Encoding.UTF8.GetBytes(saltKey);

            myRijndael.Key = GenerateKey(passwordKey);
        }

        public string Encrypt(string strPlainText)
        {
            byte[] strText = new System.Text.UTF8Encoding().GetBytes(strPlainText);
            ICryptoTransform transform = myRijndael.CreateEncryptor();
            byte[] cipherText = transform.TransformFinalBlock(strText, 0, strText.Length);
            return Convert.ToBase64String(cipherText);
        }

        public string Decrypt(string encryptedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            ICryptoTransform transform = myRijndael.CreateDecryptor();
            byte[] cipherText = transform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return System.Text.Encoding.UTF8.GetString(cipherText);
        }

        public static byte[] HexStringToByteArray(string strHex)
        {
            dynamic r = new byte[strHex.Length / 2];
            for (int i = 0; i <= strHex.Length - 1; i += 2)
            {
                r[i / 2] = Convert.ToByte(Convert.ToInt32(strHex.Substring(i, 2), 16));
            }
            return r;
        }

        private byte[] GenerateKey(string strPassword)
        {
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(System.Text.Encoding.UTF8.GetBytes(strPassword), salt, iterations);
            return rfc2898.GetBytes(128 / 8);
        }
    }
}