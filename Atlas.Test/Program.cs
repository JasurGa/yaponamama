using Atlas.OfdApi.Helpers;
using System;
using System.IO;
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

            var message = File.ReadAllBytes("C:\\Users\\User\\Desktop\\test\\data.txt");
            var encoded = CryptoHelper.EncodeData(message, certificate);

            using (var file = File.OpenWrite("C:\\Users\\User\\Desktop\\test\\test_atlas.bin"))
            {
                file.Write(encoded);
            }
        }
    }
}

