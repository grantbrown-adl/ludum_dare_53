using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    [SerializeField] private readonly int _initialScore = 0;
    [SerializeField] private int _currentScore;
    [SerializeField] private readonly int _initialWave = 0;
    [SerializeField] private int _currentWave;

    [SerializeField] private float _initialHealthMod;
    [SerializeField] private float _currentHealthMod;
    [SerializeField] private float _currentHealthModRatio;

    [SerializeField] private float _initialGoldMod;
    [SerializeField] private float _currentGoldMod;
    [SerializeField] private float _currentGoldModRatio;

    [SerializeField] private int _initialSpawnMod;
    [SerializeField] private int _currentSpawnMod;
    [SerializeField] private int _currentSpawnModRatio;

    [SerializeField] private float _initialWaveUpgradeCost;
    [SerializeField] private float _currentWaveUpgradeCost;
    [SerializeField] private float _initialWaveUpgradeCostRatio;
    [SerializeField] private float _currentWaveUpgradeCostRatio;

    [SerializeField] private float _initialEnemyUpgradeCost;
    [SerializeField] private float _currentEnemyUpgradeCost;
    [SerializeField] private float _initialEnemyUpgradeCostRatio;
    [SerializeField] private float _currentEnemyUpgradeCostRatio;
    public static ScoreManager Instance { get => _instance; private set => _instance = value; }
    public int CurrentScore { get => _currentScore; set => _currentScore = value; }
    public int CurrentWave { get => _currentWave; set => _currentWave = value; }
    public int CurrentSpawnMod { get => _currentSpawnMod; set => _currentSpawnMod = value; }
    public float CurrentGoldMod { get => _currentGoldMod; set => _currentGoldMod = value; }
    public float CurrentHealthMod { get => _currentHealthMod; set => _currentHealthMod = value; }
    public float CurrentEnemyUpgradeCost { get => _currentEnemyUpgradeCost; set => _currentEnemyUpgradeCost = value; }
    public float CurrentWaveUpgradeCost { get => _currentWaveUpgradeCost; set => _currentWaveUpgradeCost = value; }
    public float CurrentHealthModRatio { get => _currentHealthModRatio; set => _currentHealthModRatio = value; }
    public float CurrentGoldModRatio { get => _currentGoldModRatio; set => _currentGoldModRatio = value; }
    public int CurrentSpawnModRatio { get => _currentSpawnModRatio; set => _currentSpawnModRatio = value; }
    public float CurrentEnemyUpgradeCostRatio { get => _currentEnemyUpgradeCostRatio; set => _currentEnemyUpgradeCostRatio = value; }
    public float CurrentWaveUpgradeCostRatio { get => _currentWaveUpgradeCostRatio; set => _currentWaveUpgradeCostRatio = value; }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        _currentScore = _initialScore;
        _currentWave = _initialWave;
        _currentHealthMod = _initialHealthMod;
        _currentGoldMod = _initialGoldMod;
        _currentSpawnMod = _initialSpawnMod;
        _currentWaveUpgradeCost = _initialWaveUpgradeCost;
        _currentWaveUpgradeCostRatio = _initialWaveUpgradeCostRatio;
        _currentEnemyUpgradeCost = _initialEnemyUpgradeCost;
        _currentEnemyUpgradeCostRatio = _initialEnemyUpgradeCostRatio;
    }

    public void IncrementScore(int value) => _currentScore += value;
    public void IncrementWave() => _currentWave++;

}
