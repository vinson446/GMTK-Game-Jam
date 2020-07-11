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
    public GameObject[] farm;
    public GameObject tradingPost;
    public GameObject hospital;

    [Header("Purchase Buttons")]
    public Button buySoldierButton;
    public Button buyBarracksButton;
    public Button buyTradingPostButton;
    public Button buyHospitalButton;

    [Header("Upgrade Buttons")]
    public Button[] upgradeFarmButton;
    public Button upgradeBarracksButton;
    public Button upgradeTradingPostButton;
    public Button upgradeHospitalButton;

    [Header("Upgrade Texts")]
    public TextMeshProUGUI farmUpgradeText;
    public TextMeshProUGUI barracksUpgradeText;
    public TextMeshProUGUI tradingPostUpgradeText;
    public TextMeshProUGUI hospitalUpgradeText;

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
        if (gameManager.levelOfFarms[index] >= 2)
        {
            upgradeFarmButton[index].interactable = false;
        }
        else
        {
            gameManager.levelOfFarms[index] += 1;
            farm[index].GetComponent<Farm>().UpgradeFarm();
        }

        Map map = FindObjectOfType<Map>();
        map.ShowShopPanel();
    }

    public void UpgradeBarracks()
    {
        b = FindObjectOfType<Barrack>();

        if (b.levelOfBarrack >= 2)
        {
            upgradeBarracksButton.interactable = false;
        }
        else
        {
            b.UpgradeBarracks();
        }
    }

    public void UpgradeTradingPost()
    {
        t = FindObjectOfType<TradingPost>();

        if (t.levelOfTradingPost >= 2)
        {
            upgradeTradingPostButton.interactable = false;
        }
        else
        {
            t.UpgradeTradingPost();
        }
    }

    public void UpgradeHospital()
    {
        upgradeHospitalButton.interactable = false;
    }
}
