using static PasswordHash.ConsoleHelper;

namespace PasswordHash
{
    public class Pbkdf2Test
    {
        const int ITERATIONS = 10000;

        private static string CreateSalt()
        {
            var salt = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        private static string HashPassword(string password, string salt, HashAlgorithmName algorithm)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2
            (
                password: password,
                salt: Encoding.ASCII.GetBytes(salt), 
                iterations: ITERATIONS, 
                hashAlgorithm: algorithm, 
                outputLength: 32
            );

            return Convert.ToBase64String(hash);
        }

        private static bool VerifyHash(string password, string hash, string salt, HashAlgorithmName algorithm)
        {
            var hashPassword = HashPassword(password, salt, algorithm);

            var hash1 = Encoding.ASCII.GetBytes(hashPassword);
            var hash2 = Encoding.ASCII.GetBytes(hash);

            var difference = (uint)hash1.Length ^ (uint)hash2.Length;

            for (int i = 0; i < hash1.Length && i < hash2.Length; i++)
            {
                difference |= (uint)(hash1[i] ^ hash2[i]);
            }

            return difference == 0;
        }

        public static Task Test(string password, HashAlgorithmName algorithm)
        {
            return Task.Run(() =>
            {
                Stopwatch watch = new();

                Header($"PBKDF2 {algorithm}");
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
