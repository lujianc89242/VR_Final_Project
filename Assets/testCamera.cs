using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour
{
    PlayerControls controls;
    [SerializeField] float mouseSensitivity;

    float xRotation;

    Transform playerBody;

    Vector2 lookVector; 
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controls.Gameplay.View.performed += context => lookVector = context.ReadValue<Vector2>();

        playerBody = transform.parent;

        //view
        
    }

    // Update is called once per frame
    void Update()
    {
        var MouseX = lookVector.x * mouseSensitivity * Time.deltaTime;
        var MouseY = lookVector.y * mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * MouseX);

    }
}
