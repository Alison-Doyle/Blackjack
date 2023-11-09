namespace Blackjack
{
    internal class GameRecord
    {
        // Attributes
        public string PlayerName { get; private set; }
        public DateOnly DateOfGame { get; private set; }
        public string Result { get; private set; }

        // Constructors
        public GameRecord(string name, string date, string result)
        {
            PlayerName = name;
            DateOfGame = DateOnly.Parse(date);
            Result = result;
        }

        // Methods
        public override string ToString()
        {
            return $"Player Name: {PlayerName}; Date: {DateOfGame}; Result: {Result}";
        }
    }
}
