using static Blackjack.Messages;

namespace Blackjack
{
    internal class FileHandling
    {
        // Attributes
        const string FilePath = "../../../../PlayerData.txt";

        // Methods
        public static List<GameRecord> FetchDataFromCsv()
        {
            List<GameRecord> records = new List<GameRecord>();
            string[] fileContents;

            try
            {
                fileContents = File.ReadAllLines(FilePath);

                for (int i = 0; i < fileContents.Length; i++)
                {
                    // Split data into variables
                    string[] currentLine = fileContents[i].Split(",");

                    // Create record if the line isn't the header
                    if (i != 0)
                    {
                        records.Add(new GameRecord(currentLine[0], currentLine[1], currentLine[2]));
                    }
                }

            }
            catch (Exception fileException)
            {
                ErrorMessage(fileException.Message);
            }

            return records;
        }

        public static void SaveGameDetailsToCsv(string playerName, string result)
        {
            string gameData = $"\n{playerName},{DateOnly.FromDateTime(DateTime.Now)},{result}";

            try
            {
                File.AppendAllText(FilePath, gameData);
            }
            catch (Exception exception) 
            {
                ErrorMessage(exception.Message);
            }
        }
    }
}
