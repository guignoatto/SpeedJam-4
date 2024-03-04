using System;
using UnityEngine;


public class BarricadeManager : MonoBehaviour
{
    private int _barricadeStage = 0;
    private int _currentLogs = 0;

    private BarricadeController _barricadeController;

    private void Start()
    {
        _barricadeController = FindAnyObjectByType<BarricadeController>();
    }

    private void SetLogActive()
    {
        _barricadeController.SetLogActive(_barricadeStage);
        _barricadeStage++;
        _currentLogs -= 3;
    }

    private void AddLog()
    {
        _currentLogs += PlayerManager.Instance.numberOfItems;
        PlayerManager.Instance.SetNumberOfItems(0);
        PlayerManager.Instance.playerSpeedMultiplier = 1;
        if (_currentLogs >= 3)
        {
            SetLogActive();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MovementStateManager m))
        {
            AddLog();          
        }
    }
}