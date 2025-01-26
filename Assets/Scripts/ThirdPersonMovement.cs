using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject Camera;

    // Ground check for working gravity variable and jump math
    Vector3 velocity;
    public float gravity = -19.62f; // Adjustable gravity
    public float jumpHeight = 3f; // Adjustable Jump height
    public LayerMask groundMask;
    float groundDistance;
    public Transform groundCheck;
    bool isGrounded;
    
    // Movement speed and smooth turn
    public float speed = 6f; // Adjustable speed
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        // Basic Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Gravity and jump math
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); 

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
