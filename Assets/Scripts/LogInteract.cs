using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (PlayerManager.CanCarryMoreItems())
        {
            PlayerManager.numberOfItems++;
            PlayerManager.playerSpeedMultiplier = 1/(float)Math.Sqrt(PlayerManager.numberOfItems);
            Debug.Log(PlayerManager.playerSpeedMultiplier);
            //Debug.Log("+1 Item");
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
