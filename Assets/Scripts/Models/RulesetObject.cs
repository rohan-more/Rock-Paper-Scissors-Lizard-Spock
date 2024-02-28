using RPSLS.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPSLS.Core
{
    /// <summary>
    /// Holds the rulesets created in Unity Editor - add the winner element and loser element at the same index in both lists
    /// </summary>
    [CreateAssetMenu(fileName = "RuleSet", menuName = "ScriptableObjects/RuleSet", order = 1)]
    public class RulesetObject : ScriptableObject
    {
        [Header("Timer")]
        public float Timer = 2f;
        [Header("Winners")]
        public List<ElementType> Winners;
        [Header("Losers")]
        public List<ElementType> Losers;

    }
}

