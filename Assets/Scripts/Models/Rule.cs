using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPSLS.Core;

namespace RPSLS.Core
{
    public class Rule
    {
        public ElementType Winner;
        public ElementType Loser;

        public Rule(ElementType winner, ElementType loser)
        {
            Winner = winner;
            Loser = loser;
        }

    }
}

