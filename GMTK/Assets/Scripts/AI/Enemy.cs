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
    public float selectedWeapon;
    [Header("Enemy Movement Settings")]
    public float moveSpeed;
    public float turnSpeed = 10;

    [Header("Other Enemy Combat Settings")]
    public Transform target;
    public float stopToAtkRange;
    public Transform meleeAtkPoint;
    public LayerMask allyLayer;
    bool theFloatOfShame = false;

    // finding closest target
    float distance;
    float closestDistance = 1000000;

    // attacking
    float nextAttack;

    // references
    NavMeshAgent agent;
    BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
        currentHP = maxHP;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        battleManager = FindObjectOfType<BattleManager>();

        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        FaceTarget();
       
    }
    void SelectWeapon()
    {
        int i = Random.Range(1, 10);
        if (i >= 7)
        {
            selectedWeapon = 1f;
            transform.Find("Sword").gameObject.SetActive(true);
        }
        else
        {
            selectedWeapon = 2f;
            transform.Find("Spear").gameObject.SetActive(true);
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
            AttackTarget();
            agent.speed = 0;
        }  
    }

    void AttackTarget()
    {
        //sword is 1
        //spear is 2
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + 1 / atkSpeed;
            switch (selectedWeapon)
            {
                case 1:
                    AiSword swordStats = GetComponentInChildren<AiSword>();
                    swordStats.Attack(meleeAtkPoint, atkRange, damage, theFloatOfShame);
                    break;
                case 2:
                    AiSpear weaponStats = GetComponentInChildren<AiSpear>();
                    weaponStats.Attack(meleeAtkPoint, atkRange, damage, theFloatOfShame);
                    break;

            }

            closestDistance = 1000000;
            FindTarget();
        }
    }

    private void OnDrawGizmosSelected()
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
        battleManager.EnemyDies();

        gameObject.SetActive(false);
    }
}
