using Konscious.Security.Cryptography;

using static PasswordHash.ConsoleHelper;

namespace PasswordHash
{
    public class Argon2Test
    {
        private static string CreateSalt()
        {
            var buffer = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        private static string HashPassword(string password, string salt)
        {
            using Argon2id argon2 = new(Encoding.UTF8.GetBytes(password))
            {
                Salt = Convert.FromBase64String(salt),
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 64
            };

            return Convert.ToBase64String(argon2.GetBytes(32));
        }

        private static bool VerifyHash(string password, string hash, string salt)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }

        public static Task Test(string password)
        {
            return Task.Run(() =>
            {
                Stopwatch watch = new();

                Header("ARGON2");
                SubTitle("Criando Hash");

                watch.Start();

                string salt = CreateSalt();

                Write("Salt", salt);

                string hash = HashPassword(password, salt);

                Write("Hash", hash);

                watch.Stop();

                Write("Tempo total", $"{watch.ElapsedMilliseconds} milissegundos");
                SubTitle("Verificando Senha");

                watch.Restart();

                bool equal = VerifyHash(password, hash, salt);

                if (!equal) throw new Exception("Erro ao verificar senha.");

                watch.Stop();

                Write("Tempo total", $"{watch.ElapsedMilliseconds} milissegundos");
            });
        }
    }
}
