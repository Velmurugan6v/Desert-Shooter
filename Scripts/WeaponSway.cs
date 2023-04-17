using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] float smooth;
    [SerializeField] float swaySenstivity;

    private float xinput;
    private float yinput;

    private Quaternion xRotation;
    private Quaternion yRotation;

    
    
    void Update()
    {
        xinput = Input.GetAxisRaw("Mouse X") * swaySenstivity;
        yinput = Input.GetAxisRaw("Mouse Y") * swaySenstivity;

        xRotation = Quaternion.AngleAxis(-yinput, Vector3.right);
        yRotation = Quaternion.AngleAxis(xinput, Vector3.up);

        Quaternion targetRotation = xRotation * yRotation;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
