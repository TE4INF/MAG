using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDash : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    // public Transform playerCam;
    private Rigidbody rb;
    private playerMovement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<playerMovement>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(dashKey)) 
        {
            Dash();
        }
    }

    private void Dash()
    {
        Vector3 forceToApply = orientation.forward * dashForce;

        rb.AddForce(forceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetDash), dashDuration);
    }


    private void ResetDash()
    {

    }

}
