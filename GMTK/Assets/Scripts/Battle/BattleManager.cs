using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [Header("AI Spawn Settings")]
    public GameObject ally;
    public GameObject enemy;
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
    public TextMeshProUGUI battleResultsText;
    public TextMeshProUGUI loseDescriptionText;
    public TextMeshProUGUI battleRewardsText;
    public TextMeshProUGUI rewardExpText;
    public TextMeshProUGUI rewardGoldText;
    public TextMeshProUGUI allyCasualtiesText;
    public TextMeshProUGUI enemyCasualtiesText;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        battleResultsPanel.SetActive(false);
        SpawnAI();

        numAlliesRemaining = numAlliesToSpawn;
        numEnemiesRemaining = numEnemiesToSpawn;

        gameManager = FindObjectOfType<GameManager>();
    }

    void SpawnAI()
    {
        // spawn allies
        for (int i = 0; i < numAlliesToSpawn; i++)
        {
            float x = Random.Range(allySpawnMinX, allySpawnMaxX);
            float z = Random.Range(allySpawnMinZ, allySpawnMaxZ);

            Vector3 randomPos = new Vector3(x, 0, z);

            Instantiate(ally, randomPos, Quaternion.identity);
        }

        // spawn enemies
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            float x = Random.Range(enemySpawnMinX, enemySpawnMaxX);
            float z = Random.Range(enemySpawnMinZ, enemySpawnMaxZ);

            Vector3 randomPos = new Vector3(x, 0, z);

            Instantiate(enemy, randomPos, Quaternion.identity);
        }
    }

    public void PlayerDies()
    {
        BattleResults(0);
    }

    public void AllyDies()
    {
        numAlliesRemaining -= 1;

        if (numAlliesRemaining <= 0)
            BattleResults(1);
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

        switch (outcome)
        {
            // lose - player dies
            case 0:
                battleResultsText.text = "DEFEAT";
                loseDescriptionText.text = "You have died in battle.";
                allyCasualtiesText.text = (numAlliesToSpawn - numAlliesRemaining).ToString();
                enemyCasualtiesText.text = (numEnemiesToSpawn - numEnemiesRemaining).ToString();

                break;

            // lose - all allies die
            case 1:
                battleResultsText.text = "DEFEAT";
                loseDescriptionText.text = "All your men have died in battle.";
                allyCasualtiesText.text = (numAlliesToSpawn - numAlliesRemaining).ToString();
                enemyCasualtiesText.text = (numEnemiesToSpawn - numEnemiesRemaining).ToString();

                break;

            // win - all enemies die
            case 2:
                battleResultsText.text = "VICTORY";
                battleRewardsText.text = "BATTLE REWARDS";
                rewardExpText.text = "+" + rewardExp.ToString() + " EXP";
                rewardGoldText.text = "+" + rewardGold.ToString() + " GOLD";
                allyCasualtiesText.text = (numAlliesToSpawn - numAlliesRemaining).ToString();
                enemyCasualtiesText.text = (numEnemiesToSpawn - numEnemiesRemaining).ToString();

                gameManager.areasConquered[battleFieldNumber] = true;

                break;
        }
    }
}
