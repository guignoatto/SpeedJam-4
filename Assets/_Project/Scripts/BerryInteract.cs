using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private float speedBuffDuration = 2f;
    [SerializeField] private float speedBuff = 2f;
    private float initialMultiplierSpeed = 1f;
    public void Interact()
    {

        initialMultiplierSpeed = PlayerManager.Instance.playerSpeedMultiplier;
        Debug.Log(initialMultiplierSpeed);
        PlayerManager.Instance.SpeedBuff(speedBuff, speedBuffDuration);
        Debug.Log("Eaten");
        Destroy(gameObject);
    }

    public void StopInteract()
    {
        return;
    }


}
