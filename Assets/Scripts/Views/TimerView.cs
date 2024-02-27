using RPSLS.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    private float totalTime = 2f; // Total time for the timer in seconds
    [SerializeField] private Image radialFillImage; // Reference to the radial fill image component
    [SerializeField] private TMP_Text timerText;
    private float currentTime; // Current time remaining
    public GameController gameController;
    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {

        if (gameController.currentState == GameState.WaitForPlayerInput)
        {
            currentTime -= Time.deltaTime;
            float fillAmount = currentTime / totalTime;
            radialFillImage.fillAmount = fillAmount;
            timerText.text = string.Format("{0:F1}", currentTime);
            if (currentTime <= 0)
            {
                gameController.currentState = GameState.TimeOver;
            }
        }

    }
}
