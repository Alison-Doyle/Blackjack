using static System.Console;
using static Blackjack.Messages;

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
            bool receiveAnotherCard;

            do
            {
                InitialiseDecks(user);

                TurnMessage(user.Name);

                // Let user stick or twist
                do
                {
                    //user.ReceiveCards();
                    Card test = new Card();
                    WriteLine(test.ToString());
                    receiveAnotherCard = CheckIfPlayerWantsToStickOrTwist(user);
                }
                while (receiveAnotherCard == true);

                TurnMessage(dealer.Name);

                continuePlaying = CheckIfUserWantsToPlayAgain();
            }
            while (continuePlaying == true);

            WriteLine("End");
        }

        static string CreateUser()
        {
            const string ComputerUserName = "DEALER";
            string name;
            bool validName;

            do
            {
                // Getting user to input a name
                Write("Please enter your name:\t");
                name = ReadLine();

                // Validating they put in a name and that its not reserved
                if (name.ToUpper() == ComputerUserName)
                {
                    validName = false;
                    ErrorMessage("That name is not available. Please select another name.");
                } 
                else if (!String.IsNullOrWhiteSpace(name))
                {
                    validName = true;
                }
                else
                {
                    validName = false;
                    ErrorMessage("No name was entered. Please enter a name before continuing.");
                }
            }
            while (validName == false);

            return name;
        }
        
        static void InitialiseDecks(Player user)
        {
            const int StartingNumberOfCards = 2;

            for (int i = 0; i < StartingNumberOfCards; i++)
            {
                user.ReceiveCards();
            }
        }

        // NOTE: Might want to move this to user player
        static bool CheckIfUserWantsToPlayAgain()
        {
            bool playAgain = false;
            bool validInput;

            do
            {
                // Get user input
                WriteLine("Would you like to play again? (Y = Yes, N = No)");
                string input = ReadLine();

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
                    ErrorMessage("Invalid option entered. Please follow the instructions on screen");
                }
            }
            while (validInput == false);

            return playAgain;
        }
    
        static bool CheckIfPlayerWantsToStickOrTwist(Player player)
        {
            bool playerWouldLikeAnotherCard = false;
            bool validInput;

            do
            {
                // Get user input
                WriteLine("Would you like to stick or twist?");
                string input = ReadLine();

                // Give user option of reentering response until its valid
                if ((!String.IsNullOrWhiteSpace(input)) && (input.ToUpper() == "T") || (input.ToUpper() == "TWIST") || (input.ToUpper() == "S") || (input.ToUpper() == "STICK"))
                {
                    validInput = true;
                    if ((input.ToUpper() == "S") || (input.ToUpper() == "STICK"))
                    {
                        playerWouldLikeAnotherCard = false;
                    }
                    else
                    {
                        playerWouldLikeAnotherCard = true;
                    }
                }
                else
                {
                    validInput = false;
                    ErrorMessage("Invalid option entered. Please follow the instructions on screen");
                }
            }
            while (validInput == false);

            return playerWouldLikeAnotherCard;
        }
    }
}