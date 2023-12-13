using System;
using System.Collections;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Player references")]
    public Rigidbody playerRb;
    public float walkSpeed, runSpeed, jumpForce, dodgeSpeed;
    public SpriteRenderer playerSprite;

    [Header ("Conditions")]
    private bool canJump;
    private bool raycast;
    bool isFacingRight = true;

    [Header("World")]
    public LayerMask groundLayer;
    public Transform groundPoint;

    [Header("Input")]
    private Vector2 moveInput;
     
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
