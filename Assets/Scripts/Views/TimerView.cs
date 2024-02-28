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
        private float totalTime = 25f;
        [SerializeField] private Image radialFillImage;
        [SerializeField] private TMP_Text timerText;
        private float currentTime;
        public GameController gameController;
        void Start()
        {
            //currentTime = totalTime;
        }

        private void OnEnable()
        {
            currentTime = totalTime;
            radialFillImage.fillAmount = 1;
            timerText.text = string.Format(Constants.DECIMAL_ONE, currentTime);
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
                    timerText.text = string.Format(Constants.DECIMAL_ONE, currentTime);
                    if (currentTime <= 0)
                    {
                        gameController.currentState = GameState.TimeOver;
                    }
                    break;
            }
        }
    }
}

