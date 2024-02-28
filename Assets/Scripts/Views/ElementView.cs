using RPSLS.Core;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ElementView : MonoBehaviour
{
    [SerializeField] private Button elementButton;
    public RPSLS.Core.ElementType elementType;
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
        Events.SelectedElement?.Invoke(elementType);
    }
}
