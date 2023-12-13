using System;
using System.Collections;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    public float walkSpeed, runSpeed, jumpForce, dodgeSpeed;
    private bool raycast;
    public SpriteRenderer playerSprite;

    public LayerMask groundLayer;
    public Transform groundPoint;
    private bool canJump;

    private Vector2 moveInput;

    bool isFacingRight = true;
     
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize(); // Makes movement more smooth

        // Moves left or right either walk or sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerRb.velocity = new Vector3(moveInput.x * runSpeed, playerRb.velocity.y, moveInput.y * runSpeed);
        } else
        {
            playerRb.velocity = new Vector3(moveInput.x * walkSpeed, playerRb.velocity.y, moveInput.y * walkSpeed);
        }

        if(moveInput.x < 0 && isFacingRight)
        {
            flip();
        }
        else if(moveInput.x > 0 && !isFacingRight) {
            flip();
        }


        RaycastHit hit;
        if (raycast = Physics.Raycast(groundPoint.position, Vector3.down, out hit, .01f, groundLayer))
        {
            canJump = true;
            Debug.Log(canJump);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            Debug.Log(canJump);
            playerRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);

        }
    }

    void flip()
    {
        if (isFacingRight)
        {
            isFacingRight = !isFacingRight;
            playerSprite.flipX = true;
        }
           
        else
        {
            isFacingRight = true;
            playerSprite.flipX = false;
        }
        


    }

}
