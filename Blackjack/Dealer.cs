namespace Blackjack
{
    internal class Dealer : Player
    {
        // Attributes

        // Constructors
        public Dealer() : base()
        {
            Name = "Dealer";
        }

        // Methods
        public override bool StickOrTwist()
        {
            bool givePlayerAnotherCard;

            if (Score < 17)
            {
                givePlayerAnotherCard = true;
            }
            else
            {
                givePlayerAnotherCard = false;
            }

            return givePlayerAnotherCard;
        }
    }
}
