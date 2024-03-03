using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
