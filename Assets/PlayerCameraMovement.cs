using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    // Define constants
    private const float low_limit = 0.0f;
    private const float high_limit = 85.0f;

    // Editor Options
    public GameObject Player;
    public GameObject theCamera;
    float curZoomPos, zoomTo; // curZoomPos will be the value
	float zoomFrom = 20f; //Midway point between nearest and farthest zoom values (a "starting position")
    public float followDist = 5.0f;
    public float mouseSensX = 35.0f;
    public float mouseSensY = 15.0f;
    public float heightOffset = 0.5f;
    public float zoomSpeed = 100f; // Adjustable zoom speed
    
    // Private variables are hidden from the editor
    private float currentZoomLevel = 95.0f; // Initial FOV

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

        float scrollDelta = Input.GetAxis("Mouse ScrollWheel"); // Get Scrollwheel input
        currentZoomLevel -= scrollDelta * zoomSpeed; // Adjust zoom level based on scroll
        Camera.main.fieldOfView = Mathf.Clamp(currentZoomLevel, 10f, 120f); // Smooth Camera FOV zoom

    }
}
