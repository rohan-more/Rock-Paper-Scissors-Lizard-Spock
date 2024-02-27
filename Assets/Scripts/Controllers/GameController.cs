using RPSLS.Core;
using RPSLS.UI;
using UnityEngine;

namespace RPSLS.Controllers
{
    public enum GameState { StartRound, WaitForPlayerInput, CalculateComputerChoice, DeclareResults, TimeOver, StartTimer }

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
            computer = new ComputerPlayer();
        }

        private void OnEnable()
        {
            Events.SelectedElement += SetSelectedElement;
        }

        private void OnDisable()
        {
            Events.SelectedElement -= SetSelectedElement;
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
            uiController.UpdateResultsText(result);
            uiController.ToggleResultsTextVisibility(true);
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
                    rulesManager.AddDelay(2f);
                    computerChoice = computer.GetRandomMove();
                    uiController.UpdateComputerChoiceText(computerChoice.ToString().ToUpper());
                    currentState = GameState.WaitForPlayerInput;
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

