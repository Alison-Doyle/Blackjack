using static System.Console;
using static Blackjack.Messages;

namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Welcome user to game/application
            GameWelcome();

            // Creating players
            Dealer dealer = new Dealer();
            Player user = new Player(CreateUser());

            // Start game
            GameLoop(user, dealer);

            // Let user know application ha ended
            InformationMessage("Game ended. Thanks for playing!");
        }

        static void GameWelcome()
        {
            // Formatting text
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            // Write error
            Console.WriteLine("\n---{ WELCOME TO BLACKJACK }---\n");

            // Returning console text to normal
            Console.ResetColor();
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

        static void GameLoop(Player user, Player dealer)
        {
            List<Player> players = new List<Player>() { user, dealer };
            const int NumberOfCardInHandAtBeginning = 2;
            bool continuePlaying = false;

            do
            {
                // Create playing deck
                List<Card> playingDeck = CreatePlayingDeck();
                int currentCardIndex = 0;

                for (int i = 0; i < players.Count; i++)
                {
                    // NOTE: Messages seem to be thrown off by \n so using black WriteLine()s for spacing
                    WriteLine();
                    TurnMessage(players[i].Name);

                    // Initialise player's hand
                    for (int j = 0; j < NumberOfCardInHandAtBeginning; j++)
                    {
                        players[i].ReceiveCard(playingDeck[currentCardIndex]);
                        WriteLine($"{players[i].Name} has received {playingDeck[currentCardIndex]}");
                        currentCardIndex++;
                    }

                    // Let user know initial status of player
                    InformationMessage($"{players[i].Name}'s initial hand is worth {players[i].Score}");

                    // Let user stick or twist
                    while (players[i].StickOrTwist())
                    {
                        players[i].ReceiveCard(playingDeck[currentCardIndex]);
                        WriteLine($"{players[i].Name} has received {playingDeck[currentCardIndex]}");
                        currentCardIndex++;
                    }

                    // Update user on current status of player
                    InformationMessage($"{players[i].Name}'s score is {players[i].Score}");
                    if (players[i].Score > 21)
                    {
                        InformationMessage($"{players[i].Name} has gone bust! (Their score has gone over 21)");
                    }
                }

                // Find and display winner
                GetWinners(players);

                continuePlaying = CheckIfUserWantsToPlayAgain();

                // Reset scores incase user wants to play again
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].ResetScore();
                }

            }
            while (continuePlaying == true);
        }

        static List<Card> CreatePlayingDeck()
        {
            const int CardsInDeck = 52;
            List<Card> deck = new List<Card>();

            // Populating deck w/ defined number of cards
            for (int i = 0; i < CardsInDeck; i++)
            {
                bool cardExists = false;
                Card newCard;

                do
                {
                    // Create card
                    newCard = new Card();

                    // Check if card exists
                    for (int j = 0; j < deck.Count; j++)
                    {
                        if ((deck[j].Suit == newCard.Suit) && (deck[j].FaceValue == newCard.FaceValue))
                        {
                            cardExists = true;
                        }
                        else
                        {
                            cardExists = false;
                        }
                    }
                }
                while (cardExists == true);

                // Add card to deck
                deck.Add(newCard);
            }

            return deck;
        }

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

        static void GetWinners(List<Player> players)
        {
            string winnersName = "";
            int numberOfWinners = 0;

            // Sort scores w/ highest 1st (will work w/ any amount of players and increases scalability)
            players.Sort();

            int index = 0;
            do
            {
                if (index > players.Count)
                {
                    numberOfWinners = 2;
                }
                else if (players[index].Score <= 21)
                {
                    winnersName = players[index].Name;
                    numberOfWinners++;
                }

                index++;
            }
            while (numberOfWinners == 0);


            // Print result
            if (numberOfWinners >= 2)
            {
                InformationMessage("Draw!");
            }
            else
            {
                InformationMessage($"{winnersName} Wins!");
            }
        }
    }
}