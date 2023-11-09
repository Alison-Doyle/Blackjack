using static Blackjack.Messages;

namespace Blackjack
{
    internal class PlayerRecords
    {
        // Attributes
        const string FilePath = "../../../../PlayerData.txt";

        // Constructors

        // Methods
        public void FetchData()
        {
            string[] fileContents;

            try
            {
                fileContents = File.ReadAllLines(FilePath);

                for (int i = 0; i < fileContents.Length; i++)
                {
                    string[] currentLine = fileContents[i].Split(',');
                }

            }
            catch (Exception fileException)
            {
                ErrorMessage(fileException.Message);
            }
        }
    }
}
