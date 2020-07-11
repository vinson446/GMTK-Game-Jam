using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ally : MonoBehaviour
{
    [Header("Ally Stats")]
    public float damage;
    public float atkSpeed;
    public float atkRange;

    [Header("Ally Movement Settings")]
    public float moveSpeed;
    public float turnSpeed = 10;

    [Header("Other Ally Combat Settings")]
    public Transform target;
    public float stopToAtkRange;
    public Transform meleeAtkPoint;
    public LayerMask enemyLayer;

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
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        AttackTarget();
    }

    void FindTarget()
    {
        Enemy[] targets = FindObjectsOfType<Enemy>();

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

            Collider[] enemiesHit = Physics.OverlapSphere(meleeAtkPoint.position, atkRange, enemyLayer);
            foreach (Collider e in enemiesHit)
            {
                Enemy enemy = e.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }

            closestDistance = 1000000;
            FindTarget();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(meleeAtkPoint.position, atkRange);

    }

    void FaceTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
