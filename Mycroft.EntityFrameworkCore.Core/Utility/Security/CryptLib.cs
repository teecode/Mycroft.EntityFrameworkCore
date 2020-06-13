using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Core.Utility.Security
{
    class CryptLib
    {
        public RijndaelManaged GetRijndaelManaged(String secretKey)
        {
            //  byte[] passByte = generateByteSHA1AndEncodeBase64(pass);
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7,
                //KeySize = 128,
                //BlockSize = 128,
                Key = keyBytes,
                // IV = keyBytes
            };


        }

        public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }


        public byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        /// <summary>
        /// Encrypts plaintext using AES 128bit key and a Chain Block Cipher and returns a base64 encoded string
        /// </summary>
        /// <param name="plainText">Plain text to encrypt</param>
        /// <param name="key">Secret key</param>
        /// <returns>Base64 encoded string</returns>
        public String Encrypt(String plainText, String key)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            // Console.WriteLine(GetRijndaelManaged(key).);
            return ByteArrayToString(Encrypt(plainBytes, GetRijndaelManaged(key)));
        }


        private string encodeBase64String(byte[] passByte)
        {
            return Convert.ToBase64String(passByte);
        }

        private byte[] generateByteSHA1AndEncodeBase64(string pass)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(pass));
                return Encoding.UTF8.GetBytes(ByteArrayToString(hash));
                // return Convert.ToBase64String(hash);
            }
        }


        /// <summary>
        /// Decrypts a base64 encoded string using the given key (AES 128bit key and a Chain Block Cipher)
        /// </summary>
        /// <param name="encryptedText">Base64 Encoded String</param>
        /// <param name="key">Secret Key</param>
        /// <returns>Decrypted String</returns>
        public String Decrypt(String encryptedText, String key)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(key)));
        }


        //static void Main(string[] args)
        //{

        //    CryptLib sym = new CryptLib();
        //    byte[] passByte = sym.generateByteSHA1AndEncodeBase64("ILOTTO1qaz");
        //    Console.WriteLine("passByte: " + passByte);
        //    String pass64 = sym.encodeBase64String(passByte);
        //    Console.WriteLine("encodedBase64: " + pass64);
        //    Console.WriteLine(sym.Encrypt(pass64,"baU4is@8nb2iShAMc9781m8As111yHf5"));
        //}

    }
}
