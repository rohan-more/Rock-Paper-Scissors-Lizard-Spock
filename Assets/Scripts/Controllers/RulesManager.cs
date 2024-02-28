using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPSLS.Core;
using System.Linq;
using System;
using Unity.VisualScripting;

namespace RPSLS.Controllers
{
    public enum WinState
    {
        PlayerWon, ComputerWon, Draw
    }
    public class RulesManager : IComparer<Core.ElementType>
    {
        private WinState currentState;
        public RulesManager() { }

        private static readonly List<Rule> RuleSet = new List<Rule>
        {
            new(Core.ElementType.Rock, Core.ElementType.Scissors),
            new (Core.ElementType.Rock,  Core.ElementType.Lizard),
            new(Core.ElementType.Spock, Core.ElementType.Rock),
            new (Core.ElementType.Spock, Core.ElementType.Scissors),
            new (Core.ElementType.Lizard,  Core.ElementType.Paper),
            new (Core.ElementType.Lizard, Core.ElementType.Spock),
            new (Core.ElementType.Paper, Core.ElementType.Spock),
            new (Core.ElementType.Paper,  Core.ElementType.Rock),
            new (Core.ElementType.Scissors, Core.ElementType.Paper),
            new (Core.ElementType.Scissors,  Core.ElementType.Lizard)
         };

        public WinState CurrentState { get => currentState; set => currentState = value; }

        public ElementType GetRandomElement()
        {
            float randomNumber = UnityEngine.Random.Range(1, 6);
            return (ElementType)randomNumber;
        }

        public string GetMatchResult(ElementType player, ElementType opponent)
        {
            int result = Compare(player, opponent);

            switch (result)
            {
                case 0:
                    CurrentState = WinState.PlayerWon;
                    ScoreController.IncreaseScore();
                    return Constants.PLAYER_WON;
                case 1:
                    CurrentState = WinState.ComputerWon;
                    Events.EndGame?.Invoke();
                    return Constants.COMPUTER_WON;
                default:
                    CurrentState = WinState.Draw;
                    return Constants.MATCH_DRAW;
            }
        }

        public string OnTimeOver()
        {
            Events.EndGame?.Invoke();
            return Constants.TIME_OVER;
        }


        public int Compare(Core.ElementType x, Core.ElementType y)
        {
            Rule value = RuleSet.FirstOrDefault(i => i.Winner == x && i.Loser == y);
            if (value == null)
            {
                value = RuleSet.FirstOrDefault(i => i.Winner == y && i.Loser == x);
            }
            if (value == null)
            {
                return 2;
            }
            if (x == value.Winner)
            {
                return 0;
            }
            return 1;
        }
    }
}

