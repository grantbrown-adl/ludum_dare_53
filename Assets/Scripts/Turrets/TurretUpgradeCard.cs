using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretUpgradeCard : MonoBehaviour
{

    [SerializeField] private TurretScript _turretScript;
    [SerializeField] private TurretUpgradeScript _turretUpgradeScript;
    [SerializeField] private TurretNode _turretNode;
    [SerializeField] private TextMeshProUGUI _turretName;
    [SerializeField] private TextMeshProUGUI _turretLevel;
    [SerializeField] private TextMeshProUGUI _turretDamage;
    [SerializeField] private TextMeshProUGUI _turretSpeed;
    [SerializeField] private TextMeshProUGUI _turretSpeedCost;
    [SerializeField] private TextMeshProUGUI _turretDamageCost;
    [SerializeField] private TextMeshProUGUI _turretMoveCost;
    [SerializeField] private TextMeshProUGUI _turretMove;
    public TurretScript Turret { get => _turretScript; set => _turretScript = value; }
    public TurretUpgradeScript TurretUpgrades { get => _turretUpgradeScript; set => _turretUpgradeScript = value; }

    private void Awake()
    {
        SetupTurretUpgrades();
    }

    public void SetupTurretUpgrades()
    {
        Turret = _turretNode.Turret;
        TurretUpgrades = _turretNode.TurretInstance.GetComponent<TurretUpgradeScript>();
        _turretName.text = Turret.TurretName;
        _turretLevel.text = $"{TurretUpgrades.TurretLevel}";
        _turretDamage.text = TurretUpgrades.TurretProjectiles.Damage.ToString("F2");
        _turretSpeed.text = TurretUpgrades.TurretProjectiles.FireDelay.ToString("F2");
        _turretMove.text = TurretUpgrades.TurretProjectiles.MoveSpeed.ToString("F2");
        _turretSpeedCost.text = TurretUpgrades.UpgradeCostSpeed.ToString("F2");
        _turretDamageCost.text = TurretUpgrades.UpgradeCostDamage.ToString("F2");
        _turretMoveCost.text = TurretUpgrades.UpgradeCostProjectileSpeed.ToString("F2");
    }
}
