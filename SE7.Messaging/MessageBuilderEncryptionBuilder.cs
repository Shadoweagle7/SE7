using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Messaging
{
    /*
     public class Cryptograph:ICryptograph
    {
        private string RsaHashAlgorithm { get; set; }

        public Cryptograph()
        {
            this.RsaHashAlgorithm = "SHA256";
        }

        public RSAParameters[] GenarateRSAKeyPairs()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                RSAParameters publicKey = rsa.ExportParameters(false);
                RSAParameters privateKey = rsa.ExportParameters(true);
                return new RSAParameters[]{ privateKey , publicKey };
            }
        }

        public byte[] SignRsaHashData(RSAParameters privateKey,byte[]hashOfDataToSign)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);

                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm(RsaHashAlgorithm);

                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

        public bool VerifyRsaSignature(RSAParameters publicKey,byte[]hashOfDataToSign, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(publicKey);

                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm(RsaHashAlgorithm);

                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }

    }
     */

    public class MessageBuilderEncryptionBuilder
    {
        private readonly MessageBuilder MessageBuilder;

        internal MessageBuilderEncryptionBuilder(MessageBuilder messageBuilder) => MessageBuilder = messageBuilder;

        private class RSAMessageBuilderEncryptionBuilder
        {
            public RSAMessageBuilderEncryptionBuilder()
            {

            }

            public RSAMessageBuilderEncryptionBuilder WithHashAlgorithm(string algorithm)
            {

            }
        }

        public RSAMessageBuilderEncryptionBuilder RSA()
        {
            using var rsa = new RSACryptoServiceProvider(4096);
            rsa.PersistKeyInCsp = false;
            var privateKey = rsa.ExportParameters(true);
            var publicKey = rsa.ExportParameters(false);

            return new RSAMessageBuilderEncryptionBuilder();
        }
    }
}
