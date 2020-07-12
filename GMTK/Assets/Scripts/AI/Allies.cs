using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (playerStats == null)
            battleManager.AllyDies();

        gameObject.SetActive(false);
    }

    public void ReturnToMap(bool battleStatus)
    {
        if (battleStatus)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(0);
        // UpdateTowns(townNumberHolder,battleStatus);
    }
}
