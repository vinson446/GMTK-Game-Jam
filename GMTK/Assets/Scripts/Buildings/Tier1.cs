using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier1 : MonoBehaviour
{

    //public string name = "Null";
    public int number;

    public float control = 10;
    public bool owned = false;
   
    int allyAmnt;
    int enemyAmnt = 10;

    public GameObject farm;
    public GameObject canvas;


    GameManager gameManager;
    Tier1 instance;

    void Start()
    {
      
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

    public void StartBattle(int index)
    {
        gameManager.StartBattle(index, allyAmnt, enemyAmnt, number);
    }
    
    public void UpgradeUI(bool activate)
    {
        canvas.SetActive(activate);

    }

    public void UpdateStatus()
    {
        owned = true;
        farm.SetActive(true);
        gameManager.AddTown();
    }





}
