using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [Header("AI Spawn Settings")]
    public GameObject[] ally;
    public GameObject[] enemy;
    public int numAlliesToSpawn;
    public int numEnemiesToSpawn;

    public float allySpawnMinX;
    public float allySpawnMaxX;
    public float allySpawnMinZ;
    public float allySpawnMaxZ;

    public float enemySpawnMinX;
    public float enemySpawnMaxX;
    public float enemySpawnMinZ;
    public float enemySpawnMaxZ;

    [Header("Battle Spawn Settings")]
    public int battleFieldNumber;
    public float minAllyHP;
    public float maxAllyHP;
    public float minEnemyHP;
    public float maxEnemyHP;

    [Header("Battle Settings")]
    public int numAlliesRemaining;
    public int numEnemiesRemaining;

    [Header("Reward Settings")]
    public int rewardGold;
    public int rewardExp;

    [Header("UI Settings")]
    public GameObject battleResultsPanel;
    public GameObject playerUIPanel;
    public TextMeshProUGUI battleResultsText;
    public TextMeshProUGUI loseDescriptionText;
    public TextMeshProUGUI battleRewardsText;
    public TextMeshProUGUI rewardExpText;
    public TextMeshProUGUI rewardGoldText;
    public TextMeshProUGUI allyCasualtiesText;
    public TextMeshProUGUI enemyCasualtiesText;

    public Button continueButtonW;
    public Button continueButtonL;

    [Header("Player UI")]
    public Slider hpBar;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI army;
    PlayerStats playerStats;

    GameManager gameManager;
    public bool skip = true;

    public GameObject farm;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        numAlliesToSpawn = gameManager.battleAllies;
        numEnemiesToSpawn = gameManager.battleEneimes;

        SpawnAI( numAlliesToSpawn, numEnemiesToSpawn);

        numAlliesRemaining = numAlliesToSpawn;
        numEnemiesRemaining = numEnemiesToSpawn;

        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
            UpdatePlayerUI();

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

    }

    public void UpdatePlayerUI()
    {
        hpBar.maxValue = playerStats.maxHP;
        hpBar.value = playerStats.currentHP;

        hpText.text = playerStats.currentHP.ToString() + " / " + playerStats.maxHP.ToString();
        army.text = gameManager.currentArmySize.ToString();
    }

    void SpawnAI(int numAlliesToSpawn, int numEnemiesToSpawn)
    {
        // spawn allies
        for (int i = 0; i < numAlliesToSpawn; i++)
        {
            float x = Random.Range(allySpawnMinX, allySpawnMaxX);
            float z = Random.Range(allySpawnMinZ, allySpawnMaxZ);

            Vector3 randomPos = new Vector3(x, -0.5f, z);

            Instantiate(ally[Random.Range(0,3)], randomPos, Quaternion.identity);
        }

        // spawn enemies
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            float x = Random.Range(enemySpawnMinX, enemySpawnMaxX);
            float z = Random.Range(enemySpawnMinZ, enemySpawnMaxZ);

            Vector3 randomPos = new Vector3(x, -0.5f, z);

            Instantiate(enemy[Random.Range(0,3)], randomPos, Quaternion.identity);
        }
    }

    public void PlayerDies()
    {
        BattleResults(0);
    }

    public void AllyDies()
    {
        numAlliesRemaining -= 1;
        gameManager.currentArmySize -= 1;

        if (gameManager.currentArmySize < 0)
            gameManager.currentArmySize = 0;
    }

    public void EnemyDies()
    {
        numEnemiesRemaining -= 1;

        if (numEnemiesRemaining <= 0)
            BattleResults(2);
    }

    void BattleResults(int outcome)
    {
        battleResultsPanel.SetActive(true);
        playerUIPanel.SetActive(false);
        Cursor.visible = true;

        Animator[] anim = FindObjectsOfType<Animator>();
        foreach (Animator a in anim)
        {
            a.enabled = false;
        }
        NavMeshAgent[] agent = FindObjectsOfType<NavMeshAgent>();
        foreach (NavMeshAgent n in agent)
        {
            n.isStopped = true;
        }

        Ally[] ally = FindObjectsOfType<Ally>();
        foreach (Ally a in ally)
        {
            a.moveSpeed = 0;
        }

        Enemy[] enemy = FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemy)
        {
            e.moveSpeed = 0;
        }

        switch (outcome)
        {
            // lose - player dies
            case 0:
                print("YO");
                battleResultsText.text = "DEFEAT";
                loseDescriptionText.text = "You have died in battle.";
                allyCasualtiesText.text = (numAlliesToSpawn - numAlliesRemaining).ToString();
                enemyCasualtiesText.text = (numEnemiesToSpawn - numEnemiesRemaining).ToString();
                continueButtonL.gameObject.SetActive(true);
                //gameManager.ReturnToMap(0, false);
                break;

            // lose - all allies die
           

            // win - all enemies die
            case 2:
                battleResultsText.text = "VICTORY";
                battleRewardsText.text = "BATTLE   REWARDS";
                rewardGoldText.text = "+  " + rewardGold.ToString() + "   GOLD\n+ 1   FARM";
                allyCasualtiesText.text = (numAlliesToSpawn - numAlliesRemaining).ToString();
                enemyCasualtiesText.text = (numEnemiesToSpawn - numEnemiesRemaining).ToString();

                gameManager.gold += rewardGold;
                gameManager.currentHP = playerStats.currentHP;

                if (!skip)
                {
                    Farm farm = FindObjectOfType<Farm>();
                    farm.UpgradeFarm();
                }
                else
                {
                    farm.SetActive(true);
                    skip = false;
                }

                continueButtonW.gameObject.SetActive(true);
                gameManager.areasConquered[battleFieldNumber] = true;


                break;
        }
    }
}
