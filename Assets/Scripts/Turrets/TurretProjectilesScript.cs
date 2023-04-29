using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectilesScript : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _fireDelay;
    [SerializeField] private float _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackTimer;
    [SerializeField] private TurretScript _turret;
    [SerializeField] ObjectPoolScript _objectPool;
    [SerializeField] private ProjectileScript _projectile;
    [SerializeField] private bool _turretNotLoaded;

    public float Damage { get => _damage; set => _damage = value; }
    public float FireDelay { get => _fireDelay; set => _fireDelay = value; }

    private void Awake()
    {
        _objectPool = GetComponent<ObjectPoolScript>();
        _turret = GetComponent<TurretScript>();
        LoadProjectile();
    }

    private void Update()
    {       
        if(_turretNotLoaded) LoadProjectile();
        if(_attackTimer >= _fireDelay)
        {
            if(_turret.Target != null && _turret.Target.Health.CurrentHealth > 0.0f)
            {
                FireProjectile();
            }
        }
        _attackTimer += Time.deltaTime;
    }

    private void LoadProjectile()
    {
        //GameObject instance = _objectPool.GetInstance();
        //instance.transform.localPosition = _spawnPosition.position;
        //instance.transform.SetParent(_spawnPosition);
    }

    private void FireProjectile()
    {
        GameObject instance = _objectPool.GetInstance();
        instance.transform.position = _spawnPosition.position;
        ProjectileScript projectile = instance.GetComponent<ProjectileScript>();
        projectile.Target = _turret.Target;
        projectile.Damage = _damage;
        projectile.MoveSpeed = _moveSpeed;
        instance.SetActive(true);
        _attackTimer = 0;
    }


}
