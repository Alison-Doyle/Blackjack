using static System.Console;
using static Blackjack.Messages;

namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.Unicode;

            // Welcome user to game/application
            BoldInformationMessage("♦♧♥♤ 𝐁𝐥𝐚𝐜𝐤𝐉𝐚𝐜𝐤 ♤♥♧♦");

            // Creating players
            Dealer dealer = new Dealer();
            Player user = new Player(CreateUser());

            // Application loop
            const int ExitPrompt = 3;
            int userChoice = 0;

            do
            {
                userChoice = SelectProgramAction();

                switch (userChoice)
                {
                    case 1:
                        GameLoop(user, dealer);
                        break;
                    case 2:
                        DisplayPlayerHistory(user.Name);
                        break;
                    default:
                        break;
                }
            }
            while (userChoice != ExitPrompt);

            // Let user know application ha ended
            InformationMessage("Application ended. See you soon!");
        }

        static int SelectProgramAction()
        {
            string[] MenuItems = { "Start Game", "History", "Exit" };

            // Print Menu
            BoldInformationMessage("Menu");
            for (int i = 0; i < MenuItems.Length; i++)
            {
                WriteLine($"{i+1}. {MenuItems[i]}");
            }

            // Getting input
            bool validInput;
            int userChoice = 0;

            do
            {
                // Get user input
                WriteLine("Please enter the number of the option you wish to select:");
                string input = ReadLine();

                // Give user option of reentering response until its valid
                if ((int.TryParse(input, out int selectedInput)) && (selectedInput <= MenuItems.Length) && (selectedInput > 0))
                {
                    validInput = true;
                    userChoice = selectedInput;
                }
                else
                {
                    validInput = false;
                    ErrorMessage("Invalid option entered. Please follow the instructions on screen");
                }
            }
            while (validInput == false);

            return userChoice;
        }

        #region Player History
        static void DisplayPlayerHistory(string playerName)
        {
            OutputEncoding = System.Text.Encoding.UTF8;

            const string TableFormatting = "{0,-20}{1,-20}{2,-5}";

            // Get game records
            List<GameRecord> gameRecords = FileHandling.FetchDataFromCsv();

            // Printing table
            BoldInformationMessage("History");
            WriteLine(TableFormatting, "Name", "Date", "Result");
            for (int i = 0; i < gameRecords.Count; i++)
            {
                if (gameRecords[i].PlayerName.ToUpper() == playerName.ToUpper())
                {
                    WriteLine(TableFormatting, gameRecords[i].PlayerName, gameRecords[i].DateOfGame, gameRecords[i].Result);
                }
            }

            // Print win percentage
            InformationMessage($"Your win percentage is {CalculateWinPercentage(gameRecords, playerName):f2}%;");
        }

        static double CalculateWinPercentage(List<GameRecord> gameRecords, string playerName)
        {
            double numberOfWins = 0;
            double numberOfGames = 0;

            // Count member of games and wins
            for (int i = 0; i < gameRecords.Count; i++)
            {
                if (gameRecords[i].PlayerName.ToUpper() == playerName.ToUpper())
                {
                    numberOfGames++;

                    if (gameRecords[i].Result == "Win")
                    {
                        numberOfWins++;
                    }
                }
            }

            // Calculation
            double winPercentage = (numberOfWins / numberOfGames) * 100;

            return winPercentage;
        }
        #endregion

        #region Game Logic
        static void GameLoop(Player user, Player dealer)
        {
            List<Player> players = new List<Player>() { user, dealer };
            const int NumberOfCardInHandAtBeginning = 2;
            bool continuePlaying = false;

            do
            {
                // Letting user know game has started
                BoldInformationMessage("Game Start");

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
            int highestScore = 0;
            string result;

            for (int i = 0; i < players.Count; i++)
            {
                // Filters out any players that have gone bust (if all have gone bust there will be no winners
                // and a draw)
                if (players[i].Score <= 21)
                {
                    // If only 1 person has the highest score, they will be the winner and their name will be
                    // recorded else there will be a draw
                    if (players[i].Score > highestScore)
                    {
                        highestScore = players[i].Score;
                        winnersName = players[i].Name;
                        numberOfWinners = 1;
                    }
                    else if (players[i].Score == highestScore)
                    {
                        numberOfWinners++;
                    }
                }
            }

            // Print result
            if ((numberOfWinners > 1) || (numberOfWinners == 0))
            {
                WriteLine();
                InformationMessage("Draw!");
                result = "Draw";
            }
            else
            {
                WriteLine();
                InformationMessage($"{winnersName} Wins!");
                if (winnersName.ToUpper() != "DEALER")
                {
                    result = "Win";
                }
                else
                {
                    result = "Loose";
                }
            }

            FileHandling.SaveGameDetailsToCsv(players[0].Name, result);
        }
        #endregion

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
    }
}