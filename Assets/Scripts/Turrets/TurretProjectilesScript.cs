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
    [SerializeField] private bool _isHoming;
    [SerializeField] private float _spreadDistance;

    public float Damage { get => _damage; set => _damage = value; }
    public float FireDelay { get => _fireDelay; set => _fireDelay = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

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
                if(_isHoming) FireProjectile();
                else
                {
                    Vector2 direction = _turret.Target.transform.position - transform.position;
                    FireBubble(direction);
                }
            }
        }
        _attackTimer += Time.deltaTime;
        if(_attackTimer > _fireDelay) _attackTimer = _fireDelay;
    }

    private void LoadProjectile()
    {
        //GameObject instance = _objectPool.GetInstance();
        //instance.transform.localPosition = _spawnPosition.position;
        //instance.transform.SetParent(_spawnPosition);
    }

    private void FireBubble(Vector2 direction)
    {
        int random = Random.Range(0, 100);
        if(random > 95 && !SoundManager.Instance.AudioSource.isPlaying) SoundManager.Instance.PlayClip(SoundManager.Instance.Bubble);
        GameObject instance = _objectPool.GetInstance();
        instance.transform.position = _spawnPosition.position;
        ProjectileScript projectile = instance.GetComponent<ProjectileScript>();

        projectile.Direction = direction;
        projectile.Damage = Damage;
        projectile.MoveSpeed = _moveSpeed;

        float randomSpread = Random.Range(-_spreadDistance, _spreadDistance);
        Vector3 spread = new Vector3(0.0f, 0.0f, randomSpread);
        Quaternion spreadValue = Quaternion.Euler(spread);
        Vector2 spreadDirection = spreadValue * direction;
        projectile.Direction = spreadDirection;

        instance.SetActive(true);
        _attackTimer = 0;
    }

    private void FireProjectile()
    {
        GameObject instance = _objectPool.GetInstance();
        int random = Random.Range(0, 100);
        if(!SoundManager.Instance.AudioSource.isPlaying && random > 80)
        {
            if(instance.name == "claws(Clone)") SoundManager.Instance.PlayClip(SoundManager.Instance.Claws);
            else if(random > 95) SoundManager.Instance.PlayClip(SoundManager.Instance.Fire);
        }
        instance.transform.position = _spawnPosition.position;
        ProjectileScript projectile = instance.GetComponent<ProjectileScript>();
        projectile.Target = _turret.Target;
        projectile.Damage = Damage;
        projectile.MoveSpeed = _moveSpeed;
        instance.SetActive(true);
        _attackTimer = 0;
    }


}
