namespace Blackjack
{
    internal class Player
    {
        // Attributes
        public string Name { get; set; }
        public int Score { get; set; }

        // Constructors
        public Player() { }

        public Player(string name)
        {
            Name = name;
        }

        // Methods
        public void ReceiveCards()
        {
            Random cardSelect = new Random();

            long card = cardSelect.NextInt64(1, 14);
            Console.WriteLine(card);
        }
    }
}
