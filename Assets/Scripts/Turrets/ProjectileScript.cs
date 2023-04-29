using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private EnemyScript _target;
    private readonly float _epsilon = 0.01f;

    public float Damage { get => _damage; set => _damage = value; }
    public EnemyScript Target { get => _target; set => _target = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private void Update()
    {
        if(_target != null) UpdateProjectile();
    }

    private void UpdateProjectile()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);

        float remainingDistance = (_target.transform.position - transform.position).magnitude;
        if(remainingDistance <= _epsilon)
        {
            _target.Health.ReceiveDamage(_damage);
            ObjectPoolScript.ReturnInstance(gameObject);
        }
    }

    private void Rotate()
    {
        Vector2 enemyPosition = _target.transform.position - transform.position;
        float angleBetween = Vector2.SignedAngle(transform.right, enemyPosition);
        transform.Rotate(0.0f, 0.0f, angleBetween);
    }
}
