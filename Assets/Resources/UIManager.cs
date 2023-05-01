using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
    }


    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _gameOverPanel;

    public void CloseBuyPanel() => _buyPanel.SetActive(false);
    public void CloseUpgradePanel() => _upgradePanel.SetActive(false);
    public void OpenBuyPanel() => _buyPanel.SetActive(true);
    public void OpenUpgradePanel() => _upgradePanel.SetActive(true);
    public void DisplayGameOverPanel() => _gameOverPanel.SetActive(true);
    public void HideGameOverPanel() => _gameOverPanel.SetActive(false);
}
