using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Turns the Character Controller into a object
    public CharacterController playerController;
    
    //Movement
    public float playerSpeed = 12f;
    public float jumpHeight = 3f;
    bool isSprinting;

    //GroundCheck
    public Transform groundCheck;
    public float groundDistance = 0.04f;
    public LayerMask groundMask;
    bool isGrounded;
   

    //gravity
    public Vector3 velocity;
    public float gravity = -9.81f;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
     
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Player WASD input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Walking
        Vector3 move = transform.right * x + transform.forward * z;
        playerController.Move(move * playerSpeed * Time.deltaTime);

        //Jumping
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }

        //Sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0 ;
        if(isSprinting)
        {
            playerSpeed = 12f;
        }
        else
        {
            playerSpeed = 6f;
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);

        
    }
}
