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
            Console.WriteLine("Error: " + message);

            // Returning console text to normal
            Console.ResetColor();
        }

        public static void TurnMessage(string playerName)
        {
            // Formatting text
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            // Write error
            Console.WriteLine($"\n{playerName} is playing");

            // Returning console text to normal
            Console.ResetColor();
        }
    }
}
