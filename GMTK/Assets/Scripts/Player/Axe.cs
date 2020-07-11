using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [Header("Assign")]

    public Animator animator;
    public Animator player;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    [Header("Attack")]
    public float attackRange = 2f;
    public float damage = 5f;
    public float holdTime=3f;
    public float swingDamage=5f;

    private float holdCounter=0f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Swing", false);
            animator.SetBool("Spin", false);
            animator.SetBool("ChargeAxe", true);
            holdCounter+=Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (holdCounter >= holdTime)
            {
                holdCounter = 0;
                SpinAttack();
            }
            else
            {
                holdCounter = 0;
                Attack();
            }
        }
    }
    
    void Attack()
    {
        animator.SetBool("Swing", true);
        HitDetection();
        animator.SetBool("ChargeAxe", false);

    }
    void SpinAttack()
    {
        animator.SetBool("Spin", true);
        HitDetection();
        animator.SetBool("ChargeAxe", false);
    }
    public void SpinPlayer()
    {
        player.SetTrigger("Spin");
    }
public void HitDetection()
    {
        Collider[] hitEneimes = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEneimes)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage+swingDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
