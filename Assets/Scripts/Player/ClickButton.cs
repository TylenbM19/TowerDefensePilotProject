using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private Button _sellButton;
    [SerializeField] private string _nameTowerDefense;

    public static event UnityAction <string> ButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClick?.Invoke(_nameTowerDefense);
    }
}
