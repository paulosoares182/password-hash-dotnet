using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using static PasswordHash.ConsoleHelper;

namespace PasswordHash
{
    public class Pbkdf2AspNetTest
    {
        const int ITERATIONS = 10000;

        private static string CreateSalt()
        {
            var salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        private static string HashPassword(string password, string salt, HashAlgorithmName algorithm)
        {
            var hash = KeyDerivation.Pbkdf2
            (
                password: password,
                salt: Encoding.ASCII.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: ITERATIONS,
                numBytesRequested: 32
            );

            return Convert.ToBase64String(hash);
        }

        private static bool VerifyHash(string password, string hash, string salt, HashAlgorithmName algorithm)
        {
            var hashPassword = HashPassword(password, salt, algorithm);

            return hashPassword.Equals(hash);
        }

        private static bool VerifyHash(byte[] password, byte[] hash)
        {
            var difference = (uint)password.Length ^ (uint)hash.Length;

            for (int i = 0; i < password.Length && i < hash.Length; i++)
            {
                difference |= (uint)(password[i] ^ hash[i]);
            }

            return difference == 0;
        }

        public static Task Test(string password, HashAlgorithmName algorithm)
        {
            return Task.Run(() =>
            {
                Stopwatch watch = new();

                Header($"PBKDF2 ASPNET {algorithm}");
                SubTitle("Criando Hash");

                watch.Start();

                string salt = CreateSalt();

                Write("Salt", salt);

                string hash = HashPassword(password, salt, algorithm);

                Write("Hash", hash);

                watch.Stop();

                Write("Tempo total", $"{watch.ElapsedMilliseconds} milissegundos");
                SubTitle("Verificando Senha");

                watch.Restart();

                bool equal = VerifyHash(password, hash, salt, algorithm);

                if (!equal) throw new Exception("Erro ao verificar senha.");

                watch.Stop();

                Write("Tempo total", $"{watch.ElapsedMilliseconds} milissegundos");
            });
        }
    }
}
