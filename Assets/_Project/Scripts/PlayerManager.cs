using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float numberOfItems = 0;
    public int maxItems = 3;

    public float playerSpeedMultiplier = 1f;

    public bool CanCarryMoreItems()
    {
        return numberOfItems < maxItems;
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
