using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static hidden_file.Hash;

namespace hidden_file
{
    internal class Hash
    {
        public class PasswordHasher 
        {
            private const int KeySize = 32; // 256 bit
            private const int Iterations = 1000;
            private static readonly byte[] StaticSalt = Encoding.UTF8.GetBytes("Rasol1324");





            public string Hash(string password)
            {
                using var algorithm = new Rfc2898DeriveBytes(password, StaticSalt, Iterations, HashAlgorithmName.MD5);
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var saltBase64 = Convert.ToBase64String(StaticSalt);
                return $"{Iterations}.{saltBase64}.{key}";
            }


        }
    }
}
