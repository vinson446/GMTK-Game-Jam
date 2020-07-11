using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI currentArmySizeText;
    public TextMeshProUGUI maxArmySizeText;

    [Header("Shop Costs")]
    public int buySoldierCost;
    public int buyBarracksCost;
    public int buyTradingPostCost;
    public int buyHospitalCost;
    public int upgradeFarmCost;
    public int upgradeBarracksCost;
    public int upgradeTradingPostCost;
    public int upgradeHospitalCost;

    [Header("Buildings")]
    public GameObject barrack;
    public GameObject farm;
    public GameObject tradingPost;
    public GameObject hospital;

    [Header("Purchase Buttons")]
    public Button buySoldierButton;
    public Button buyBarracksButton;
    public Button buyTradingPostButton;
    public Button buyHospitalButton;

    [Header("Upgrade Buttons")]
    public Button[] upgradeFarmButton;
    public int upgradeFarmButtonIndex;
    public Button upgradeBarracksButton;
    public Button upgradeTradingPostButton;
    public Button upgradeHospitalButton;

    [Header("Potential Farm Upgrades")]
    public GameObject[] conqueredFarms;

    Barrack b;
    Farm f;
    TradingPost t;
    Hospital h;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerStuffDisplay();
    }

    public void UpdatePlayerStuffDisplay()
    {
        goldText.text = "$" + gameManager.gold.ToString();
        currentArmySizeText.text = gameManager.currentArmySize.ToString();
        maxArmySizeText.text = gameManager.maxArmySize.ToString();
    }

    public void PurchaseSoldier()
    {
        if (gameManager.gold >= buySoldierCost)
        {
            gameManager.boughtSoldiers += 1;
            gameManager.gold -= buySoldierCost;
        }
    }

    public void PurchaseBarracks()
    {
        if (!gameManager.unlockedBarracks && gameManager.gold >= buyBarracksCost)
        {
            //Instantiate(barrack, Vector3.zero, Quaternion.identity);
            barrack.SetActive(true);

            gameManager.unlockedBarracks = true;

            buyBarracksButton.interactable = false;

            gameManager.gold -= buyBarracksCost;
        }
    }

    public void PurchaseTradingPost()
    {
        if (!gameManager.unlockedTradingPost && gameManager.gold >= buyTradingPostCost)
        {
            //Instantiate(tradingPost, Vector3.zero, Quaternion.identity);
            tradingPost.SetActive(true);

            gameManager.unlockedTradingPost = true;

            buyTradingPostButton.interactable = false;

            gameManager.gold -= buyTradingPostCost;
        }
    }

    public void PurchaseHospital()
    {
        if (!gameManager.unlockedHospital && gameManager.gold >= buyHospitalCost)
        {
            // Instantiate(hospital, Vector3.zero, Quaternion.identity);
            hospital.SetActive(true);

            gameManager.unlockedHospital = true;

            buyHospitalButton.interactable = false;

            gameManager.gold -= buyHospitalCost;
        }
    }

    public void UpgradeFarm(int index)
    {
        upgradeFarmButton[upgradeFarmButtonIndex].interactable = false;
    }

    public void UpgradeBarracks()
    {
        b = FindObjectOfType<Barrack>();
        b.UpgradeBarracks();

        upgradeBarracksButton.interactable = false;
    }

    public void UpgradeTradingPost()
    {
        t = FindObjectOfType<TradingPost>();
        t.UpgradeTradingPost();

        upgradeTradingPostButton.interactable = false;
    }

    public void UpgradeHospital()
    {
        upgradeHospitalButton.interactable = false;
    }
}
