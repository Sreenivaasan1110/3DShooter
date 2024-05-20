using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float xspeed;
    public float yspeed;
    float xRotation;
    float yRotation;
    public Transform orientation;
    void Awake()
    {
       // Cursor.lockState = CursorLockMode.Locked;
      //  Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xspeed;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * yspeed;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}