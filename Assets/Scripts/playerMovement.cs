using System;
using System.Collections;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public static playerMovement main;

    [Header("Player references")]
    [SerializeField] Rigidbody playerRb;
    [SerializeField] SpriteRenderer playerSprite;
    Vector3 moveDirection;
    Rigidbody rb;
    public Image Health;

    [Header("Forces")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float dashForce;

    [Header ("Conditions")]
    private bool canJump;
    private bool canDash = true;
    bool isDashing = false;
    bool isFacingRight = true;
    private bool raycast;
    Animator animator;

    [Header("World")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundPoint;

    [Header("Input")]
    [SerializeField] KeyCode dashKey = KeyCode.E;
    private Vector2 moveInput;
    

    [Header("Dash Settings")]
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    private float dashCooldownTimer;
    public GameObject CD;
    public Image FILLBAR;

    [Header("Player attributes")]
    [SerializeField]private float PlayerHealth = 1;

    private void Awake() {
        main = this;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;

        if (moveDirection == Vector3.zero)
        {
            animator.SetFloat("Speed", 0);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Speed", 1);
        }
        else 
        {
            animator.SetFloat("Speed", 2);
        }

        if(Input.GetKeyDown(dashKey))
        {
            Dash();
        }

        if(dashCooldownTimer > 0) 
        { 
            dashCooldownTimer -= Time.deltaTime;
        }

        // Jump
        RaycastHit hit;
        if (raycast = Physics.Raycast(groundPoint.position, Vector3.down, out hit, .01f, groundLayer))
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            playerRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }

        // Flips the sprite according to movement
        if (moveInput.x < 0 && isFacingRight)
        {
            flip();
        }
        else if (moveInput.x > 0 && !isFacingRight)
        {
            flip();
        }

        if(CD == true)
        {
            FILLBAR.fillAmount -= 1f * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {

        // Moves left or right either walk or sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerRb.velocity = new Vector3(moveDirection.x * runSpeed, playerRb.velocity.y, moveDirection.y * runSpeed);
        }
        else
        {   
            playerRb.velocity = new Vector3(moveDirection.x * walkSpeed, playerRb.velocity.y, moveDirection.y * walkSpeed);
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

    private void Dash()
    {
        if (dashCooldownTimer > 0) return;
        else dashCooldownTimer = dashCooldown;

        FILLBAR.fillAmount = 1;
        isDashing = true;
        canDash = false;


        if (moveDirection == Vector3.zero) return;
        else
        {
            CD.SetActive(true);
            animator.SetTrigger("dash");
        }

        playerRb.AddForce(moveDirection.x * dashForce, 0, moveDirection.y * dashForce, ForceMode.Impulse);
        Invoke(nameof(resetDash), dashDuration);
    }

    private void resetDash()
    {
        CD.SetActive(false);
        canDash = true;
        isDashing = false;
    }

    public void takeDamage(float Damage)
    {
        PlayerHealth -= Damage;
        Health.fillAmount -= Damage;
    }
}
