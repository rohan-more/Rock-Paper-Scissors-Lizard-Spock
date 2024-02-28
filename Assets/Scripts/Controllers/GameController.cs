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
        private bool hasPlayerChosen;
        [SerializeField] private UIController uiController;

        void Start()
        {
            currentState = GameState.RoundOver;
            computer = new ComputerPlayer();
            uiController.ToggleComputerPlayedTextVisibility(false);
        }

        private void OnEnable()
        {
            Events.SelectedElement += SetSelectedElement;
            Events.StartGame += StartGame;
            Events.EndGame += EndGame;
    
        }

        private void OnDisable()
        {
            Events.SelectedElement -= SetSelectedElement;
            Events.StartGame -= StartGame;
            Events.EndGame -= EndGame;
        }

        private void EndGame()
        {
            currentState = GameState.RoundOver;
        }

        private void StartGame()
        {
            ScoreController.ResetScore();
           StartCoroutine(AddDelay(2f));
        }

        public IEnumerator AddDelay(float seconds) // I usually used Cysharp.Unitask for such delays but for some reason it isnt importing correctly in my project
        {
            yield return new WaitForSeconds(seconds);
            currentState = GameState.StartRound;
        }


        private void SetSelectedElement(ElementType element)
        {
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
        } 

        private void DeclareResults()
        {
            string result = rulesManager.GetMatchResult(playerChoice, computerChoice);

            if(rulesManager.CurrentState == WinState.ComputerWon)
            {
                currentState = GameState.GameOver;
            }
            else
            {
                currentState = GameState.RoundOver;
            }
            uiController.UpdateResultsText(result);
            uiController.ToggleResultsTextVisibility(true);
            uiController.UpdateScoreText();
            uiController.UpdateHighScoreText(SaveDataManager.GetKey(Constants.HIGH_SCORE_KEY).ToString());
            if (currentState != GameState.GameOver)
            {
                Invoke(nameof(StartRound), 2f);
            }
        }

        private void TimeOver()
        {
            string result = rulesManager.OnTimeOver();
            uiController.UpdateResultsText(result);
            uiController.ToggleResultsTextVisibility(true);
        }


        void Update()
        {
            switch (currentState)
            {
                case GameState.StartRound:
                    StartRound();
                    break;
                case GameState.WaitForPlayerInput:
                    if(hasPlayerChosen)
                    {
                        currentState = GameState.DeclareResults;
                    }
                    break;
                case GameState.CalculateComputerChoice:
                    computerChoice = computer.GetRandomMove();
                    uiController.UpdateComputerChoiceText(computerChoice.ToString().ToUpper());
                    currentState = GameState.WaitForPlayerInput;
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

    }
}

