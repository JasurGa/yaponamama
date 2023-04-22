using Atlas.OfdApi.Helpers;
using System;
using System.Text;

namespace Atlas.Test
{
    internal class Program
    {
        public static void Main()
        { 
            var privateKey  = CryptoHelper.ParseKeyFile("C:\\Users\\User\\Desktop\\OFD\\user1.key");
            var certRequest = CryptoHelper.ParseCsrFile("C:\\Users\\User\\Desktop\\OFD\\user1.csr");
            var certificate = CryptoHelper.GetCertificate(certRequest, privateKey);

            var message = Encoding.UTF8.GetBytes("Hello, world!");
            var encoded = Encoding.UTF8.GetString(CryptoHelper.EncodeData(message, certificate));
            
            Console.WriteLine(encoded);
        }
    }
}

