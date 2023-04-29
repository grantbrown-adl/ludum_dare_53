using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPoolScript))]
public class EnemySpawnerScript : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private int _maxSpawnedObjects;
    [SerializeField] private float _spawnDelay;

    [Header("Visualised Internals")]
    [SerializeField] private float _spawnTimer;
    [SerializeField] private int _spawnedObjectCount;
    private ObjectPoolScript _objectPool;

    private void Awake()
    {
        _objectPool = GetComponent<ObjectPoolScript>();
    }

    private void Update()
    {
        SpawnTimer();
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

    private void SpawnObject()
    {
        _spawnedObjectCount++;
        GameObject instance = _objectPool.GetInstance();
        instance.SetActive(true);
    }
}
