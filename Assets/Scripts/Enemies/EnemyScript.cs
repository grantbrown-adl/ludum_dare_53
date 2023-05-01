using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private HealthBar _health;
    [SerializeField] float _goldValue;
    [SerializeField] float _initialGoldValue;
    [SerializeField] private float _initialMoveSpeed;
    [SerializeField] private WaypointScript _waypoint;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Vector2 _previousWaypointLocation;
    [SerializeField] private Vector2 _targetWaypoint;
    [SerializeField] private int _currentWaypointIndex;
    [SerializeField] private GameObject _spawnLocation;
    [SerializeField] private float _duration;
    [SerializeField] PoolAfterTime _poolAfterTime;
    [SerializeField] private bool _hasDecrementedLife;
    [SerializeField] private bool _rightFacing;

    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
    public HealthBar Health { get => _health; set => _health = value; }
    public float Duration { get => _duration; set => _duration = value; }
    public float GoldValue { get => _goldValue; set => _goldValue = value; }

    private void Awake()
    {
        _hasDecrementedLife = false;
        _poolAfterTime = GetComponent<PoolAfterTime>();        
        _currentWaypointIndex = 0;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _targetWaypoint = _waypoint.Waypoints[_currentWaypointIndex];
        _health = GetComponent<HealthBar>();
    }
    

    private void OnEnable()
    {
        //_poolAfterTime.enabled = false;
    }

    public void CleanupObject()
    {
        _hasDecrementedLife = false;
        ClearWaypoints();
    }

    private void Start()
    {
        _previousWaypointLocation = transform.position;
        _goldValue = _initialGoldValue * ScoreManager.Instance.CurrentGoldMod;
        _moveSpeed = _initialMoveSpeed;
    }

    private void Update()
    {
        Move();
        AdjustFacing();
        if(ReachedTargetWaypoint()) UpdateTargetWaypoint();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetWaypoint, _moveSpeed * Time.deltaTime);
    }

    private void AdjustFacing()
    {
        if(_rightFacing)
        {
            if(_targetWaypoint.x > _previousWaypointLocation.x) _spriteRenderer.flipX = false;
            else _spriteRenderer.flipX = true;
        } else
        {
            if(_targetWaypoint.x < _previousWaypointLocation.x) _spriteRenderer.flipX = false;
            else _spriteRenderer.flipX = true;
        }

    }

    private bool ReachedTargetWaypoint()
    {
        float distanceToWaypoint = (transform.position - (Vector3)_targetWaypoint).magnitude;
        if(distanceToWaypoint < 0.1f)
        {
            _previousWaypointLocation = transform.position;
            return true;
        }
        return false;
    }

    private void UpdateTargetWaypoint()
    {
        int finalWaypointIndex = _waypoint.Waypoints.Length - 1;
        if(_currentWaypointIndex < finalWaypointIndex) _currentWaypointIndex++;
        else LastWaypointReached();
        _targetWaypoint = _waypoint.Waypoints[_currentWaypointIndex];
    }

    private void LastWaypointReached()
    {
        //_poolAfterTime.enabled = true;
        if(!_hasDecrementedLife)
        {
            ResourceManager.Instance.UpdateHealth(-1);
            int random = Random.Range(0, 100);
            if(random > 80) SoundManager.Instance.PlayClip(SoundManager.Instance.PlayerDamage);
            _hasDecrementedLife = true;
            ObjectPoolScript.ReturnInstance(gameObject);
            CleanupObject();
        }
            
    }

    private void ClearWaypoints()
    {
        _currentWaypointIndex = 0;
        _previousWaypointLocation = _spawnLocation.transform.position;
        _targetWaypoint = _waypoint.Waypoints[_currentWaypointIndex];
    }

    public IEnumerator StopMovement(float duration)
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position, _moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(duration);
        transform.position = Vector2.MoveTowards(transform.position, _targetWaypoint, _moveSpeed * Time.deltaTime);
    }
}
