using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MonitorSpyAPI.Util.Helpers {
    public static class EncryptHelper {
        private static readonly string KEY_ENCRYPTION = ExtensionHelper.GetProjectName();
        private static readonly byte[] SALT_ENCRYPTION = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

        public static string Encrypt(this string encrypt) {
            byte[] encryptBytes = Encoding.Unicode.GetBytes(encrypt);
            string ret;

            using (Aes encryptor = Aes.Create()) {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(KEY_ENCRYPTION, SALT_ENCRYPTION);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream()) {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
                        cs.Write(encryptBytes, 0, encryptBytes.Length);
                        cs.Close();
                    }
                    ret = Convert.ToBase64String(ms.ToArray());
                }
            }
            return ret;
        }

        public static string Decrypt(this string decrypt) {
            decrypt = decrypt.Replace(" ", "+");
            byte[] decryptBytes = Convert.FromBase64String(decrypt);
            string ret;

            using (Aes encryptor = Aes.Create()) {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(KEY_ENCRYPTION, SALT_ENCRYPTION);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream()) {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)) {
                        cs.Write(decryptBytes, 0, decryptBytes.Length);
                        cs.Close();
                    }
                    ret = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return ret;
        }
    }
}
