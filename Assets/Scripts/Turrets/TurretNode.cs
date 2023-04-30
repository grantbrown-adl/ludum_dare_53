using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretNode : MonoBehaviour
{
    [SerializeField] private TurretScript _turretScript;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private GameObject _upgradePanel;

    public TurretScript Turret { get => _turretScript; set => _turretScript = value; }

    public void SetTurret(TurretScript turret) => Turret = turret;
    public bool IsEmpty() => Turret == null;

    public void SelectNode()
    {
        if(IsEmpty()) _buyPanel.SetActive(true);
        else ShowTurretInfo();
    }

    private void ShowTurretInfo()
    {
        _upgradePanel.SetActive(true);
    }

    public void PlaceTurret(TurretScript turretScript, GameObject turret)
    {
        Turret = turretScript;
        GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        Instantiate(turret, transform.position, Quaternion.identity);
    }
}
