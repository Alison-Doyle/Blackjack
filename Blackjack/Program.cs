using static System.Console;

namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating players
            Player dealer = new Player("Dealer");
            Player user = new Player(CreateUser());

            // Start and continue game
            bool continuePlaying;

            do
            {
                continuePlaying = CheckIfUserWantsToPlayAgain();
            }
            while (continuePlaying == true);

            WriteLine("End");
        }

        static string CreateUser()
        {
            string name;
            bool validName;

            do
            {
                // Getting user to input a name
                Write("Please enter your name:\t");
                name = ReadLine();

                // Validating they put in a name
                if (!String.IsNullOrWhiteSpace(name))
                {
                    validName = true;
                } 
                else
                {
                    validName = false;
                    WriteLine("ERROR: No name entered. Please enter an name before continuing.");
                }
            }
            while (validName == false);

            return name;
        }
        
        static bool CheckIfUserWantsToPlayAgain()
        {
            bool playAgain = false;
            bool validInput;

            do
            {
                string input;
                WriteLine("Would you like to play again? (Y = Yes, N = No)");
                input = ReadLine();

                // Validating entry
                if ((!String.IsNullOrWhiteSpace(input)) && (input.ToUpper() == "N") || (input.ToUpper() == "NO") || (input.ToUpper() == "Y") || (input.ToUpper() == "YES"))
                {
                    validInput = true;
                    if ((input.ToUpper() == "N") || (input.ToUpper() == "NO"))
                    {
                        playAgain = false;
                    }
                    else
                    {
                        playAgain = true;
                    }
                }
                else
                {
                    validInput = false;
                    WriteLine("ERROR: Invalid option entered. Please follow the instructions on screen");
                }
            }
            while (validInput == false);

            return playAgain;
        }
    }
}