using RPSLS.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPSLS.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text computerChoiceText;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private TMP_Text scoreText;
        public TMP_Text gameStateText;
        [SerializeField] private Button startGameBtn;
        [SerializeField] private Button backBtn;

        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject gamePanel;

        private void OnEnable()
        {
            startGameBtn.onClick.AddListener(OnPlayClicked);
            backBtn.onClick.AddListener(OnBackClicked);
            Events.EndGame += OnBackClicked;
        }

        private void OnDisable()
        {
            startGameBtn.onClick.RemoveListener(OnPlayClicked);
            backBtn.onClick.RemoveListener(OnBackClicked);
            Events.EndGame -= OnBackClicked;
        }



        private void OnPlayClicked()
        {
            startPanel.SetActive(false);
            gamePanel.SetActive(true);
            Events.StartGame?.Invoke();
        }
        private void OnBackClicked()
        {
            startPanel.SetActive(true);
            gamePanel.SetActive(false);
        }

        public void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + ScoreController.PlayerScore.ToString();
            }
        }

        public void UpdateComputerChoiceText(string value)
        {
            if (computerChoiceText != null)
            {
                computerChoiceText.text = value;
            }
        }

        public void UpdateResultsText(string value)
        {
            if (resultText != null)
            {
                resultText.text = value;
            }
        }

        public void ToggleResultsTextVisibility(bool value)
        {
            if (resultText != null)
            {
                resultText.gameObject.SetActive(value);
            }
        }
    }
}

