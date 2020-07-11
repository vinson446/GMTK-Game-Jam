using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    [Header("Level")]
    public int currentLevelOfHospital;
    public int maxLevelOfHospital;

    [Header("Current Setting")]
    public int playerHeal;
    public int playerCost;

    [Header("Upgrade Settings")]
    public int upgradeAmount;
    public int upgradeCost;

    GameManager gameManager;

    Hospital instance;
     
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealPlayer()
    {
        if (gameManager.gold >= playerCost)
        {
            Allies player = FindObjectOfType<Allies>();
            player.TakeDamage(-playerHeal);

            gameManager.gold -= playerCost;
        }
    }

    public void UpgradeHospital()
    {
        if (gameManager.gold >= upgradeCost && currentLevelOfHospital < maxLevelOfHospital)
        {
            currentLevelOfHospital += 1;
            playerHeal += upgradeAmount;

            gameManager.gold -= upgradeCost;
        }
    }
}
