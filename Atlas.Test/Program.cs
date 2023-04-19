using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Operators;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Atlas.Test
{
    internal class Program
    {
        public static void Main(string[] args)
        { 
            var key         = ParseKeyFile("C:\\Users\\User\\Desktop\\OFD\\user1.key");
            var request     = ParseCsrFile("C:\\Users\\User\\Desktop\\OFD\\user1.csr");
            var certificate = GetCertificate(request, key);

            var data = "Hello, world!";

            var gen = new CmsSignedDataGenerator();
            gen.AddCertificate(certificate);
            var encoded = gen.Generate(new CmsProcessableByteArray(Encoding.UTF8.GetBytes(data)), false);
            Console.WriteLine(Encoding.UTF8.GetString(encoded.GetEncoded()));
        }

        public static Pkcs10CertificationRequest ParseCsrFile(string file)
        {
            var content = File.ReadAllText(file);
            content = content.Replace("-----BEGIN CERTIFICATE REQUEST-----", "");
            content = content.Replace("-----END CERTIFICATE REQUEST-----", "");
            content = content.Replace("\n", "");

            return new Pkcs10CertificationRequest(Convert.FromBase64String(content));
        }

        public static X509Certificate GetCertificate(Pkcs10CertificationRequest csr, AsymmetricKeyParameter privateKey)
        {
            var notBefore = DateTime.UtcNow;
            var notAfter  = DateTime.UtcNow.AddDays(7);

            var certGen = new X509V3CertificateGenerator();
            certGen.SetSerialNumber(new BigInteger(DateTime.UtcNow.Ticks.ToString()));
            certGen.SetIssuerDN(csr.GetCertificationRequestInfo().Subject);
            certGen.SetSubjectDN(csr.GetCertificationRequestInfo().Subject);
            certGen.SetNotBefore(notBefore);
            certGen.SetNotAfter(notAfter);
            certGen.SetPublicKey(csr.GetPublicKey());
            
            return certGen.Generate(new Asn1SignatureFactory("SHA256WithRSAEncryption", privateKey));
        }

        public static AsymmetricKeyParameter ParseKeyFile(string file)
        {
            var fileStream = File.OpenText(file);
            var pemReader = new PemReader(fileStream);
            var result = pemReader.ReadObject();

            return (AsymmetricKeyParameter)result;
        }
    }
}

