﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject shopPanel;
    public GameObject upgradeFarmPanel;

    GameManager gameManager;
    Upgrades upgrades;

    // Start is called before the first frame update
    void Start()
    {
        mapPanel.SetActive(true);
        upgradeFarmPanel.SetActive(false);
        shopPanel.SetActive(false);

        gameManager = FindObjectOfType<GameManager>();
        upgrades = FindObjectOfType<Upgrades>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ShowShopPanel()
    {
        mapPanel.SetActive(false);
        upgradeFarmPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void ShowMapPanel()
    {
        mapPanel.SetActive(true);
        upgradeFarmPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void ShowFarmUpgradePanel()
    {
        mapPanel.SetActive(false);
        upgradeFarmPanel.SetActive(true);
        shopPanel.SetActive(false);

        for (int i = 0; i < gameManager.farmsConquered.Length; i++)
        {
            if (!gameManager.farmsConquered[i])
            {
                upgrades.conqueredFarms[i].SetActive(false);
            }
            else
            {
                upgrades.conqueredFarms[i].SetActive(true);
            }
        }
    }
}
