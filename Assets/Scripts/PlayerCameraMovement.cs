using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{

    // Editor Options
    // Private variables are hidden from the editor (Anything wihtout public before it)

    // Camera controlls, all adjustable
    public GameObject theCamera;
    public float followDist = 5.0f; // Camera distance from Gameobject
    public float mouseSensX = 35.0f;
    public float mouseSensY = 15.0f;
    public float heightOffset = 0.5f;
    private const float low_limit = 0.0f; // if below 0 it flips
    private const float high_limit = 85.0f; // Max camera height

    // Initialization
    void Start()
    {
        // Place the camera, set forward vector to match player
         theCamera.transform.forward = gameObject.transform.forward;
         // Keep Cursor Visible and change lock mode to None
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {

            if (Input.GetMouseButton(1))
            {
             // Rotation around the player
            Vector2 cameraMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            

                // Adjust Rotation based on captured mouse movement
                // Clamp pitch (X angle) of camera to avoid flipping
                // Adjust values to acount for mouse sensetivity settings
                theCamera.transform.eulerAngles = new Vector3(
                    Mathf.Clamp(theCamera.transform.eulerAngles.x + -cameraMovement.y * mouseSensY, low_limit, high_limit),
                    theCamera.transform.eulerAngles.y + cameraMovement.x * mouseSensX, 0);

                
            }
            
        // Player position + height offset
        theCamera.transform.position = gameObject.transform.position + new Vector3(0, heightOffset, 0);


        //Move to desired follow distance
        theCamera.transform.position -= theCamera.transform.forward * followDist;

        Camera.main.fieldOfView = 85; // Setting FOV to 85

    }
}
