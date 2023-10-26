namespace Blackjack
{
    internal class Card
    {
        // Attributes
        public string FaceValue { get; private set; }
        public string Suit { get; private set; }

        // Constructors
        public Card()
        {
            Suit = PickSuit();
            FaceValue = PickFaceValue();
        }

        // Methods
        static string PickSuit()
        {
            string suit;

            // Picking suit index
            Random pickSuit = new Random();
            int suitNumber = pickSuit.Next(1, 5);

            // Match index to name
            switch (suitNumber)
            {
                case 1:
                    suit = "Clubs";
                    break;
                case 2:
                    suit = "Hearts";
                    break;
                case 3:
                    suit = "Spades";
                    break;
                case 4:
                    suit = "Diamonds";
                    break;
                default:
                    suit = "";
                    break;
            }

            return suit;
        }

        static string PickFaceValue()
        {
            const int AceIndex = 1;
            const int JackIndex = 11;
            string faceValue;

            // Pick face value index
            Random cardSelect = new Random();
            long faceValueIndex = cardSelect.NextInt64(1, 14);

            // Matching index to name if necessary
            if ((faceValueIndex > AceIndex) && (faceValueIndex < JackIndex))
            {
                faceValue = faceValueIndex.ToString();
            } 
            else if (faceValueIndex >= JackIndex)
            {
                switch (faceValueIndex)
                {
                    case 11:
                        faceValue = "Jack";
                        break;
                    case 12:
                        faceValue = "Queen";
                        break;
                    case 13:
                        faceValue = "King";
                        break;
                    default:
                        faceValue = "";
                        break;
                }
            }
            else
            {
                faceValue = "Ace";
            }

            return faceValue;
        }

        public override string ToString()
        {
            return $"{FaceValue} of {Suit}";
        }
    }
}
