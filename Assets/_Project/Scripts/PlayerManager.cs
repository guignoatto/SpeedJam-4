using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static float numberOfItems = 0;
    public int maxItems = 3;

    public static float playerSpeedMultiplier = 1;
    public static bool CanCarryMoreItems()
    {
        return numberOfItems < PlayerManager.Instance.maxItems;
    }



    // Singleton instance
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(PlayerManager).Name;
                    _instance = obj.AddComponent<PlayerManager>();
                }
            }
            return _instance;
        }
    }
}
