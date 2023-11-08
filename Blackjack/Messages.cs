namespace Blackjack
{
    internal class Messages
    {
        public static void ErrorMessage(string message)
        {
            // Formatting text
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

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
            Console.WriteLine($"{message}\n");

            // Returning console text to normal
            Console.ResetColor();
        }
    }
}
