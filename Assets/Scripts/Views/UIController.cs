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
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private Button startGameBtn;
        [SerializeField] private Button backBtn;
        [SerializeField] private TMP_Text highScoreResultsText;
        [SerializeField] private TMP_Text currentScoreResultsText;
        [SerializeField] private Button exitBtn;

        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private GameObject computerPlayed;

        private void Start()
        {
            UpdateHighScoreText(SaveDataManager.GetKey(Constants.HIGH_SCORE_KEY).ToString());
        }

        private void OnEnable()
        {
            startGameBtn.onClick.AddListener(OnPlayClicked);
            backBtn.onClick.AddListener(OnBackClicked);
            exitBtn.onClick.AddListener(ExitToMainMenu);
            Events.EndGame += EndGame;
            ResetGameUI();
        }

        private void OnDisable()
        {
            startGameBtn.onClick.RemoveListener(OnPlayClicked);
            backBtn.onClick.RemoveListener(OnBackClicked);
            exitBtn.onClick.RemoveListener(ExitToMainMenu);
            Events.EndGame -= EndGame;
        }
        private void ExitToMainMenu()
        {
            OnBackClicked();
            ToggleResultsPanelVisibility(false);
        }

        private void EndGame()
        {
            StartCoroutine(AddDelay(2f));
        }

        private IEnumerator AddDelay(float secs)
        {
            yield return new WaitForSeconds(secs);
            ToggleResultsPanelVisibility(true);
            ShowResultsPanel();
        }


        private void OnPlayClicked()
        {
            startPanel.SetActive(false);
            gamePanel.SetActive(true);
            ResetGameUI();
            Events.StartGame?.Invoke();
        }
        private void OnBackClicked()
        {
            startPanel.SetActive(true);
            gamePanel.SetActive(false);
            Events.OnBack?.Invoke();
        }

        public void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = Constants.SCORE + ScoreController.PlayerScore.ToString();
            }
        }

        public void UpdateComputerChoiceText(string value)
        {
            if (computerChoiceText != null)
            {
                computerChoiceText.text = value;
            }
        }

        public void UpdateHighScoreText(string value)
        {
            if (highScoreText != null)
            {
                highScoreText.text = Constants.HIGH_SCORE + value;
            }
        }

        public void UpdateResultsText(string value)
        {
            if (resultText != null)
            {
                resultText.text = value;
            }
        }

        public void ToggleComputerPlayedTextVisibility(bool value)
        {
            if (computerPlayed != null)
            {
                computerPlayed.SetActive(value);
            }
            if (computerChoiceText != null)
            {
                computerChoiceText.gameObject.SetActive(value);
            }
        }

        public void ToggleResultsTextVisibility(bool value)
        {
            if (resultText != null)
            {
                resultText.gameObject.SetActive(value);
            }
        }

        public void ToggleResultsPanelVisibility(bool value)
        {
            if (resultsPanel != null)
            {
                resultsPanel.gameObject.SetActive(value);
            }
        }

        public void ShowResultsPanel()
        {
            var score = SaveDataManager.GetKey(Constants.HIGH_SCORE_KEY).ToString();
            ToggleResultsPanelVisibility(true);
            highScoreResultsText.text = score;
            currentScoreResultsText.text = ScoreController.PlayerScore.ToString();
            UpdateHighScoreText(score);
        }

        public void ResetGameUI()
        {
            scoreText.text = Constants.SCORE + "0";
            UpdateComputerChoiceText(string.Empty);
            ToggleComputerPlayedTextVisibility(false);
            ToggleResultsTextVisibility(false);
            ToggleResultsPanelVisibility(false);
        }
    }
}

