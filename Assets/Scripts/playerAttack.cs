using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Transform attackOrigin;
    [SerializeField] float radius;
    public float attackDelay = 0.3f;
    private bool attackBlocked = false;
    Animator animator;
    static public bool isAttacking;
    [SerializeField] LayerMask EnemyLayer;

    public playerMovement playerMovement;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            attack();
        }
    }

    private void attack()
    {
        if (attackBlocked)
        {
            return;
        }
        else
        {
            animator.SetTrigger("attack");

            DetectEnemy();

            attackBlocked = true;
            isAttacking = true;
            StartCoroutine(DelayAttack());
        }
        
    }

    private IEnumerator DelayAttack()
    {

        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = attackOrigin == null ? Vector3.zero : attackOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectEnemy()
    {
        // Checks which direction the player is facing
        Vector3 raycastDirection = transform.right;
        if(!playerMovement.isFacingRight)
        {
            raycastDirection = -transform.right;
        }

        // Casts a raycast checking for enemy
        RaycastHit hit;
        if(Physics.Raycast(attackOrigin.position, raycastDirection, out hit, 0.5f, EnemyLayer))
        {
            // Debug if enemy is hit
            Debug.Log(hit.collider.name);

            // Debug the hit point
            Debug.DrawLine(attackOrigin.position, hit.point, Color.red, 2.0f);
        }
        else
        {
            Debug.Log("Nothing Hit");
        }
    }
}
