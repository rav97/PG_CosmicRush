using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using UnityEngine;

static class Cryptography
{
    private static AesCryptoServiceProvider aes;

    private static void AesParameters()
    {
        aes = new AesCryptoServiceProvider();
        aes.BlockSize = 128;
        aes.KeySize = 128;
        aes.IV = Encoding.UTF8.GetBytes("RafalIsbrandtBSI");
        aes.Key = Encoding.UTF8.GetBytes("PoliTeChnika2020");
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
    }
    public static byte[] Encrypt(string plainText)
    {
        AesParameters();
        byte[] encrypted;
        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
        }
        return encrypted;
    }
    public static string Decrypt(byte[] cipherText)
    {
        AesParameters();
        string plaintext;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
        }
        return plaintext;
    }
    public static void EncryptAndSave(string plainText, string location)
    {
        try
        {
            byte[] cipherText = Encrypt(plainText);
            File.WriteAllBytes(location, cipherText);
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured: " + e);
        }
    }
    public static string ReadAndDecrypt(string location)
    {
        try
        {
            byte[] cipherText = File.ReadAllBytes(location);
            return Decrypt(cipherText);
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured: " + e);
            return "";
        }
    }
}
