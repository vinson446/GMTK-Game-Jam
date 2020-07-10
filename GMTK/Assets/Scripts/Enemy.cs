using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float currentHP;
    public float maxHP;
    public float damage;
    public float atkSpeed;
    public float atkRange;

    [Header("Enemy Movement Settings")]
    public float moveSpeed;
    public float turnSpeed = 10;

    [Header("Other Enemy Combat Settings")]
    public Transform target;
    public float stopToAtkRange;
    public Transform meleeAtkPoint;
    public LayerMask allyLayer;

    // finding closest target
    float distance;
    float closestDistance = 1000000;

    // attacking
    float nextAttack;

    // references
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            MoveToTarget();
            AttackTarget();
        }
    }

    void FindTarget()
    {
        Allies[] targets = FindObjectsOfType<Allies>();

        for (int i = 0; i < targets.Length; i++)
        {
            distance = Vector3.Distance(transform.position, targets[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = targets[i].transform;
            }
        }
    }

    void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, target.position) > stopToAtkRange)
        {
            agent.destination = target.position;
            agent.speed = moveSpeed;
        }
        else
        {
            agent.speed = 0;
        }
    }

    void AttackTarget()
    {
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + 1 / atkSpeed;

            Collider[] alliesHit = Physics.OverlapSphere(meleeAtkPoint.position, atkRange, allyLayer);
            foreach (Collider a in alliesHit)
            {
                Allies ally = a.GetComponent<Allies>();
                ally.TakeDamage(damage);
            }
;       }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(meleeAtkPoint.position, atkRange);

    }

    void FaceTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void TakeDamage(float damage)
    {
        // hurt vfx
        // hurt sfx

        currentHP -= damage;

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        // death vfx
        // death sfx

        MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.enabled = false;

        Collider coll = GetComponent<Collider>();
        coll.enabled = false;

        Destroy(gameObject, 3);
    }
}
