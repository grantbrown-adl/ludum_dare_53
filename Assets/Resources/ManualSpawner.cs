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
    [SerializeField] private bool _automaticWave;
    [SerializeField] private Button _autoButton;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _autoEnabledText;
    [SerializeField] private GameObject _cheatButton;

    public static ManualSpawner Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        _automaticWave = false;
        _timer = 0;
    }

    private void Update()
    {
        _cheatButton.SetActive(TimeManager.Instance.CheatsEnabled);   
        if(_automaticWave)
        {
            if(EnemySpawnerScript.Instance.CanSpawnAgain()) _timerText.text = $"Next Wave: {  _timer.ToString("f0") }";
            else _timerText.text = "In Progress";
            _autoEnabledText.text = "On";
            _autoEnabledText.color = Color.green;
            _waveButton.interactable = false;
        }
        else
        {
            _timerText.text = "";
            _autoEnabledText.text = "Off";
            _autoEnabledText.color = Color.red;
        }

        if(!_automaticWave)
        {
            if(EnemySpawnerScript.Instance.MaxSpawnedObjects == EnemySpawnerScript.Instance.SpawnedObjectCount)
            {
                _timer -= Time.deltaTime;
                if(_timer >= 15) _waveButton.interactable = false;
                else
                {
                    _timer = 0;
                    if(EnemySpawnerScript.Instance.CanSpawnAgain()) _waveButton.interactable = true;
                }
            }
            else _waveButton.interactable = false;
        } else
        {
            if(EnemySpawnerScript.Instance.CanSpawnAgain())
            {
                _timer -= Time.deltaTime;
                if(_timer <= 0) LaunchWave();
            }

        }


        if(ResourceManager.Instance.CurrentGold >= ScoreManager.Instance.CurrentWaveUpgradeCost) _waveUpgradeButton.interactable = true;
        else _waveUpgradeButton.interactable = false;

        if(ResourceManager.Instance.CurrentGold >= ScoreManager.Instance.CurrentEnemyUpgradeCost) _enemyUpgradeButton.interactable = true;
        else _enemyUpgradeButton.interactable = false;
    }

    public void GoldIncrease() => ScoreManager.Instance.CurrentGoldMod += 1;

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

    public void ToggleAutomaticWave() => _automaticWave = !_automaticWave;

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
        if(_automaticWave) EnemySpawnerScript.Instance.ObjectPool.RefreshPool();
        int random = Random.Range(0, 100);
        if(random > 80) SoundManager.Instance.PlayClip(SoundManager.Instance.WaveStart);
        ScoreManager.Instance.IncrementWave();
        EnemySpawnerScript.Instance.StartWave();
        _timer = 15;
        EnemySpawnerScript.Instance.UpdateMaxSpawns(ScoreManager.Instance.CurrentSpawnMod - 1);
    }
}
