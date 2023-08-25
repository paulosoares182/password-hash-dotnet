namespace PasswordHash
{
    internal static class ConsoleHelper
    {
        const char character = ' ';
        const int length = 80;

        internal static void Init(string password)
        {
            ResetColors();

            Console.ForegroundColor = ConsoleColor.Red;

            string separator = string.Empty;

            for (int count = 0; count < length; count++)
                separator += "-";

            Console.WriteLine(separator);

            password = $"[SENHA]: {password}";

            int maxAroundText = (length - password.Length) / 2;

            for (int count = 0; count < maxAroundText; count++)
                Console.Write(" ");

            Console.Write($"{password}");

            for (int count = 0; count < maxAroundText; count++)
                Console.Write(" ");

            Console.WriteLine($"\n{separator}");
        }

        internal static void Header(string title, char character = character, int maxLength = length)
        {
            const int spaces = 2;
            maxLength -= spaces;

            int maxAroundTitle = (maxLength - title.Length) / 2;

            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int count = 0; count < maxAroundTitle; count++)
                Console.Write(character);

            Console.Write($" {title} ");

            for (int count = 0; count < maxAroundTitle; count++)
                Console.Write(character);

            ResetColors();

            Console.WriteLine();
        }

        internal static void Write(string display, string value)
        {
            //Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("\t");
            Console.Write($"[{display.ToUpper()}]:");

            ResetColors();

            Console.Write($" {value}");
            Console.WriteLine();
        }

        internal static void SubTitle(string text, int maxLength = length)
        {
            const int additionalCharacters = 4;

            maxLength -= (text.Length + additionalCharacters);

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[-]");
            Console.Write($" {text}");


            for (int count = 0; count < maxLength; count++)
                Console.Write(" ");

            ResetColors();

            Console.WriteLine();
        }

        internal static void ResetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
