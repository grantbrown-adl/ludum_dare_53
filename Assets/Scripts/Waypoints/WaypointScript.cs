using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    [SerializeField] private Vector2[] _waypoints;
    [SerializeField] private Vector2 _currentPosition;
    [SerializeField] private bool _gameRunning;

    public Vector2[] Waypoints { get => _waypoints; }
    public Vector2 CurrentPosition { get => _currentPosition; }

    private void Awake()
    {
        _gameRunning = true;
        _currentPosition = transform.position;
    }

    public Vector2 GetWaypointPosition(int waypointIndex)
    {
        return _currentPosition + _waypoints[waypointIndex];
    }

    private void OnDrawGizmos()
    {
        //if(!_gameRunning) return;
        if(transform.hasChanged) _currentPosition = transform.position;

        for(int i = 0; i < _waypoints.Length; i++)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawSphere(_waypoints[i] + _currentPosition, 0.05f);

            if(i < _waypoints.Length - 1)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawLine(_waypoints[i] + _currentPosition, _waypoints[i + 1] + _currentPosition);
            }
        }
    }
}
