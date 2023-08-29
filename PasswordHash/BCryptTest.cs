using static PasswordHash.ConsoleHelper;

namespace PasswordHash
{
    public class BCryptTest
    {
        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
        }

        private static bool VerifyHash(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public static Task Test(string password)
        {
            return Task.Run(() =>
            {
                Stopwatch watch = new();

                Header("BCrypt");
                SubTitle("Criando Hash");

                watch.Start();

                string hash = HashPassword(password);

                Write("Hash", hash);

                watch.Stop();

                Write("Tempo total", $"{watch.ElapsedMilliseconds} milissegundos");
                SubTitle("Verificando Senha");

                watch.Restart();

                bool equal = VerifyHash(password, hash);

                if (!equal) throw new Exception("Erro ao verificar senha.");

                watch.Stop();

                Write("Tempo total", $"{watch.ElapsedMilliseconds} milissegundos");
            });
        }
    }
}