using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private Button _sellButton;
    [SerializeField] private Tower _towerDefense;
    [SerializeField] private Bilder _bilder;

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
        _bilder.SelectBulding(_towerDefense);
    }
}
