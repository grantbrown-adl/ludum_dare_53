using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private float _initialGold;
    [SerializeField] private float _currentGold;
    [SerializeField] private float _initialHealth;
    [SerializeField] private float _currentHealth;
    private static ResourceManager _instance;

    public static ResourceManager Instance { get => _instance; private set => _instance = value; }
    public float CurrentGold { get => _currentGold; set => _currentGold = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;

        _currentGold = _initialGold;
        _currentHealth = _initialHealth;
    }



    public void UpdateGold(float value)
    {
        float updatedGold = _currentGold + value;
        if(updatedGold < 0) return;
        _currentGold = updatedGold;
    }

    public void UpdateHealth(float value)
    {
        float updatedHealth = _currentHealth + value;
        if(updatedHealth <= 0)
        {
            updatedHealth = 0;
            UIManager.Instance.DisplayGameOverPanel();
        }
        _currentHealth = updatedHealth;
    }    
}
