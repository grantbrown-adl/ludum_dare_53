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
    [SerializeField] private Button _button;
    [SerializeField] private TurretSO _turretLoaded;
    [SerializeField] private TurretUpgradeScript _turretUpgrades;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private TurretNode _turretNode;
    [SerializeField] private TurretScript _turret;
    [SerializeField] private Vector2 _spawnOffset;
    [SerializeField] private GameObject _spawnedTurret;

    public TurretSO TurretLoaded { get => _turretLoaded; set => _turretLoaded = value; }

    private void Awake()
    {
        SetupTurretButton();
    }

    private void Update()
    {
        if(TurretLoaded.TurretCost > ResourceManager.Instance.CurrentGold) _button.interactable = false;
        else _button.interactable = true;
    }

    public void SetupTurretButton()
    {
        _turretImage.sprite = TurretLoaded.TurretSprite;
        _turretCost.text = $"{TurretLoaded.TurretCost}";
    }

    public void PlaceTurret()
    {
        if(ResourceManager.Instance.CurrentGold >= TurretLoaded.TurretCost)
        {
            ResourceManager.Instance.UpdateGold(value: -TurretLoaded.TurretCost);
            SoundManager.Instance.PlayClip(SoundManager.Instance.Build);

            _spawnedTurret = Instantiate(_prefab, (Vector2)_turretNode.transform.position + _spawnOffset, Quaternion.identity, _turretNode.transform);
            _turretNode.SetTurretInstance(_spawnedTurret);
            _turretNode.SetTurret(_turret);
        }
    }
}
