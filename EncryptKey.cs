using RedundantFileSearch.Properties;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptKey
{
    static public string Enc(string src, string allKey)
    {
        var keys = GetKeys(allKey);
        return MyEncrypt(src, keys.Item1, keys.Item2);
    }

    static public string Dec(string src, string allKey)
    {
        var keys = GetKeys(allKey);
        return MyDecrypt(src, keys.Item1, keys.Item2);
    }


    private static Tuple<byte[], byte[]> GetKeys(string allKey)
    {
        var key = Encoding.UTF8.GetBytes(allKey.Substring(0, 32));
        var iv = Encoding.UTF8.GetBytes(allKey.Substring(32, 16));
        return Tuple.Create(key, iv);
    }

    static private string MyEncrypt(string plain_text, byte[] key, byte[] iv)
    {
        string encrypted_str;

        using (Aes aes = Aes.Create())
        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
        using (MemoryStream out_stream = new MemoryStream())
        using (CryptoStream cs = new CryptoStream(out_stream, encryptor, CryptoStreamMode.Write))
        {
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plain_text);
            }
            encrypted_str = Convert.ToBase64String(out_stream.ToArray());
        }

        return encrypted_str;
    }

    static private string MyDecrypt(string base64_text, byte[] key, byte[] iv)
    {
        string plain_text;

        // Base64文字列をバイト型配列に変換
        byte[] cipher = Convert.FromBase64String(base64_text);

        using (Aes aes = Aes.Create())
        using (ICryptoTransform decryptor = aes.CreateDecryptor(key, iv))
        using (MemoryStream in_stream = new MemoryStream(cipher))
        using (CryptoStream cs = new CryptoStream(in_stream, decryptor, CryptoStreamMode.Read))
        using (StreamReader sr = new StreamReader(cs))
        {
            plain_text = sr.ReadToEnd();
        }

        return plain_text;
    }
}
