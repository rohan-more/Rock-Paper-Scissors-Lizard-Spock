using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreController
{
    private static  int playerScore = 0;
    private static int highScore = 0;

    public static int PlayerScore { get => playerScore; }
    public static int HighScore { get => highScore; }

    public static void IncreaseScore()
    {
        playerScore++;
        if (PlayerScore > HighScore)
        {
            highScore = PlayerScore;
        }
    }

    public static void ResetScore()
    {
        playerScore = 0;
    }
}
