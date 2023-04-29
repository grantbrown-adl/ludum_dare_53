using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResources : MonoBehaviour
{
    [SerializeField] private bool isPaused;

    public bool IsPaused { get => isPaused; set => isPaused = value; }

    private void Start()
    {
        // Ensure instance is loaded
        //GameAssets.instance.LoadGameAssets();
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
