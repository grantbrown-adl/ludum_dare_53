using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeScript : MonoBehaviour
{
    [SerializeField] private float _initialCost;
    [SerializeField] private float _initialCostRatio;
    [SerializeField] private float _upgradeCost;
    [SerializeField] private float _upgradeCostSpeed;
    [SerializeField] private float _upgradeCostDamage;
    [SerializeField] private float _upgradeCostProjectileSpeed;
    [SerializeField] private float _damageIncrease;
    [SerializeField] private float _delayReduction;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private TurretProjectilesScript _turretProjectiles;
    [SerializeField] private int _turretLevel;

    public float UpgradeCost { get => _upgradeCost; set => _upgradeCost = value; }
    public int TurretLevel { get => _turretLevel; set => _turretLevel = value; }
    public TurretProjectilesScript TurretProjectiles { get => _turretProjectiles; set => _turretProjectiles = value; }
    public float UpgradeCostSpeed { get => _upgradeCostSpeed; set => _upgradeCostSpeed = value; }
    public float UpgradeCostDamage { get => _upgradeCostDamage; set => _upgradeCostDamage = value; }
    public float ProjectileSpeed { get => _projectileSpeed; set => _projectileSpeed = value; }
    public float UpgradeCostProjectileSpeed { get => _upgradeCostProjectileSpeed; set => _upgradeCostProjectileSpeed = value; }

    private void Awake()
    {
        _turretProjectiles = GetComponent<TurretProjectilesScript>();
        _upgradeCostSpeed = _upgradeCostDamage = _upgradeCost = _upgradeCostProjectileSpeed = _initialCost;
        _turretLevel = 1;
    }

    public void UpdgradeTurret()
    {
        if(ResourceManager.Instance.CurrentGold < _upgradeCost) return;
        UpgradeDamage();
        UpgradeDelay();
        UpdateUpgrades();
    }

    public void UpgradeDamage()
    {
        if(ResourceManager.Instance.CurrentGold < _upgradeCostDamage) return;
        ResourceManager.Instance.UpdateGold(-_upgradeCostDamage);
        SoundManager.Instance.PlayClip(SoundManager.Instance.Money);
        _turretProjectiles.Damage += _damageIncrease;
        _upgradeCostDamage *= _initialCostRatio;
        _turretLevel++;
    }

    public void UpgradeDelay()
    {
        if(_turretProjectiles.FireDelay <= 0.01f)
        {
            _upgradeCostSpeed = int.MaxValue;
            return;
        }
        if(ResourceManager.Instance.CurrentGold < _upgradeCostSpeed) return;
        ResourceManager.Instance.UpdateGold(-_upgradeCostSpeed);
        SoundManager.Instance.PlayClip(SoundManager.Instance.Money);
        _turretProjectiles.FireDelay -= _delayReduction;
        _upgradeCostSpeed *= _initialCostRatio;
        _turretLevel++;
    }

    public void UpgradeProjectileSpeed()
    {
        if(ResourceManager.Instance.CurrentGold < _upgradeCostProjectileSpeed) return;
        ResourceManager.Instance.UpdateGold(-_upgradeCostProjectileSpeed);
        SoundManager.Instance.PlayClip(SoundManager.Instance.Money);
        _turretProjectiles.MoveSpeed += _projectileSpeed;
        _upgradeCostProjectileSpeed *= _initialCostRatio;
        _turretLevel++;
    }

    private void UpdateUpgrades()
    {
        ResourceManager.Instance.UpdateGold(-_upgradeCost);
        _upgradeCost *= _initialCostRatio;
        _turretLevel++;
    }
}
