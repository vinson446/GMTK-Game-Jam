using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPost : MonoBehaviour
{
    [Header("Level")]
    public int levelOfTradingPost;
    public int maxLevelOfTradingPost;

    [Header("Current Settings")]
    public int goldGainOverTime;
    public int timeIncrement;

    [Header("Upgrade Settings")]
    public int upgradeGoldAmount = 2;
    public int upgradeCost;

    GameManager gameManager;

    static TradingPost instance;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(GainGoldOverTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeTradingPost()
    {
        if (gameManager.gold >= upgradeCost && levelOfTradingPost < maxLevelOfTradingPost)
        {
            levelOfTradingPost += 1;
            goldGainOverTime += upgradeGoldAmount;

            gameManager.CalculateGold(-upgradeCost);
        }
    }

    IEnumerator GainGoldOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeIncrement);

            gameManager.CalculateGold(goldGainOverTime);
        }
    }
}
