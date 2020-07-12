using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [Header("Assign")]

    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    [Header("Attack")]
    public float attackRange = 2f;
    public float damage = 5f;
    public float holdTime = 3f;

    private float holdCounter = 0f;
    public GameObject[] p;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
           
            holdCounter += Time.deltaTime;
            p[0].SetActive(true);
            p[1].SetActive(true);
            animator.SetBool("Charge", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
                Attack();
                holdCounter = 0;
            StartCoroutine(GlowDisappear());
        }
    }

    IEnumerator GlowDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        p[0].SetActive(false);
        p[1].SetActive(false);
    }

    void Attack()
    {
        animator.SetBool("Stab", true);
        animator.SetBool("Charge", false);

        HitDetection(holdCounter);
    }
    public void StabReset()
    {
        animator.SetBool("Stab", false);
    }

    public void HitDetection(float holdDamage)
    {
        Collider[] hitEneimes = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEneimes)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage+holdDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
