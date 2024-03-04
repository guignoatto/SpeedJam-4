using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> logs;

    public int numberOfItems = 0;
    public int maxItems = 3;

    public float playerSpeedMultiplier = 1;

    public UiManager textUi;


    public bool CanCarryMoreItems()
    {
        return numberOfItems < maxItems;
    }

    public void SetNumberOfItems(int num)
    {
        numberOfItems = num;
        if (num == 0)
        {
            foreach (var log in logs)
            {
                log.SetActive(false);
            }
            UpdateLogUI();
            return;
        }
        logs[num - 1].SetActive(true);
        UpdateLogUI();
    }

    public void SpeedBuff(float speedBuff, float speedBuffDuration)
    {
        StopAllCoroutines();
        StartCoroutine(ActivateSpeedBuff(speedBuff, speedBuffDuration));
    }

    private IEnumerator ActivateSpeedBuff(float speedBuff, float speedBuffDuration)
    {
        playerSpeedMultiplier = playerSpeedMultiplier * speedBuff;
        yield return new WaitForSeconds(speedBuffDuration);
        playerSpeedMultiplier = 1f;

    }

    public void PickUpLog()
    {
        SetNumberOfItems(numberOfItems + 1);
        playerSpeedMultiplier = 1 / (float)Math.Sqrt(numberOfItems);
    }

    private void UpdateLogUI()
    {
        if (textUi != null)
        {
            textUi.UpdateLogText();
        }
        else
        {
            Debug.LogWarning("LogUi reference not set in PlayerManager!");
        }
    }

    // Singleton instance
    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<PlayerManager>();
            if (Instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(PlayerManager).Name;
                Instance = obj.AddComponent<PlayerManager>();
            }
        }
    }
   
}
