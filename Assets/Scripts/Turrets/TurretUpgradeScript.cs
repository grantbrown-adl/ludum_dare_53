using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeScript : MonoBehaviour
{
    [SerializeField] private float _initialCost;
    [SerializeField] private float _initialCostRatio;
    [SerializeField] private float _upgradeCost;
    [SerializeField] private float _damageIncrease;
    [SerializeField] private float _delayReduction;
    [SerializeField] private TurretProjectilesScript _turretProjectiles;
    [SerializeField] private int _turretLevel;

    public float UpgradeCost { get => _upgradeCost; set => _upgradeCost = value; }
    public int TurretLevel { get => _turretLevel; set => _turretLevel = value; }

    private void Awake()
    {
        _turretProjectiles = GetComponent<TurretProjectilesScript>();
        _upgradeCost = _initialCost;
        _turretLevel = 1;
    }

    public void UpdgradeTurret()
    {
        if(ResourceManager.Instance.CurrentGold < _upgradeCost) return;
        UpgradeDamage();
        UpgradeDelay();
        UpdateUpgrades();
    }

    private void UpgradeDamage()
    {
        _turretProjectiles.Damage += _damageIncrease;
    }

    private void UpgradeDelay()
    {
        _turretProjectiles.FireDelay -= _delayReduction;
    }

    private void UpdateUpgrades()
    {
        ResourceManager.Instance.UpdateGold(-_upgradeCost);
        _upgradeCost *= _initialCostRatio;
        _turretLevel++;
    }
}
