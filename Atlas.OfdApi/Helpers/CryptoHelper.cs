using System;
using System.IO;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Operators;

namespace Atlas.OfdApi.Helpers
{
    public static class CryptoHelper
    {
        public static byte[] EncodeData(byte[] data, X509Certificate certificate)
        {
            var generator = new CmsSignedDataGenerator();
            generator.AddCertificate(certificate);

            var encoded = generator.Generate(new CmsProcessableByteArray(data), false);
            return encoded.GetEncoded();
        }

        public static Pkcs10CertificationRequest ParseCsrFile(string file)
        {
            var content = File.ReadAllText(file)
                .Replace("-----BEGIN CERTIFICATE REQUEST-----", "")
                .Replace("-----END CERTIFICATE REQUEST-----", "")
                .Replace("\n", "");

            return ParseCsrPemContent(content);
        }

        public static Pkcs10CertificationRequest ParseCsrPemContent(string content)
        {
            return new Pkcs10CertificationRequest(Convert.FromBase64String(content));
        }

        public static X509Certificate GetCertificate(Pkcs10CertificationRequest csr, AsymmetricKeyParameter privateKey)
        {
            var notBefore = DateTime.UtcNow.AddDays(-1);
            var notAfter  = DateTime.UtcNow.AddDays(7);
            var serialNum = DateTime.UtcNow.Ticks.ToString();

            var certGen = new X509V3CertificateGenerator();
            certGen.SetSerialNumber(new BigInteger(serialNum));
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
            var pemReader  = new PemReader(fileStream);
            return (AsymmetricKeyParameter)pemReader.ReadObject();
        }
    }
}
