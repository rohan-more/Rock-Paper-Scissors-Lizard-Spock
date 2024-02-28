
namespace RPSLS.Core
{
    /// <summary>
    /// Controls the scoring system for the game 
    /// </summary>
    public static class ScoreController
    {
        private static int playerScore = 0;
        private static int highScore = 0;

        public static int PlayerScore { get => playerScore; }
        public static int HighScore { get => highScore; }

        public static void IncreaseScore()
        {
            playerScore++;
            if (PlayerScore > SaveDataManager.GetKey(Constants.HIGH_SCORE_KEY))
            {
                highScore = PlayerScore;
                SaveDataManager.SaveKey(Constants.HIGH_SCORE_KEY, HighScore);
            }
        }

        public static void ResetScore()
        {
            playerScore = 0;
        }
    }
}

