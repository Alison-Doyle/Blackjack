namespace Blackjack
{
    internal class Messages
    {
        public static void ErrorMessage(string message)
        {
            // Formatting text
            Console.ForegroundColor = ConsoleColor.Red;

            // Write error
            Console.Write($"Error: {message}");

            // Returning console text to normal
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void TurnMessage(string playerName)
        {
            // Formatting text
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            // Write whose turn it is
            Console.Write($"{playerName} is playing");

            // Returning console text to normal
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void InformationMessage(string message)
        {
            // Formatting text
            Console.ForegroundColor = ConsoleColor.Green;

            // Write message
            Console.WriteLine($"{message}");

            // Returning console text to normal
            Console.ResetColor();
        }

        public static void BoldInformationMessage(string message)
        {
            // Spacer in regular console styling
            Console.WriteLine();

            // Formatting text
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            // Write message
            Console.Write($"-----<[ {message} ]>-----");

            // Returning console text to normal
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
