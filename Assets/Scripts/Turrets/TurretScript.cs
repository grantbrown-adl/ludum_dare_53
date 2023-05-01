using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private string _turretName;
    [SerializeField] private List<EnemyScript> _enemies;
    [SerializeField] private EnemyScript _target;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _rotate;
    [SerializeField] private bool _faceTarget;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _attackRange;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private bool _rightFacing;

    public EnemyScript Target { get => _target; set => _target = value; }
    public string TurretName { get => _turretName; set => _turretName = value; }

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<CircleCollider2D>();
        _collider.radius = _attackRange;
    }

    private void Update()
    {
        TargetEnemy();
        if(_rotate) RotateTowardTarget();
        if(_faceTarget && _target != null) AdjustFacing();
    }

    private void TargetEnemy()
    {
        if(_enemies.Count <= 0)
        {
            _target = null;
            return;
        }
        _target = _enemies[0];
    }

    private void AdjustFacing()
    {
        if(_rightFacing)
        {
            if(_target.transform.position.x > transform.position.x) _spriteRenderer.flipX = false;
            else _spriteRenderer.flipX = true;
        } else
        {
            if(_target.transform.position.x < transform.position.x) _spriteRenderer.flipX = false;
            else _spriteRenderer.flipX = true;
        }


    }

    private void RotateTowardTarget()
    {
        if(_target == null) return;
        Vector2 targetPosition = _target.transform.position - transform.position;
        float angleBetween = Vector2.SignedAngle(transform.up, targetPosition);
        transform.Rotate(0.0f, 0.0f, angleBetween);

        //transform.Rotate(0.0f, 0.0f, Mathf.Lerp(transform.rotation.z, angleBetween, Time.deltaTime * rotationSpeed));
        //if(_rotate)
        //{

        //    Vector2 targetPosition = _target.transform.position - transform.position;
        //    float angleBetween = Vector2.SignedAngle(transform.up, targetPosition);
        //    transform.Rotate(0.0f, 0.0f, angleBetween);

        //    //Vector2 targetPosition = _target.transform.position - transform.position;
        //    //Vector3 currentAngle = transform.eulerAngles;
        //    //float angleBetween = Vector2.SignedAngle(transform.up, targetPosition);
        //    //transform.Rotate(0.0f, 0.0f, Mathf.Lerp(transform.rotation.z, angleBetween, Time.deltaTime * rotationSpeed));
        //    //transform.rotation = Quaternion.Lerp(Quaternion.AngleAxis(angleBetween, Vector3.forward), transform.rotation, Time.deltaTime * _rotationSpeed);

        //    //currentAngle = new Vector3(currentAngle.x, currentAngle.y, Mathf.LerpAngle(currentAngle.z, angleBetween, Time.deltaTime));
        //    //transform.eulerAngles = currentAngle;
        //    // float angle = Mathf.Atan2(_target.transform.position.y - transform.position.y, _target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        //    //Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //    //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        //}
        //else
        //{
        //    Vector2 targetPosition = _target.transform.position - transform.position;
        //    float angleBetween = Vector2.SignedAngle(transform.up, targetPosition);
        //    transform.Rotate(0.0f, 0.0f, angleBetween);

        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        if(enemy) _enemies.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        if(enemy) _enemies.Remove(enemy);
    }
}
