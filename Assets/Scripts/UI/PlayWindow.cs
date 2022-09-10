using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _waveCount;
    [SerializeField] private TMP_Text _countGold;

    private const string _wave = "Wave";
    private const string _gold = "Gold";

    private int _countWave = 0;
    private int _goldtWave = 0;

    private void Awake()
    {
        _waveCount.text = (_wave);
        //_countGold.text = (_gold);
    }

    private void OnEnable()
    {
        WaveSpawner.OnWave += ChangeWave;
        Enemy.OnDie += ChangeGold;        
    }

    private void OnDisable()
    {
        WaveSpawner.OnWave -= ChangeWave;
        Enemy.OnDie -= ChangeGold;
    }


    private void ChangeWave(int _countWave)
    {
        _waveCount.text = ("Wave" + " " + _countWave.ToString());
    }

    private void ChangeGold(int _goldtWave)
    {
        _countGold.text = (_goldtWave.ToString());
    }
}
