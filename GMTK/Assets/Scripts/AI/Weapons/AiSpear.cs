using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpear : MonoBehaviour
{
    [Header("Assign")]

    public Animator animator;
    public LayerMask enemyLayer;
    public LayerMask Ally;
    [Header("AttackStats")]
    public float attakHeight = 1;
    public float atkRange = 1;
    public Transform attackPoint;
    [Header("Stats")]
    public float damageBuff = -1;


    //[Header("Attack")]

    public Vector3 attackEndPoint;

    void Start()
    {
        animator = GetComponent<Animator>();

        attackEndPoint = new Vector3(attackPoint.position.x, attackPoint.position.y + attakHeight, attackPoint.position.z);
    }

    void Update()
    {

    }



    public void Attack(float damage, bool isAlly)
    {
        animator.SetTrigger("Stab");
        if (isAlly != true)
        {
            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, atkRange, Ally);
            foreach (Collider e in enemiesHit)
            {
                Debug.Log(e.name + "Ally Spear");
                if (e.GetComponent<Allies>() != null)
                    e.GetComponent<Allies>().TakeDamage(damage);
            }
        }
        else
        {
            Collider[] enemiesHit = Physics.OverlapCapsule(attackPoint.position, attackEndPoint, atkRange, enemyLayer);
            foreach (Collider e in enemiesHit)
            {
                Debug.Log(e.name + "Enemy Spear");

                if (e.GetComponent<Enemy>() != null)
                    e.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}


