namespace SaveManager
{
    [System.Serializable]
    public class PlayerData
    {
        public int[] entryScores;
        public string[] entryNames;

        public int entryAmount;

        public PlayerData()
        {
            entryAmount = LeaderboardsManager.Instance.GetEntryList().Count;
            
            entryScores = new int[entryAmount];
            entryNames = new string[entryAmount];

            for (int i = 0; i <= entryAmount - 1; i++)
            {
                entryScores[i] = LeaderboardsManager.Instance.GetEntryList()[i].Score;
                entryNames[i] = LeaderboardsManager.Instance.GetEntryList()[i].EntryName;
            }
            
        }
    }
}
