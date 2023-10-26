using System;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Gets the rigidbody of the player
    public Rigidbody playerRb;
    // Defines variables
    public float walkSpeed, runSpeed, jumpForce, dodgeSpeed;

    // Variables needed for jumping
    public LayerMask groundLayer;
    public Transform groundPoint;
    private bool canJump;

    // Gets user input
    private Vector2 moveInput;

    // Update is called once per frame
    void Update()
    {
        // Fetches input from WASD or arrow keys (from Unitys input manager)
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize(); // Makes movement more smooth

        // Player presses down LEFT SHIFT
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Run
            playerRb.velocity = new Vector3(moveInput.x * runSpeed, playerRb.velocity.y, moveInput.y * runSpeed);
        } else
        {
            // Walk
            playerRb.velocity = new Vector3(moveInput.x * walkSpeed, playerRb.velocity.y, moveInput.y * walkSpeed);
        }

        RaycastHit hit;
        if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, groundLayer))
        {
            canJump = true;
        }

        // Check if the user is pressing SPACE to perform jump
        if (Input.GetKeyUp(KeyCode.Space) && canJump)
        {
            canJump = false;
            // Lets the player jump
            playerRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }
}
