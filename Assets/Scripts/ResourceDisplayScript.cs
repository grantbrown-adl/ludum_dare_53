using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceDisplayScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentGold;
    [SerializeField] private TextMeshProUGUI _currentHealth;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _currentWave;
    [SerializeField] private TextMeshProUGUI _currentEnemyUpgradeCost;
    [SerializeField] private TextMeshProUGUI _currentWaveUpgradeCost;

    private void Update()
    {
        _currentGold.text = ResourceManager.Instance.CurrentGold.ToString("F2");
        _currentHealth.text = ResourceManager.Instance.CurrentHealth.ToString();
        _currentWave.text = $"Wave: {ScoreManager.Instance.CurrentWave}";
        _currentScore.text = $"Score: {ScoreManager.Instance.CurrentScore}";
        _currentEnemyUpgradeCost.text = $"Cost: {ScoreManager.Instance.CurrentEnemyUpgradeCost.ToString("n2")}";
        _currentWaveUpgradeCost.text = $"Cost: {ScoreManager.Instance.CurrentWaveUpgradeCost.ToString("n2")}";
    }
}
