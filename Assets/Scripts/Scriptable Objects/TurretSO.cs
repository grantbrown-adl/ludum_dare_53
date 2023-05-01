using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Turret")]
public class TurretSO : ScriptableObject
{
    [SerializeField] private string _turretName;
    [SerializeField] private GameObject _turret;
    [SerializeField] private float _turretCost;
    [SerializeField] private Sprite _turretSprite;

    public GameObject Turret { get => _turret; set => _turret = value; }
    public float TurretCost { get => _turretCost; set => _turretCost = value; }
    public Sprite TurretSprite { get => _turretSprite; set => _turretSprite = value; }
    public string TurretName { get => _turretName; set => _turretName = value; }
}
