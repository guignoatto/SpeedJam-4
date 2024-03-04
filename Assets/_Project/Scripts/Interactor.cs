using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
    public void StopInteract();
}
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractorRange;
    public float InteractorThickness;
    public float YOffsetAmount;
    public float XOffsetAmount;
    public float ZOffsetAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hitInfo;
            Vector3 origin = InteractorSource.position + new Vector3(XOffsetAmount, YOffsetAmount, ZOffsetAmount);
            if (Physics.SphereCast(origin, InteractorThickness, InteractorSource.forward, out hitInfo, InteractorRange))
            {
                if (hitInfo.collider.TryGetComponent(out IInteractable interactableObject))
                {
                    interactableObject.Interact();
                }
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = InteractorSource.position + new Vector3(XOffsetAmount, YOffsetAmount, ZOffsetAmount);
        Gizmos.DrawWireSphere(origin, InteractorThickness);
    }
}
