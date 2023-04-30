using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceDisplayScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentGold;
    [SerializeField] private TextMeshProUGUI _currentHealth;

    private void Update()
    {
        _currentGold.text = ResourceManager.Instance.CurrentGold.ToString();
        _currentHealth.text = ResourceManager.Instance.CurrentHealth.ToString();
    }
}
