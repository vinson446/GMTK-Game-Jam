using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Assign")]

    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public GameObject trails;
    [Header("Attack")]
    public float attackRange = 2f;
    public float damage = 5f;

    public float attackTimmer=2f;
    public int numberOfClicks = 0;
    public float comboDelay =2f;
    float lastClickTime =0f;
   

    void Start()
    {

        animator = GetComponent<Animator>();

    }

     void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            trails.gameObject.SetActive(true);

           Debug.Log("Swing 1");
            Attack();
        }

            //trails.gameObject.SetActive(false);
    }

   

    void Attack()
    {
        animator.SetBool("Attack1", true);
        HitDetection();

    }

    public void Swing1()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Attack2", true);
            HitDetection();
          
        }
        else
        {
            animator.SetBool("Attack1", false);
        }
    }

    public void Swing2()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Attack3", true);
            HitDetection();
        }
        else
        {
            animator.SetBool("Attack2", false);
        }
    }
    public void Swing3()
    {
        if (Input.GetMouseButton(0))
        {
            HitDetection();

        }
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
    }



    public void HitDetection()
    {
        Collider[] hitEneimes = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEneimes)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
