using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManualSpawner : MonoBehaviour
{
    private static ManualSpawner _instance;
    [SerializeField] private int _incrementMax;
    [SerializeField] private float _decrementDelay;
    [SerializeField] private float _decrementTimer;
    [SerializeField] private Button _waveButton;
    [SerializeField] private Button _enemyUpgradeButton;
    [SerializeField] private Button _waveUpgradeButton;
    [SerializeField] private float _timer;

    public static ManualSpawner Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        _timer = 15;
    }

    private void Update()
    {
        if(EnemySpawnerScript.Instance.MaxSpawnedObjects == EnemySpawnerScript.Instance.SpawnedObjectCount)
        {
            _timer += Time.deltaTime;
            if(_timer < 15) _waveButton.interactable = false;
            else
            {
                _timer = 15;
                _waveButton.interactable = true;
            }
        }
        else _waveButton.interactable = false;

        if(ResourceManager.Instance.CurrentGold >= ScoreManager.Instance.CurrentWaveUpgradeCost) _waveUpgradeButton.interactable = true;
        else _waveUpgradeButton.interactable = false;

        if(ResourceManager.Instance.CurrentGold >= ScoreManager.Instance.CurrentEnemyUpgradeCost) _enemyUpgradeButton.interactable = true;
        else _enemyUpgradeButton.interactable = false;
    }

    public void UpgradeWave()
    {
        if(ResourceManager.Instance.CurrentGold >= ScoreManager.Instance.CurrentWaveUpgradeCost)
        {
            ResourceManager.Instance.UpdateGold(-ScoreManager.Instance.CurrentWaveUpgradeCost);
            //EnemySpawnerScript.Instance.MaxSpawnedObjects += ScoreManager.Instance.CurrentSpawnMod;
            EnemySpawnerScript.Instance.UpdateMaxSpawns(ScoreManager.Instance.CurrentSpawnMod);
            SoundManager.Instance.PlayClip(SoundManager.Instance.Money);
            UpgradeWaveCost();
        }
    }

    public void UpgradeEnemy()
    {
        if(ResourceManager.Instance.CurrentGold >= ScoreManager.Instance.CurrentEnemyUpgradeCost)
        {
            ResourceManager.Instance.UpdateGold(-ScoreManager.Instance.CurrentEnemyUpgradeCost);
            ScoreManager.Instance.CurrentGoldMod *= ScoreManager.Instance.CurrentGoldModRatio;
            ScoreManager.Instance.CurrentHealthMod *= ScoreManager.Instance.CurrentHealthModRatio;
            SoundManager.Instance.PlayClip(SoundManager.Instance.Money);
            UpgradeEnemyCost();
        }
    }

    private void UpgradeEnemyCost()
    {
        ScoreManager.Instance.CurrentEnemyUpgradeCost *= ScoreManager.Instance.CurrentEnemyUpgradeCostRatio;
    }

    private void UpgradeWaveCost()
    {
        ScoreManager.Instance.CurrentWaveUpgradeCost *= ScoreManager.Instance.CurrentWaveUpgradeCostRatio;
    }

    public void LaunchWave()
    {
        int random = Random.Range(0, 100);
        if(random > 80) SoundManager.Instance.PlayClip(SoundManager.Instance.WaveStart);
        ScoreManager.Instance.IncrementWave();
        EnemySpawnerScript.Instance.StartWave();
        _timer = 0;
    }
}
