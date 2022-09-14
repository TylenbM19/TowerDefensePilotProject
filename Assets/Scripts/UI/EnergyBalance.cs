using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _energy;
    private PlayerWindow _playerWindow;

    private void Start()
    {
        _playerWindow = Service.Instance.Get<PlayerWindow>();
        _energy.text = _playerWindow.CurrentEnergy.ToString();
    }
}
