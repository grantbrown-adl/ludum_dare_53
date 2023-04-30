using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private bool isPaused;
    private static PauseManager _instance;

    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public static PauseManager Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        // Always unpause on start
        if(IsPaused) IsPaused = false;
    }
    private void Update()
    {
        if(isPaused) Time.timeScale = 0;
        else Time.timeScale = 1;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }
}
