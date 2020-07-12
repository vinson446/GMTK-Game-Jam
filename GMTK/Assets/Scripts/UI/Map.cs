 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject shopPanel;
    public GameObject upgradeFarmPanel;

    GameManager gameManager;
    Upgrades upgrades;

    [Header("Stats For Each Battleground")]
    public GameObject UISHIT;
    public TextMeshProUGUI townName;
    public TextMeshProUGUI powerLevel;
    public TextMeshProUGUI difficulty;

    public Button[] buttonForVillages;

    public int ip;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        upgrades = FindObjectOfType<Upgrades>();
        upgradeFarmPanel.SetActive(false);
        shopPanel.SetActive(false);
        ShowMapPanel();

        Cursor.visible = true;

        for (int i = 0; i < gameManager.areasConquered.Length; i++)
        {
            if (gameManager.areasConquered[i])
            {
                buttonForVillages[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ShowShopPanel()
    {
        // HideMapPanel();
        upgradeFarmPanel.SetActive(false);
        shopPanel.SetActive(true);
    }




    public void ShowFarmUpgradePanel()
    {
        //HideMapPanel();
        //print("show");
        upgradeFarmPanel.SetActive(true);
        shopPanel.SetActive(false);

        /*
        for (int i = 0; i < gameManager.tier1.Length; i++)
        {
            if (!gameManager.tier1[i].GetComponent<Tier1>().owned)
            {
                print(i);
                gameManager.tier1[i].GetComponent<Tier1>().UpgradeUI(false);
            }
            else
            {
                gameManager.tier1[i].GetComponent<Tier1>().UpgradeUI(true);
            }
        }
        */
    }
    public void HideFarmUpgradePanel()
    {

        upgradeFarmPanel.SetActive(false);
        shopPanel.SetActive(true);

        /*
        for (int i = 0; i < gameManager.tier1.Length; i++)
        {
             gameManager.tier1[i].GetComponent<Tier1>().UpgradeUI(false);
          
        }
        */
    }
    public void ShowMapPanel()
    {
        mapPanel.SetActive(true);
        upgradeFarmPanel.SetActive(false);
        shopPanel.SetActive(false);

        /*
       for (int i = 0; i < gameManager.tier1.Length; i++)
        {
            if (!gameManager.tier1[i].GetComponent<Tier1>().owned)
            {
                print(i);
                gameManager.tier1[i].GetComponent<Tier1>().AttackUI(true);
            }
            else
            {
                gameManager.tier1[i].GetComponent<Tier1>().AttackUI(false);
            }
        }
        */
    }
    /*
    public void HideMapPanel()
    {
        mapPanel.SetActive(false);

        /*
        for (int i = 0; i < gameManager.tier1.Length; i++)
        {
            gameManager.tier1[i].GetComponent<Tier1>().AttackUI(false);

        }
        
    }
    */

    public void ShowVillageStats(int index)
    {
        UISHIT.SetActive(true);
        ip = index;

        switch (index)
        {
            case 0:
                townName.text = "ARAMOOR";
                powerLevel.text = "5";
                difficulty.text = "DIFFICULTY : EASY";
     
                break;
            case 1:
                townName.text = "WOODPINE";
                powerLevel.text = "30";
                difficulty.text = "DIFFICULTY : EASY";
                break;
            case 2:
                townName.text = "PENKETH";
                powerLevel.text = "40";
                difficulty.text = "DIFFICULTY : EASY";
                break;
            case 3:
                townName.text = "GILLAMOOR";
                powerLevel.text = "50";
                difficulty.text = "DIFFICULTY : EASY";
                break;
            case 4:
                townName.text = "DRAYCOTT";
                powerLevel.text = "40";
                difficulty.text = "DIFFICULTY : EASY";
                break;
            case 5:
                townName.text = "THRELKELD";
                powerLevel.text = "50";
                difficulty.text = "DIFFICULTY : EASY";
                break;
            case 6:
                townName.text = "WARTHFORD";
                powerLevel.text = "35";
                difficulty.text = "DIFFICULTY : EASY";
                break;
            case 7:
                townName.text = "EASTHALLOW";
                powerLevel.text = "40";
                difficulty.text = "DIFFICULTY : EASY";
                break;
            case 8:
                townName.text = "COLCHESTER";
                powerLevel.text = "45";
                difficulty.text = "DIFFICULTY : EASY";
                break;


            case 9:
                townName.text = "BEXLEY";
                powerLevel.text = "80";
                difficulty.text = "DIFFICULTY : MEDIUM";
                break;
            case 10:
                townName.text = "AEREDALE";
                powerLevel.text = "100";
                difficulty.text = "DIFFICULTY : MEDIUM";
                break;
            case 11:
                townName.text = "EDINBURGH";
                powerLevel.text = "90";
                difficulty.text = "DIFFICULTY : MEDIUM";
                break;


            case 12:
                townName.text = "KING'S   WATCH";
                powerLevel.text = "200";
                difficulty.text = "DIFFICULTY : HARD";
                break;
        }
    }
    public void TransferBattleShitToGameManager()
    {
        gameManager.battleAllies = gameManager.currentArmySize;

        switch (ip)
        {
            case 0:
                gameManager.battleEneimes = 5;
                SceneManager.LoadScene(3);
                break;
            case 1:
                gameManager.battleEneimes = 30;
                SceneManager.LoadScene(4);
                break;
            case 2:
                gameManager.battleEneimes = 40;
                SceneManager.LoadScene(5);
                break;
            case 3:
                gameManager.battleEneimes = 50;
                SceneManager.LoadScene(6);
                break;
            case 4:
                gameManager.battleEneimes = 40;
                SceneManager.LoadScene(7);
                break;
            case 5:
                gameManager.battleEneimes = 50;
                SceneManager.LoadScene(8);
                break;
            case 6:
                gameManager.battleEneimes = 35;
                SceneManager.LoadScene(9);
                break;
            case 7:
                gameManager.battleEneimes = 40;
                SceneManager.LoadScene(10);
                break;
            case 8:
                gameManager.battleEneimes = 45;
                SceneManager.LoadScene(11);
                break;


            case 9:
                gameManager.battleEneimes = 80;
                SceneManager.LoadScene(12);
                break;
            case 10:
                gameManager.battleEneimes = 100;
                SceneManager.LoadScene(13);
                break;
            case 11:
                gameManager.battleEneimes = 90;
                SceneManager.LoadScene(14);
                break;


            case 12:
                gameManager.battleEneimes = 200;
                SceneManager.LoadScene(15);
                break;
        }
    }
}
