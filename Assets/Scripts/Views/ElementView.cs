using RPSLS.Core;
using RPSLS.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ElementView : MonoBehaviour
{
    [SerializeField] private Button elementButton;
    public RPSLS.Core.ElementType elementType;
    RulesManager rulesManager = new RulesManager();

    private void OnEnable()
    {
        elementButton.onClick.AddListener(HandleBtnClick);
    }

    private void OnDisable()
    {
        elementButton.onClick.RemoveListener(HandleBtnClick);
    }

    private void HandleBtnClick()
    {
        if(elementType == ElementType.Random)
        {
            Events.SelectedElement?.Invoke(rulesManager.GetRandomElement());
        }
        else
        {
            Events.SelectedElement?.Invoke(elementType);
        }
    }
}
