using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allies : MonoBehaviour
{
    public float currentHP;
    public float maxHP;

    BattleManager battleManager;
    PlayerStats playerStats;

    private void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();

        playerStats = GetComponent<PlayerStats>();
        if (playerStats != null)
            playerStats.UpdatePlayerHP(currentHP, maxHP);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("oof");
        // hurt vfx
        // hurt sfx

        currentHP -= damage;

        if (playerStats != null)
            playerStats.UpdatePlayerHP(currentHP, maxHP);

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
      

        gameObject.SetActive(false);
    }
}
