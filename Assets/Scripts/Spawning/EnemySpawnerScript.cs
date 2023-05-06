using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPoolScript))]
public class EnemySpawnerScript : MonoBehaviour
{
    private static EnemySpawnerScript _instance;
    public static EnemySpawnerScript Instance { get => _instance; private set => _instance = value; }
    public int MaxSpawnedObjects { get => _maxSpawnedObjects; set => _maxSpawnedObjects = value; }
    public float SpawnDelay { get => _spawnDelay; set => _spawnDelay = value; }
    public float SpawnTime { get => _spawnTimer; set => _spawnTimer = value; }
    public int SpawnedObjectCount { get => _spawnedObjectCount; set => _spawnedObjectCount = value; }
    public ObjectPoolScript ObjectPool { get => _objectPool; set => _objectPool = value; }

    [Header("Spawn Settings")]
    [SerializeField] private int _maxSpawnedObjects;
    [SerializeField] private int _initialMaxSpawnedObjects;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _initialSpawnDelay;

    [Header("Visualised Internals")]
    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _initialSpawnTimer;
    [SerializeField] private int _spawnedObjectCount;
    [SerializeField] private ObjectPoolScript _objectPool;

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        _objectPool = GetComponent<ObjectPoolScript>();
        _spawnedObjectCount = _maxSpawnedObjects = _initialMaxSpawnedObjects;
        _spawnDelay = _initialSpawnDelay;
        _spawnTimer = _initialSpawnDelay;
    }

    private void Update()
    {
        SpawnTimer();
    }

    public void StartWave()
    {
        _spawnedObjectCount = 0;
    }

    void SpawnTimer()
    {
        _spawnTimer += Time.deltaTime;

        if(_spawnTimer >= _spawnDelay)
        {
            if(_spawnedObjectCount >= _maxSpawnedObjects) _spawnTimer = _maxSpawnedObjects;
            else _spawnTimer = 0;
            if(_spawnedObjectCount < _maxSpawnedObjects) SpawnObject();
        }            
    }

    public bool CanSpawnAgain()
    {
        foreach(GameObject item in _objectPool.Pool)
        {
            if(item.activeInHierarchy) return false;
        }
        return true;
    }

    private void SpawnObject()
    {
        _spawnedObjectCount++;
        GameObject instance = _objectPool.GetInstance();
        instance.GetComponent<EnemyScript>();
        instance.SetActive(true);
    }

    public void UpdateMaxSpawns(int value)
    {
        _spawnedObjectCount += value;
        _maxSpawnedObjects += value;
    }
}
