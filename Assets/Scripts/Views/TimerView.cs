using RPSLS.Controllers;
using RPSLS.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPSLS.UI
{
    public class TimerView : MonoBehaviour
    {
        private float totalTime;
        [SerializeField] private Image radialFillImage;
        [SerializeField] private TMP_Text timerText;
        private float currentTime;
        [SerializeField] private GameController gameController;

        private void OnEnable()
        {
            totalTime = gameController.ruleSet.Timer;
            currentTime = totalTime;
            radialFillImage.fillAmount = 1;
            timerText.text = string.Format(Constants.DECIMAL_ONE, currentTime);
        }

        private void UpdateTimer()
        {
            switch (gameController.currentState)
            {
                case GameState.RoundOver:
                case GameState.StartRound:
                    currentTime = totalTime;
                    break;
                case GameState.WaitForPlayerInput:
                    currentTime -= Time.deltaTime;
                    float fillAmount = currentTime / totalTime;
                    radialFillImage.fillAmount = fillAmount;
                    timerText.text = string.Format(Constants.DECIMAL_ONE, currentTime);
                    if (currentTime <= 0)
                    {
                        gameController.currentState = GameState.TimeOver;
                    }
                    break;
            }
        }

        void Update()
        {
            UpdateTimer();
        }
    }
}

