using System;
using System.Collections;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public Transform attackPoint;
    public static playerMovement main;

    [Header("Player references")]
    [SerializeField] Rigidbody playerRb;
    [SerializeField] SpriteRenderer playerSprite;
    public Vector3 moveDirection;
    Rigidbody rb;
    public Image Health;
    public Image Shield;

    [Header("Forces")]
    [SerializeField] private float walkSpeed = 0.7f;
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] float jumpForce;
    [SerializeField] float dashForce;

    [Header("Conditions")]
    private bool canJump;
    // private bool canDash = true;
    // bool isDashing = false;
    public bool isFacingRight = true;
    private bool raycast;
    Animator animator;

    [Header("World")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundPoint;

    [Header("Input")]
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;//KeyCode.E;

    private Vector2 moveInput;


    [Header("Dash Settings")]
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    private float dashCooldownTimer;
    public GameObject dashCooldownObject;
    public Image dashFillbar;

    [Header("Player attributes")]
    [SerializeField] private float PlayerHealth = 1f;
    [SerializeField] public float ShieldHealth = 0f;
    [SerializeField] public int Kills = 0;
    public TextMeshProUGUI scoreText;
    public GameObject WaveUI;
    public GameObject deathScene;
    public GameObject uiHub;
    public GameObject pauseMenuUI;
    private float elapsedTime = 0f;

    private Vector3 flipAttack;

    [Header("Sounds")]
    public AudioClip playerHurtSound;
    public AudioClip playerDeathSound;

    [Header("Leveling stuff")]
    public float maxHealth = 1f;
    public float maxShield = 1f;
    private float baseWalk;
    private float baseRunSpeed;
    private float baseHealth;
    public int SpeedLevel;
    [SerializeField] private int HealthLevel;
    private float extrahealth;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        baseHealth = PlayerHealth;
        baseRunSpeed = runSpeed;
        baseWalk = walkSpeed;
        PlayerHealth = maxHealth;

        flipAttack = attackPoint.localPosition;
        animator = GetComponentInChildren<Animator>();
        scoreText.text = "Kills: " + playerMovement.main.Kills;
        WaveUI.SetActive(true);

        UpdateUI();
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        elapsedTime += Time.deltaTime;

        moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;

        if (moveDirection == Vector3.zero)
        {
            animator.SetFloat("Speed", 0);
        }
        
        else
        {
            animator.SetFloat("Speed", 2);
        }

        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }

        if (dashCooldownTimer > 0)
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
        // if (moveInput.x < 0 && isFacingRight)
        // {
        //     flip();
        // }
        // else if (moveInput.x > 0 && !isFacingRight)
        // {
        //     flip();
        // }
        if (dashCooldownObject == true)
        {
            dashFillbar.fillAmount -= 1f /dashDuration * Time.deltaTime;
        }

        if (Input.mousePosition.x < Screen.width / 2f)
        {
            // => left half
            playerSprite.flipX = true;
        }
        else
        {
            // => right half
            playerSprite.flipX = false;
        }

    }

    private void FixedUpdate()
    {

        // Moves left or right either walk or sprinting
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    playerRb.velocity = new Vector3(moveDirection.x * runSpeed, playerRb.velocity.y, moveDirection.y * runSpeed);
        //}
        //else
        //{
        playerRb.velocity = new Vector3(moveDirection.x * walkSpeed, playerRb.velocity.y, moveDirection.y * walkSpeed);
        //}
    }

    // void flip()
    // {
    //     if (isFacingRight)
    //     {
    //         isFacingRight = !isFacingRight;
    //         playerSprite.flipX = true;
    //         //attackPoint.localPosition = new Vector3(flipAttack.x * -1, flipAttack.y, flipAttack.z);
    //     }

    //     else
    //     {
    //         isFacingRight = true;
    //         playerSprite.flipX = false;
    //         //attackPoint.localPosition = new Vector3(flipAttack.x, flipAttack.y, flipAttack.z);
    //     }
    // }
    void UpdateUI()
    {
        Health.fillAmount = PlayerHealth / maxHealth;
        Shield.fillAmount = ShieldHealth / maxShield;
    }

    private void Dash()
    {
        if (dashCooldownTimer > 0) return;
        else dashCooldownTimer = dashCooldown;

        dashFillbar.fillAmount = 1;
        // isDashing = true;
        // canDash = false;

        if (moveDirection == Vector3.zero) return;
        else
        {
            dashCooldownObject.SetActive(true);
            animator.SetTrigger("dash");
        }

        playerRb.AddForce(moveDirection.x * dashForce, 0, moveDirection.y * dashForce, ForceMode.Impulse);
        Invoke(nameof(resetDash), dashDuration);
    }

    private void resetDash()
    {
        dashCooldownObject.SetActive(false);
        // canDash = true;
        // isDashing = false;
    }

    public void takeDamage(float Damage)
    {
        if (ShieldHealth > 0)
        {
            AudioSource.PlayClipAtPoint(playerHurtSound, moveDirection);
            ShieldHealth -= Damage;
        
        if (ShieldHealth < 0)
        {
            PlayerHealth += ShieldHealth;
            ShieldHealth = 0;
        }
        }
        else
        {
            PlayerHealth-= Damage;
        }

        UpdateUI();

        if(PlayerHealth <= 0)
        {
           AudioSource.PlayClipAtPoint(playerDeathSound, moveDirection);

            deathScene.SetActive(true);
            uiHub.SetActive(false);
            WaveUI.SetActive(false);
            Time.timeScale = 0;
            Destroy(pauseMenuUI);
            Display.main.Dead(); 
        }
    }
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void kill(string typeofenemy)
    {
        Kills++;
        updateScore();
        XPManager.main.CalculateXP(typeofenemy);
    }
    public void updateScore()
    {
        scoreText.text = "Kills: " + playerMovement.main.Kills;
    }
    public float UpgradeSpeed()
    {
        SpeedLevel++;
        walkSpeed = CalculateWalkSpeed();
        runSpeed = CalculateRunSpeed();
        runspeedfix();
        return walkSpeed;
    }
    private float runspeedfix()
    {
        return runSpeed;
    }
    public float UpgradeHealth()
    {
        HealthLevel++;
        PlayerHealth = CalculateHealth();
        float totalhealth = CalculateHealth();
        float remaningHealth = Mathf.Max(totalhealth - maxHealth, 0f);
        PlayerHealth = Mathf.Min(totalhealth, maxHealth);
        if(remaningHealth > 0)
        {
            extrahealth = remaningHealth;
            UpgradeShield(extrahealth);
        }
        UpdateUI();
        return PlayerHealth;
    }
    private float CalculateWalkSpeed()
    {
        return baseWalk * Mathf.Pow(SpeedLevel,0.6f);
    }
    private float CalculateRunSpeed()
    {
        return baseRunSpeed * Mathf.Pow(SpeedLevel, 0.6f);
    }
    private float CalculateHealth()
    {
        return PlayerHealth * Mathf.Pow(HealthLevel, 0.6f);
    }
    private void UpgradeShield(float extrahealth)
    {
        ShieldHealth = Mathf.Min(extrahealth, maxShield);
        UpdateUI();
    }
}