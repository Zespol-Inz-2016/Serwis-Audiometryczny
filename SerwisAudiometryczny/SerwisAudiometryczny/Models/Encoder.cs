﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Klasa szyfrująca
    /// </summary>
    static public class Encoder
    {
        /// <summary>
        /// Metoda szyfrujaca
        /// </summary>
        /// <param name="clearText">Tekst do zaszyfrowania</param>
        /// <returns>Tekst po zaszyfrowaniu</returns>
        static public string Encrypt(string clearText)
        {
            if (clearText == null || clearText == string.Empty)
                return clearText;
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = "@" + Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Metoda deszyfrujaca
        /// </summary>
        /// <param name="clearText">Tekst do odszyfrowania</param>
        /// <returns>Tekst po odszyfrowaniu</returns>
        static public string Decrypt(string cipherText)
        {
            if (cipherText == null || cipherText == string.Empty)
                return cipherText;
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText.Remove(0, 1));
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        /// <summary>
        /// Metoda sprawdza czy tekst jest szyfrownay
        /// </summary>
        /// <param name="s">tekst do sprawdzenia</param>
        /// <returns>prawda, gdy zaszyfrowany</returns>
        public static bool IsEncrypted(string s)
        {
            if (s == null || s == string.Empty)
                return true;
            else
                return s.StartsWith("@");
        }
    }
}
