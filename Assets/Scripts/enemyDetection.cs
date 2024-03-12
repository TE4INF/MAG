using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class enemyDetection : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    Animator animator;

    public bool isFacingRight = true;

    [SerializeField] SpriteRenderer enemySprite;

    //patroll
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //attack
    public float timeBetweenAttacks;

    public float timeAttackDelay;
    bool alreadyAttacked;
    [SerializeField] private float EnemyDamage;
    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, facingRight = true;

    //sounds
    public AudioClip enemyAttackSound;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //Check sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        //Debug.Log(agent.velocity.x);

        if (agent.velocity.x < 0 && isFacingRight) { flip(); }
        if (agent.velocity.x > 0 && !isFacingRight) { flip(); }

    }

    void flip()
    {
        if (isFacingRight)
        {
            isFacingRight = !isFacingRight;
            enemySprite.flipX = true;
        }
        else
        {
            isFacingRight = true;
            enemySprite.flipX = false;
        }

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }


    private void AttackPlayer()
    {
        //Make sure enemy doesnt move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here
            alreadyAttacked = true;
            AudioSource.PlayClipAtPoint(enemyAttackSound, transform.position);
            //Play animation here
            animator.SetTrigger("attacking");
            Debug.Log("play anim");

            Invoke(nameof(AttackDelay), timeAttackDelay);

        }
    }

    private void AttackDelay()
    {
        if (playerInSightRange && playerInAttackRange)
        {
            playerMovement.main.takeDamage(EnemyDamage);
            Debug.Log("Damage");
        }

        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
