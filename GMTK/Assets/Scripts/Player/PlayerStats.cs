using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float currentHP;
    public float maxHP;

    public float damage;

    BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerHP(float cHP, float mHP)
    {
        currentHP = cHP;
        maxHP = mHP;

        battleManager = FindObjectOfType<BattleManager>();
        if (battleManager != null)
            battleManager.UpdatePlayerUI();
    }

    public void UpdatePlayerStats(float d)
    {
        damage = d;
    }
}
