using System.ComponentModel.Design;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Gets the rigidbody of the player
    public Rigidbody playerRb;
    // Defines variables
    public float walkSpeed, runSpeed, jumpForce, dodgeSpeed;

    // Variables needed for jumping
    public LayerMask whatIsGround;
    public Transform groundPoint;
    private bool isGrounded;

    // Gets user input
    private Vector2 moveInput;

    // Update is called once per frame
    void Update()
    {
        // Fetches input from WASD or arrow keys (from Unitys input manager)
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize(); // Makes movement more smooth

        // If player is pressing down LEFT SHIFT
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Lets the player run
            playerRb.velocity = new Vector3(moveInput.x * runSpeed, playerRb.velocity.y, moveInput.y * runSpeed);
        } else
        {
            // Lets the player walk
            playerRb.velocity = new Vector3(moveInput.x * walkSpeed, playerRb.velocity.y, moveInput.y * walkSpeed);
        }

        // Check if the player is touching the ground

        // Check if the user is pressing SPACE to perform jump

        // Lets the player jump
    }
}
