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
            Debug.Log("+1 Item");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Max Items Reached");
        }
   
    }
}
