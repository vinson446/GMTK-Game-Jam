﻿using System.Collections;
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
    public Button upgradeBarracksButton;
    public Button upgradeTradingPostButton;
    public Button upgradeHospitalButton;

    [Header("Upgrade Texts")]
    public TextMeshProUGUI hospitalChangeText;
    public TextMeshProUGUI hospitalDescriptionChangeText;

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
        if (gameManager.currentArmySize >= 0)
            currentArmySizeText.text = gameManager.currentArmySize.ToString();
        //Debug.Log(gameManager.currentArmySize);
        if (gameManager.totalFoodAmmount >= 0)
            maxArmySizeText.text = gameManager.totalFoodAmmount.ToString();
    }

    public void PurchaseSoldier()
    {
        if (gameManager.gold >= buySoldierCost&& gameManager.currentFoodCapacity !=0)
        {
            gameManager.currentArmySize += 1;
            gameManager.gold -= buySoldierCost;

            
        }
    }

    public void GetFarm()
    {
        farm.SetActive(true);
    }

    public void PurchaseBarracks()
    {
        if (!gameManager.purchasedBarracks && gameManager.gold >= buyBarracksCost && gameManager.unlockedBarracks)
        {
            //Instantiate(barrack, Vector3.zero, Quaternion.identity);
            barrack.SetActive(true);

            gameManager.purchasedBarracks = true;

            buyBarracksButton.interactable = false;

            gameManager.gold -= buyBarracksCost;
        }
    }

    public void PurchaseTradingPost()
    {
        if (!gameManager.purchasedTradingPost && gameManager.gold >= buyTradingPostCost&& gameManager.unlockedTradingPost)
        {
            //Instantiate(tradingPost, Vector3.zero, Quaternion.identity);
            tradingPost.SetActive(true);

            gameManager.purchasedTradingPost = true;

            buyTradingPostButton.interactable = false;

            gameManager.gold -= buyTradingPostCost;
        }
    }

    public void PurchaseHospital()
    {
        if (!gameManager.purchasedHospital && gameManager.gold >= buyHospitalCost && gameManager.unlockedTradingPost)
        {
            // Instantiate(hospital, Vector3.zero, Quaternion.identity);
            hospital.SetActive(true);

            gameManager.purchasedHospital = true;

            h = FindObjectOfType<Hospital>();
            hospitalChangeText.text = "BUY HEAL";
            hospitalDescriptionChangeText.text = "+" + h.playerHeal.ToString() + " Player HP" + " (Current HP: " + gameManager.currentHP.ToString() + ")";

            gameManager.gold -= buyHospitalCost;
        }
        else if (gameManager.purchasedHospital && gameManager.gold >= buyHospitalCost)
        {
            h = FindObjectOfType<Hospital>();

            gameManager.currentHP += h.playerHeal;

            if (gameManager.currentHP > gameManager.maxHP)
                gameManager.currentHP = gameManager.maxHP;
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
            farm.GetComponent<Farm>().UpgradeFarm();
        }

        Map map = FindObjectOfType<Map>();
        map.ShowShopPanel();
    }

    public void UpgradeBarracks()
    {
        if (gameManager.purchasedBarracks)
        {
            b = FindObjectOfType<Barrack>();

            if (b.levelOfBarrack >= 2)
            {
                b.UpgradeBarracks();
                upgradeBarracksButton.interactable = false;
            }
            else
            {
                b.UpgradeBarracks();
            }
        }
    }

    public void UpgradeTradingPost()
    {
        if (gameManager.purchasedTradingPost)
        {
            t = FindObjectOfType<TradingPost>();

            if (t.levelOfTradingPost >= 2)
            {
                t.UpgradeTradingPost();
                upgradeTradingPostButton.interactable = false;
            }
            else
            {
                t.UpgradeTradingPost();
            }
        }
    }

    public void UpgradeHospital()
    {
        if (gameManager.purchasedHospital)
        {
            h = FindObjectOfType<Hospital>();

            if (h.currentLevelOfHospital >= 2)
            {
                h.UpgradeHospital();
                upgradeHospitalButton.interactable = false;
            }
            else
            {
                h.UpgradeHospital();
            }
        }
    }
}
