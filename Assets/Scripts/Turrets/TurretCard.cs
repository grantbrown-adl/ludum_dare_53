using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretCard : MonoBehaviour
{
    [SerializeField] private Image _turretImage;
    [SerializeField] private TextMeshProUGUI _turretCost;
    [SerializeField] private TurretSO _turretLoaded;
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private TurretNode _turretNode;
    [SerializeField] private TurretScript _turret;
    [SerializeField] private Vector2 _spawnOffset;

    public TurretSO TurretLoaded { get => _turretLoaded; set => _turretLoaded = value; }

    private void Awake()
    {
        SetupTurretButton();
    }

    public void SetupTurretButton()
    {
        _turretImage.sprite = TurretLoaded.TurretSprite;
        _turretCost.text = TurretLoaded.TurretCost.ToString();
    }

    public void PlaceTurret()
    {
        if(ResourceManager.Instance.CurrentGold >= TurretLoaded.TurretCost)
        {
            ResourceManager.Instance.UpdateGold(value: -TurretLoaded.TurretCost);
            _turretNode.SetTurret(_turret);
            Instantiate(_spawnObject, (Vector2)_turretNode.transform.position + _spawnOffset, Quaternion.identity);
        }
    }
}
