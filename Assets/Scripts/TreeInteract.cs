using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TreeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactMaxTime;
    [SerializeField] private GameObject Log;
    private bool _startTimer = false;
    private bool _canInteract = false;
    private float timer;
    // Start is called before the first frame update
    public void Interact()
    {
        _startTimer = true;
        
    }

    public void StopInteract()
    {
        _startTimer = false;
        timer = 0;    
    }

    private void Update()
    {
        if(_startTimer && _canInteract){
            timer += Time.deltaTime;
            Debug.Log("Timer Start");
            if(timer >= interactMaxTime)
            {
           
                int RandomDrop = UnityEngine.Random.Range(1,4);

                for (int i = 0; i < RandomDrop ; i++)
                {
                    Instantiate(Log, transform.position, Quaternion.identity);
                }
                //StopInteract();
                //Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out MovementStateManager player))
        {
            _canInteract = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MovementStateManager player))
        {
            _canInteract = false;
            StopInteract() ;
            
        }

    }
}
