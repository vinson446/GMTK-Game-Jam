using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public bool purchasedBarracks = false;
    public bool unlockedBarracks = false;
    public int levelOfBarracks;
    public int gainsFromBarracks;

    [Header("TradingPost")]
    public bool unlockedTradingPost = false;
    public bool purchasedTradingPost = false;
    public int levelOfTradingPost;
    public int gainsFromTradingPosts;

    [Header("Hospital")]
    public bool unlockedHospital = false;
    public bool purchasedHospital = false;
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

    [Header("Town & Battles")]
    public int battleAllies;
    public int battleEneimes;

    public GameObject[] tier1;
    public int tier1Ammount;
    public int curretTier=1;

    public int townNumberHolder;

    GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        tier1 = GameObject.FindGameObjectsWithTag("Tier1");
        SetTownNumbers();


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
        //just keep this here idk why it shits itself without it
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



    public void StartBattle(int index,int alliesAmnt,int enemiesAmnt, int townNumber)
    {
        
        battleAllies = alliesAmnt;
        battleEneimes = enemiesAmnt;
        townNumberHolder = townNumber;
        SceneManager.LoadScene(index);
    }
    public void ReturnToMap(int index,bool battleStatus)
    {
        SceneManager.LoadScene(index);
        UpdateTowns(townNumberHolder,battleStatus);
    }

    public void SetTownNumbers()
    {
        for (int i = 0; i < tier1.Length; i++)
        {
            tier1[i].GetComponent<Tier1>().number = i;
            
            
        }
    }

    public void UpdateTowns(int townNumber,bool battleStatus)
    {
        tier1[townNumber].GetComponent<Tier1>().UpdateStatus();
        Debug.Log("Town Number " + townNumber + " is named " + tier1[townNumber].GetComponent<Tier1>().name+"has won");
        FindObjectOfType<Map>().ShowMapPanel();
    }

    public void AddTown()
    {
        townNumberHolder++;
        CheckT2Upgrade();
    }

    void CheckT2Upgrade()
    {
        if (townNumberHolder == 3)
        {
            unlockedBarracks = true;
            unlockedHospital = true;
            unlockedTradingPost = true;
            curretTier = 2;
        }
    }


}
