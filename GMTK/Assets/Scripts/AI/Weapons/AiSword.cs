using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSword: MonoBehaviour
{
    [Header("Assign")]

    public Animator animator;
    public Transform attackPoint;
    public float atkRange = 1;
    public LayerMask enemyLayer;
    public LayerMask Ally;

    //[Header("Attack")]


    void Start()
    {

        animator = GetComponent<Animator>();

    }

    void Update()
    {
       
    }



    public void Attack(float damage, bool isAlly)
    {
        animator.SetTrigger("Attack1");

        if (isAlly != true)
        {
            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, atkRange, enemyLayer);
            foreach (Collider e in enemiesHit)
            {
                Debug.Log(e.name + "Ally Spear");
                if (e.GetComponent<Allies>() != null)
                    e.GetComponent<Allies>().TakeDamage(damage);
            }
        }
        else
        {
            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, atkRange, Ally);
            foreach (Collider e in enemiesHit)
            {
                Debug.Log(e.name + "Enemy Spear");

                if (e.GetComponent<Enemy>() != null)
                    e.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
