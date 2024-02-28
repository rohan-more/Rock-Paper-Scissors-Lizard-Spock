using RPSLS.Core;
using RPSLS.UI;
using UnityEngine;
using System;
using System.Collections;

namespace RPSLS.Controllers
{
    public enum GameState { StartRound, WaitForPlayerInput, 
        CalculateComputerChoice, DeclareResults, TimeOver, RoundOver, GameOver }

    public class GameController : MonoBehaviour
    {
        private static RulesManager rulesManager = new RulesManager();
        private ComputerPlayer computer;
        private ElementType playerChoice;
        private ElementType computerChoice;
        public GameState currentState;
        private bool hasPlayerChosen, hasComputerChosen;
        [SerializeField] private UIController uiController;
        public RulesetObject ruleSet;

        private float shuffleTime = 1.5f;
        private float startGameDelay = 2f;
        void Start()
        {
            currentState = GameState.RoundOver;
            computer = new ComputerPlayer();
            uiController.ToggleComputerPlayedTextVisibility(false);
            rulesManager.CreateDynamicRuleSet(ruleSet);
        }

        private void OnEnable()
        {
            Events.SelectedElement += SetSelectedElement;
            Events.StartGame += StartGame;
            Events.EndGame += EndGame;
            Events.OnBack += OnBack;
        }

        private void OnDisable()
        {
            Events.SelectedElement -= SetSelectedElement;
            Events.StartGame -= StartGame;
            Events.EndGame -= EndGame;
            Events.OnBack -= OnBack;
        }

        private void EndGame()
        {
            currentState = GameState.RoundOver;
        }
        private void OnBack()
        {
            currentState = GameState.GameOver;
        }

        private void StartGame()
        {
            ScoreController.ResetScore();
           StartCoroutine(AddDelay(startGameDelay));
        }

        public IEnumerator AddDelay(float seconds) // I usually used Cysharp.Unitask for such tasks
        {
            yield return new WaitForSeconds(seconds);
            currentState = GameState.StartRound;
        }

        public IEnumerator ShuffleElements(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            currentState = GameState.WaitForPlayerInput;
        }


        private void SetSelectedElement(ElementType element)
        {
            if (element == ElementType.Random)
            {
                element = rulesManager.GetRandomElement();
            }
            playerChoice = element;
            hasPlayerChosen = true;
        }

        private void StartRound()
        {
            currentState = GameState.StartRound;
            uiController.ToggleComputerPlayedTextVisibility(false);
            playerChoice = ElementType.Rock; 
            computerChoice = ElementType.Rock;
            uiController.UpdateComputerChoiceText(string.Empty);
            uiController.UpdateHighScoreText(ScoreController.HighScore.ToString());
            uiController.ToggleResultsTextVisibility(false);
            currentState = GameState.CalculateComputerChoice;
            StartCoroutine(ShuffleElements(shuffleTime));
        } 

        private void DeclareResults()
        {
            string result = rulesManager.GetMatchResult(playerChoice, computerChoice);

            currentState = rulesManager.CurrentState == WinState.ComputerWon ? GameState.GameOver : GameState.RoundOver;
            uiController.UpdateResultsText(result);
            uiController.ToggleResultsTextVisibility(true);
            uiController.UpdateScoreText();
            uiController.UpdateHighScoreText(SaveDataManager.GetKey(Constants.HIGH_SCORE_KEY).ToString());
            if (currentState != GameState.GameOver)
            {
                Invoke(nameof(StartRound), startGameDelay);
            }
        }

        private void TimeOver()
        {
            string result = rulesManager.OnTimeOver();
            uiController.UpdateResultsText(result);
            uiController.ToggleResultsTextVisibility(true);
        }

        private void UpdateGameState()
        {
            switch (currentState)
            {
                case GameState.StartRound:
                    StartRound();
                    break;
                case GameState.WaitForPlayerInput:
                    if (hasPlayerChosen)
                    {
                        currentState = GameState.DeclareResults;
                    }
                    break;
                case GameState.CalculateComputerChoice:
                    computerChoice = computer.GetRandomMove();
                    uiController.UpdateComputerChoiceText(computerChoice.ToString().ToUpper());
                    uiController.ToggleComputerPlayedTextVisibility(true);
                    break;
                case GameState.TimeOver:
                    TimeOver();
                    break;
                case GameState.DeclareResults:
                    hasPlayerChosen = false;
                    DeclareResults();
                    break;
            }
        }


        void Update()
        {
            UpdateGameState();
        }

    }
}

