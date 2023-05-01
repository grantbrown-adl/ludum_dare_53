using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private EnemyScript _target;
    [SerializeField] private PoolAfterTime _poolAfter;
    private readonly float _epsilon = 0.05f;
    [SerializeField] private bool _isHoming;
    [SerializeField] private Vector2 _direction;

    public float Damage { get => _damage; set => _damage = value; }
    public EnemyScript Target { get => _target; set => _target = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }
    public bool IsHoming { get => _isHoming; set => _isHoming = value; }

    private void Update()
    {
        if(_target != null || _direction != null) UpdateProjectile();
    }

    private void UpdateProjectile()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if(_target != null)
        {
            if(!_target.gameObject.activeInHierarchy)
            {
                ObjectPoolScript.ReturnInstance(gameObject);
                //transform.position = _moveSpeed * Time.deltaTime * _direction;
            }
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);

            float remainingDistance = (_target.transform.position - transform.position).magnitude;
            if(remainingDistance <= _epsilon)
            {
                if(_target.gameObject.activeInHierarchy)
                {
                    _target.Health.ReceiveDamage(_damage);
                    ObjectPoolScript.ReturnInstance(gameObject);
                }
            }
            _direction = _target.transform.position;
        }

        if(!_isHoming)
        {
            //transform.position = Vector2.MoveTowards(transform.position, _direction * 3, _moveSpeed * Time.deltaTime);
            transform.position += (Vector3)_direction * (Time.deltaTime * _moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isHoming) return;
        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        if(enemy)
        {
            enemy.Health.ReceiveDamage(_damage);
        }
            
    }

    private void Rotate()
    {
        if(!_isHoming) return;
        Vector2 enemyPosition = _target.transform.position - transform.position;
        float angleBetween = Vector2.SignedAngle(transform.right, enemyPosition);
        transform.Rotate(0.0f, 0.0f, angleBetween);
    }
}
