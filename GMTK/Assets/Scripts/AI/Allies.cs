using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allies : MonoBehaviour
{
    public float currentHP;

    BattleManager battleManager;

    private void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
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
        battleManager.AllyDies();

        gameObject.SetActive(false);
    }
}
