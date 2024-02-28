using RPSLS.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPSLS.Core
{
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

