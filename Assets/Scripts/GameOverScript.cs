using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private void Awake()
    {
        //Pause the game
        //PauseManager.Instance.IsPaused = true;
        Time.timeScale = 0;
    }
}
