using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [Header("Level")]
    public int levelOfFarm = 0;
    public int maxLevelOfFarm = 3;

    [Header("Current Settings")]
    public int currentFood;
    public int maxFood;
    public int foodGainOverTime;
    public int timeIncrement;

    [Header("Upgrade Settings")]
    public int upgradeMaxArmyAmount = 50;
    public int foodGainUpgradeAmount = 2;
    public int upgradeCost;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        StartCoroutine(GainFoodOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpgradeFarm();
        }
    }

    public void UpgradeFarm()
    {
        if (gameManager.gold >= upgradeCost && levelOfFarm < maxLevelOfFarm)
        {
            levelOfFarm += 1;
            foodGainOverTime += foodGainUpgradeAmount;
            maxFood += upgradeMaxArmyAmount;

            gameManager.CalculateGold(-upgradeCost);
        }
    }

    IEnumerator GainFoodOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeIncrement);

            if (currentFood >= maxFood)
            {
                currentFood = maxFood;
            }
            else
            {
                currentFood += foodGainOverTime;
            }

            gameManager.CalculateMaxArmySize();
        }
    }
}
