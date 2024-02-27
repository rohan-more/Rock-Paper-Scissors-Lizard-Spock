using RPSLS.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPSLS.UI
{
    public class TimerView : MonoBehaviour
    {
        private float totalTime = 5f;
        [SerializeField] private Image radialFillImage;
        [SerializeField] private TMP_Text timerText;
        private float currentTime;
        public GameController gameController;
        void Start()
        {
            currentTime = totalTime;
        }

        void Update()
        {
            switch (gameController.currentState)
            {
                case GameState.RoundOver:
                    currentTime = totalTime;
                    break;
                case GameState.WaitForPlayerInput:
                    currentTime -= Time.deltaTime;
                    float fillAmount = currentTime / totalTime;
                    radialFillImage.fillAmount = fillAmount;
                    timerText.text = string.Format("{0:F1}", currentTime);
                    if (currentTime <= 0)
                    {
                        gameController.currentState = GameState.TimeOver;
                    }
                    break;
            }
        }
    }
}

