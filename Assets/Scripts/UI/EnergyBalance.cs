using TMPro;
using UnityEngine;

public class EnergyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _energy;
    private Player _playerWindow;

    private void OnDisable()
    {
        _playerWindow.CurrentBalanse -= ChangeBalance;
    }

    private void Start()
    {
        _playerWindow = Service.Instance.Get<Player>();
        _energy.text = _playerWindow.CurrentEnergy.ToString();
        _playerWindow.CurrentBalanse += ChangeBalance;
    }

    private void ChangeBalance(int reward)
    {
        _energy.text = reward.ToString();
    }
}
