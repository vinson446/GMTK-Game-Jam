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
    public float selectedWeapon;


    [Header("Ally Movement Settings")]
    public float moveSpeed;
    public float turnSpeed = 10;

    [Header("Other Ally Combat Settings")]
    public Transform target;
    public float stopToAtkRange;
    public LayerMask enemyLayer;
    public GameObject weapon;
    //public GameObject weaponHolder;

    // finding closest target
    float distance;
    float closestDistance = Mathf.Infinity;
    public bool isAlly = true;

    // attacking
    float nextAttack;

    // references
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
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
        if (i >= 5)
        {
            selectedWeapon = 1f;
            transform.Find("Sword").gameObject.SetActive(true);
            stopToAtkRange = 5;
            atkSpeed = 2f;
            damage = 4;
        }
        else
        {
            selectedWeapon = 2f;
            transform.Find("Spear").gameObject.SetActive(true);
            stopToAtkRange = 6;
            atkSpeed = 4f;
            damage = 1;
        }
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
            AttackTarget();
        }
    }

    void AttackTarget()
    {
        if(gameObject.activeSelf)
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + 1 / atkSpeed;
                switch (selectedWeapon)
                {
                    case 1:
                        AiSword swordStats = GetComponentInChildren<AiSword>();
                        swordStats.Attack(damage, isAlly);
                        break;
                    case 2:
                        AiSpear weaponStats = GetComponentInChildren<AiSpear>();
                        weaponStats.Attack(damage, isAlly);
                        break;
                }
                closestDistance = 1000000;
                FindTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
