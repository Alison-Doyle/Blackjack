using static System.Console;
using static Blackjack.Messages;

namespace Blackjack
{
    internal class Player
    {
        // Attributes
        public string Name { get; set; }
        public int Score { get; private set; }

        // Constructors
        public Player() { }

        public Player(string name)
        {
            Name = name;
        }

        // Methods
        public virtual bool StickOrTwist()
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

        public void ReceiveCard(Card cardReceived)
        {
            if (cardReceived.FaceValue == "Ace")
            {
                if ((Score + 11) < 21)
                {
                    Score += 11;
                }
                else
                {
                    Score += 1;
                }
            }
            else if ((cardReceived.FaceValue == "Jack") || (cardReceived.FaceValue == "Queen") || (cardReceived.FaceValue == "King")) 
            {
                Score += 10;
            }
            else
            {
                Score += Convert.ToInt32(cardReceived.FaceValue);
            }
        }

        public void ResetScore()
        {
            Score = 0;
        }

        public override string ToString()
        {
            return $"Player Name:\t{Name}\nScore:\t\t{Score}";
        }
    }
}
