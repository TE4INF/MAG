using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack2 : MonoBehaviour
{
    public static attack2 main;


    public bool CanAttack = true;
    public bool IsAttacking = false;
    public float AttackCooldown = 1.0f;
    public float AttackTime = 1.0f;
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask enemyLayers;

    [SerializeField]private int attackDamage = 30;
    private int baseDamage;

    Animator animator;
    public AudioClip clip; 

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        baseDamage = attackDamage;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                Attack();
            }

        }


    }
    public void Attack()
    {
        CanAttack = false;
        IsAttacking = true;
        animator.SetTrigger("attack");
        StartCoroutine(waitAnimation());
        AudioSource.PlayClipAtPoint(clip, AttackPoint.position);


    }
    IEnumerator waitAnimation()
    {
        yield return new WaitForSeconds(AttackTime);
        Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Detected" + enemy.name);
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                enemy.GetComponent<Enemy>().FlashRed();
            }

        }

        StartCoroutine(ResetAttackCooldown());

    }


    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    
    public void UpgradeDamage()
    {
        baseDamage = CalculateNewDamage();
    }
    private int CalculateNewDamage()
    {
        return baseDamage + 5;
    }
}
