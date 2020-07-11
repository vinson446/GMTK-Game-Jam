using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Stats")]
    public int gold;
    public int hp;
    public float damage;
    public float atkSpeed;

    [Header("Army Stats")]
    public int currentArmySize;
    public int maxArmySize;

    public int boughtSoldiers;

    [Header("Areas Conquered")]
    public bool[] areasConquered;

    [Header("Farm")]
    public bool[] farmsConquered;
    public int[] levelOfFarms;

    [Header("Barrack")]
    public bool unlockedBarracks = false;
    public int levelOfBarracks;
    public int gainsFromBarracks;

    [Header("TradingPost")]
    public bool unlockedTradingPost = false;
    public int levelOfTradingPost;
    public int gainsFromTradingPosts;

    [Header("Hospital")]
    public bool unlockedHospital = false;
    public int levelOfHospital;
    public int playerHeal;
    public int playerHealCost;
    public int armyHeal;
    public int armyHealCost;

    [Header("Control")]
    public int[] controlInTier1s;
    public int[] controlInTier2s;

    GameManager instance;

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
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateCurrentArmySize()
    {
        int currentSoldiersInAllBarracks = 0;

        Barrack[] barracks = FindObjectsOfType<Barrack>();

        for (int i = 0; i < barracks.Length; i++)
        {
            currentSoldiersInAllBarracks += barracks[i].currentNumOfSoldiers;
        }

        currentArmySize = currentSoldiersInAllBarracks + boughtSoldiers;
    }

    // get max army size from all the farms
    public void CalculateMaxArmySize()
    {
        int currentFoodInAllFarms = 0;

        Farm[] farms = FindObjectsOfType<Farm>();

        for (int i = 0; i < farms.Length; i++)
        {
            currentFoodInAllFarms += farms[i].currentFood;
        }

        maxArmySize = currentFoodInAllFarms;
    }

    public void CalculateGold(int amt)
    {
        gold += amt;

        if (gold < 0)
            gold = 0;
    }
}
