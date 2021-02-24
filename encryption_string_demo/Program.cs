using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Security.Cryptography;

namespace encryption_string_demo
{
    class Program
    {
        private static RSAParameters pubKey;
        private static RSAParameters privKey;
        static string CONTAINER_NAME = "MyContainerName";

        public enum KeySizes
        {
              SIZE_512 = 512
            , SIZE_1024 = 1024
            , SIZE_2048 = 2048
            , SIZE_952 = 952
            , SIZE_1369 = 1369
        }
        static void Main(string[] args)
        {
         
            string message = "The quick brown fox jumps over the lazy dog";
            generateKeys();
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(message));
            byte[] decrypted = Decrypt(encrypted);
            Console.WriteLine("Original\n\t" + message + "\n");
            Console.WriteLine("Encrypted\n\t" + BitConverter.ToString(encrypted).Replace("-","")+ "\n");
            Console.WriteLine("Decrypted\n\t" + Encoding.UTF8.GetString(decrypted));

            Console.ReadLine();
        }


        static void generateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                pubKey = rsa.ExportParameters(false);
                privKey = rsa.ExportParameters(true);
            }
        }

        static byte[] Encrypt(byte[] input)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(pubKey);
                encrypted = rsa.Encrypt(input, true);
            }

            return encrypted;
        }

        static byte[] Decrypt(byte[] input)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privKey);
                decrypted = rsa.Decrypt(input, true);
            }

            return decrypted;
        }


    }
}
