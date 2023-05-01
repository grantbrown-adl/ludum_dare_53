using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private bool isPaused;
    private static PauseManager _instance;
    [SerializeField] GameObject _pausePanel;

    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public static PauseManager Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        // Always unpause on start
        Time.timeScale = 1;
        if(IsPaused) IsPaused = false;
    }
    private void Update()
    {
        if(isPaused)
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }
}
