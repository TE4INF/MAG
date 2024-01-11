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
    private bool attackBlocked;
    Animator animator;
    static public bool isAttacking;
    
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left-click was pressed");
            attack();
        }
    }

    private void attack()
    {
        if (attackBlocked) return;
        animator.SetTrigger("attack");
        attackBlocked = true;
        isAttacking = true;
        StartCoroutine(DelayAttack());
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
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(attackOrigin.position, radius)) 
        {
            Debug.Log(collider.name);
        }
    }
}
