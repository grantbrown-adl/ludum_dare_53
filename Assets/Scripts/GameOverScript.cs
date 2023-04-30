using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private void OnEnable()
    {
        //Pause the game
        PauseManager.Instance.IsPaused = true;
    }
}
