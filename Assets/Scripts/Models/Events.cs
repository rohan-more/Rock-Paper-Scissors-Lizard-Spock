using System;
using System.Collections;
using System.Collections.Generic;

namespace RPSLS.Core
{
    public static class Events
    {
        public static Action<ElementType> SelectedElement;
        public static Action StartGame;
        public static Action EndGame;
        public static Action OnBack;
    }
}

