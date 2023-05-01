using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretNode : MonoBehaviour
{
    [SerializeField] private TurretScript _turretScript;
    [SerializeField] private TurretUpgradeScript _turretUpgradeScript;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private string _turretName;
    [SerializeField] private GameObject _turretInstance;

    public TurretScript Turret { get => _turretScript; set => _turretScript = value; }
    public TurretUpgradeScript TurretUpgrade { get => _turretUpgradeScript; set => _turretUpgradeScript = value; }
    public string TurretName { get => _turretName; set => _turretName = value; }
    public GameObject TurretInstance { get => _turretInstance; set => _turretInstance = value; }

    public void SetTurret(TurretScript turret) => Turret = turret;
    public void SetUpgrade(TurretUpgradeScript turretUpgrade) => TurretUpgrade = turretUpgrade;
    public void SetTurretInstance(GameObject turretInstance) => _turretInstance = turretInstance;
    public bool IsEmpty() => Turret == null;

    public void SelectNode()
    {
        if(IsEmpty()) _buyPanel.SetActive(true);
        else ShowTurretInfo();
    }

    public void ClearNode()
    {
        Turret = null;
        TurretUpgrade = null;
        Destroy(_turretInstance);
    }

    private void ShowTurretInfo()
    {
        _upgradePanel.SetActive(true);
    }

    public void UpgradeTurret()
    {
        TurretInstance.GetComponent<TurretUpgradeScript>().UpdgradeTurret();
    }
    public void UpgradeTurretDamage()
    {
        TurretInstance.GetComponent<TurretUpgradeScript>().UpgradeDamage();
    }
    public void UpgradeTurretSpeed()
    {
        TurretInstance.GetComponent<TurretUpgradeScript>().UpgradeDelay();
    }

    public void UpgradeTurretMove()
    {
        TurretInstance.GetComponent<TurretUpgradeScript>().UpgradeProjectileSpeed();
    }
}
