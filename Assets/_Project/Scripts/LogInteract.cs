using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (PlayerManager.Instance.CanCarryMoreItems())
        {
            PlayerManager.Instance.PickUpLog();
;           Debug.Log(PlayerManager.Instance.playerSpeedMultiplier);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Max Items Reached");
        }
   
    }

    public void StopInteract()
    {
        return ;
    }
}
