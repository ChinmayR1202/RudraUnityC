using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 15f;
    public float walkSpeed = 10f;
    private float playerSpeed;
    bool jump = false;

    // Dashing
    private bool dashButtonPressed;

    // Animation
    private bool isWalking;

    public bool isMovementLocked = false;
    private Animator animLock;


    // Start is called before the first frame update
    void Start()
    {
        animLock = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // To check if the player is walking or running.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = walkSpeed;
            isWalking = true;
        }
        else
        {
            playerSpeed = runSpeed;
            isWalking = false;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;


        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

        }
        else
        {
            jump = false;
        }

        // Dashing
        CheckDash();
        // Player movement  
        controller.movementDir(horizontalMove * Time.deltaTime, isWalking);
        
        
    }

    private void FixedUpdate()
    {
        // Movement of Character 
        if (!isMovementLocked)
        {            
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, dashButtonPressed);
            jump = false;
            dashButtonPressed = false;            
        }
        LockMoveAnim();
    }


    private void LockMoveAnim()
    {
        if (isMovementLocked)
        {
            animLock.SetBool("movementLock", true);
        }
        else
        {
            animLock.SetBool("movementLock", false);
        }
    }

    private void CheckDash()
    {
        if (Input.GetButton("Dash"))
        {
            dashButtonPressed = true;
        }
        else
        {
            dashButtonPressed = false;
        }
    }
}
