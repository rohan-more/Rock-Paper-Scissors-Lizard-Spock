using RPSLS.Core;
using RPSLS.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;

namespace RPSLS.Controllers
{
    public enum GameState { StartRound, WaitForPlayerInput, CalculateComputerChoice, DeclareResults, TimeOver, RoundOver }

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
           StartCoroutine(AddDelay(2f));
        }

        public IEnumerator AddDelay(float seconds)
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
            playerChoice = ElementType.Rock; 
            computerChoice = ElementType.Rock;
            uiController.UpdateComputerChoiceText(string.Empty);
            uiController.ToggleResultsTextVisibility(false);
            currentState = GameState.CalculateComputerChoice;
        } 

        private void DeclareResults()
        {
            string result = rulesManager.GetMatchResult(playerChoice, computerChoice);
            currentState = GameState.RoundOver;
            uiController.UpdateResultsText(result);
            uiController.ToggleResultsTextVisibility(true);
            uiController.UpdateScoreText();
            Invoke(nameof(StartRound), 2f);
        }

        private void TimeOver()
        {
            string result = rulesManager.OnTimeOver();
            uiController.UpdateResultsText(result);
            uiController.ToggleResultsTextVisibility(true);
        }


        void Update()
        {
            //uiController.gameStateText.text = currentState.ToString();
            Debug.Log(currentState.ToString());
            switch (currentState)
            {
                case GameState.StartRound:
                    StartRound();
                    Debug.Log(currentState.ToString());
                    break;
                case GameState.WaitForPlayerInput:
                    if(hasPlayerChosen)
                    {
                        currentState = GameState.DeclareResults;
                    }
                    Debug.Log(currentState.ToString());
                    break;
                case GameState.CalculateComputerChoice:
                    computerChoice = computer.GetRandomMove();
                    uiController.UpdateComputerChoiceText(computerChoice.ToString().ToUpper());
                    currentState = GameState.WaitForPlayerInput;
                    Debug.Log(currentState.ToString());
                    break;
                case GameState.TimeOver:
                    TimeOver();
                    Debug.Log(currentState.ToString());
                    break;
                case GameState.DeclareResults:
                    hasPlayerChosen = false;
                    DeclareResults();
                    Debug.Log(currentState.ToString());
                    break;
            }
        }

    }
}

