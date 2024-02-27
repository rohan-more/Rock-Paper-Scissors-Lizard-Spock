using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPSLS.Views
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text computerChoiceText;
        [SerializeField] private TMP_Text resultText;

        public void UpdateComputerChoiceText(string value)
        {
            if (computerChoiceText != null)
            {
                computerChoiceText.text = value;
            }
        }

        public void UpdateResultsText(string value)
        {
            if (resultText != null)
            {
                resultText.text = value;
            }
        }

        public void ToggleResultsTextVisibility(bool value)
        {
            if (resultText != null)
            {
                resultText.gameObject.SetActive(value);
            }
        }
    }
}

