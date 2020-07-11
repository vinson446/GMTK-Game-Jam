using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpear : MonoBehaviour
{
    [Header("Assign")]

    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public LayerMask Ally;

    [Header("Stats")]
    public float damageBuff = -1;
  

//[Header("Attack")]


    void Start()
    {

        animator = GetComponent<Animator>();
        

    }

    void Update()
    {

    }



    public void Attack(Transform meleeAtkPoint, float atkRange, float damage, bool isAlly)
    {
        animator.SetTrigger("Stab");
        if (isAlly != true)
        {
            Collider[] enemiesHit = Physics.OverlapSphere(meleeAtkPoint.position, atkRange, Ally);
            foreach (Collider e in enemiesHit)
            {
                Debug.Log(e.name +"Ally Spear");
                if (e.GetComponent<Allies>() != null)
                    e.GetComponent<Allies>().TakeDamage(damage);
            }
        }
        else
        {
            Collider[] enemiesHit = Physics.OverlapSphere(meleeAtkPoint.position, atkRange, enemyLayer);
            foreach (Collider e in enemiesHit)
            {
                Debug.Log(e.name + "Enemy Spear");

                if (e.GetComponent<Enemy>() != null)
                    e.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}