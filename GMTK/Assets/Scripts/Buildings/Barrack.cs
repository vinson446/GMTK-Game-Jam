using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    [Header("Level")]
    public int levelOfBarrack;
    public int maxLevelOfBarrack;

    [Header("Current Settings")]
    public int currentNumOfSoldiers;
    public int soldierGainOverTime;
    public int timeIncrement;

    [Header("Upgrade Settings")]
    public int soldierGainUpgradeAmount = 2;
    public int upgradeCost;

    GameManager gameManager;

    Barrack instance;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

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

    private void OnEnable()
    {
        StartCoroutine(GainMenOverTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeBarracks()
    {
        if (gameManager.gold >= upgradeCost && levelOfBarrack < maxLevelOfBarrack)
        {
            levelOfBarrack += 1;
            soldierGainOverTime += soldierGainUpgradeAmount;

            gameManager.CalculateGold(-upgradeCost);
        }
    }

    IEnumerator GainMenOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeIncrement);

            if (currentNumOfSoldiers >= gameManager.maxArmySize)
            {
                currentNumOfSoldiers = gameManager.maxArmySize;
            }
            else
            {
                currentNumOfSoldiers += soldierGainOverTime;
            }

            gameManager.CalculateCurrentArmySize();
        }
    }
}
