using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] float _cameraSenstivity = 100f;
    private float xInput;
    private float yInput;
    private float yRotationLimit;
    [SerializeField] Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Input
        xInput = Input.GetAxisRaw("Mouse X") * _cameraSenstivity * Time.deltaTime;
        yInput = Input.GetAxisRaw("Mouse Y") * _cameraSenstivity * Time.deltaTime;

        //Value Inverse
        yRotationLimit -= yInput;

        //Set Limite
        

        if (PlayerMovement.instance._ttp)
        {
            yRotationLimit = Mathf.Clamp(yRotationLimit, -10f, 10f);
        }
        else
        {
            yRotationLimit = Mathf.Clamp(yRotationLimit, -70f, 70f);
        }

        //Mouse and Player Rotation
        transform.localRotation = Quaternion.Euler(yRotationLimit, 0, 0);
        _player.Rotate(Vector3.up * xInput);
    }
}
