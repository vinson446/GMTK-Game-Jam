using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier1 : MonoBehaviour
{

    //public string name = "Null";
    public int number;

    public float control = 10;
    public bool owned = false;

    public int maxEnemies;
    public int MineEnemies;
    int allyAmnt;
    int enemyAmnt;

    public GameObject farm;
    public GameObject upgradeCanvas;
    public GameObject AttackCanvas;


    GameManager gameManager;
    Tier1 instance;

    void Start()
    {
        enemyAmnt = Random.Range(MineEnemies, maxEnemies);
      
        gameManager = FindObjectOfType<GameManager>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public void StartBattle(int index)
    {
        Debug.Log("Started Fight");
        gameManager.StartBattle(index, allyAmnt, enemyAmnt, number);
    }
    */

    public void UpgradeUI(bool activate)
    {
        upgradeCanvas.SetActive(activate);
    }

    /*
    public void AttackUI(bool activate)
    {
        AttackCanvas.SetActive(activate);
    }

    
    public void UpdateStatus()
    {
        owned = true;
        farm.SetActive(true);
        gameManager.AddTown();
    }
    */





}
