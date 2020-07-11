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

    [Header("Food")]
   public float totalFoodAmmount;
    float totalFoodProcuction;
    float currentFoodProduciton;
   public float currentFoodCapacity;



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

    private void OnEnable()
    {
        InvokeRepeating("CalculateCurrentArmySize", 0f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(currentArmySize, 0f, Mathf.Infinity);
        Mathf.Clamp(totalFoodAmmount, 0f, Mathf.Infinity);

    }



    public void CalcualateTotalFoodCapacity()
    {

        currentFoodCapacity = 0;
        Farm[] farms = FindObjectsOfType<Farm>();

        for (int i = 0; i < farms.Length; i++)
        {
            currentFoodCapacity += farms[i].maxFood;
        }

    }
    public void CalcualateTotalFoodProduction()
    {
        currentFoodProduciton = 0;
        Farm[] farms = FindObjectsOfType<Farm>();

        for (int i = 0; i < farms.Length; i++)
        {
            currentFoodProduciton += farms[i].foodGainOverTime;
        }

    }


    public void CalculateCurrentArmySize()
    {

        int currentSoldiersInAllBarracks = 0;

        Barrack[] barracks = FindObjectsOfType<Barrack>();
        for (int i = 0; i < barracks.Length; i++)
        {
            currentSoldiersInAllBarracks += barracks[i].soldierGainOverTime;
        }

        currentArmySize += currentSoldiersInAllBarracks;
        FoodCalculation();
        if (totalFoodAmmount < currentArmySize)
            currentArmySize--;
        
    }

    // get max army size from all the farms
    public void FoodCalculation()
    {
        CalcualateTotalFoodCapacity();
        CalcualateTotalFoodProduction();

        totalFoodProcuction = currentFoodProduciton- currentArmySize;
        
        
        totalFoodAmmount += totalFoodProcuction;
        if (totalFoodAmmount < 0)
            totalFoodAmmount = 0;

    }

    public void CalculateGold(int amt)
    {
        gold += amt;

        if (gold < 0)
            gold = 0;
    }
}
