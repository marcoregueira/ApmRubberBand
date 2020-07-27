using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace H2h.RubberBand.Server.ETag
{
    // credit: https://stackoverflow.com/a/40455004/2982757
    public static class ETagGenerator
    {
        public static string GetETag(string key, byte[] contentBytes)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var combinedBytes = Combine(keyBytes, contentBytes);

            return GenerateETag(combinedBytes);
        }

        private static string GenerateETag(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                string hex = BitConverter.ToString(hash);
                return hex.Replace("-", "");
            }
        }

        private static byte[] Combine(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, c, 0, a.Length);
            Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }
    }
}
