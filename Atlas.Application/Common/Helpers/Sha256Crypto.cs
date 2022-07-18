using System.Text;
using System.Security.Cryptography;

namespace Atlas.Application.Common.Helpers
{
    public static class Sha256Crypto
    {
        public static string GetHash(string s)
        {
            var sb = new StringBuilder();

            using (var hash = SHA256Managed.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(s));

                foreach (var b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString().ToUpper();
        }
    }
}
