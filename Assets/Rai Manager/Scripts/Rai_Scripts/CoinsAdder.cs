using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinsAdder : MonoBehaviour
{
    public Text coins;
    public Text twocoins;
    public int totalCoins;
    public int addCoins;
    public int doItrations = 10;
    public bool addNow;
    public bool resetNow;
    public GameObject coinsAnim;
    // Start is called before the first frame update

    void Start()
    {
        if (GameManager.Instance.Initialized == false)
        {
            GameManager.Instance.Initialized = true;
            Rai_SaveLoad.LoadProgress();
        }
        if(coins) coins.text = SaveData.Instance.Coins.ToString();
        if(twocoins) twocoins.text = SaveData.Instance.Coins.ToString();
    }
    IEnumerator CoinsAddition()
    {
        int modValue = addCoins % doItrations;
        int perValue = (addCoins) / doItrations;
        int loopValue = 0;
        if(perValue == 0)
        {
            modValue = 0;
            perValue = 1;
            loopValue = addCoins;
        }
        else
        {
            loopValue = doItrations;
        }
        if (AudioManager.Instance)
        {
            AudioManager.Instance.CoinSfx.Play();
        }

        for (int i = 0; i < loopValue ; i++)
        {
            totalCoins += perValue;
            SaveData.Instance.Coins += perValue;
            if (coins)
            {
                coins.text = totalCoins.ToString();

            }
            if (twocoins) twocoins.text = SaveData.Instance.Coins.ToString();
            yield return new WaitForSecondsRealtime(0.1f);
        }
        totalCoins += modValue;
        if (AudioManager.Instance)
        {
            AudioManager.Instance.CoinSfx.Stop();
        }
        Rai_SaveLoad.SaveProgress();
        if (coinsAnim)
        {
            coinsAnim.SetActive(false);
        }
    }
    private void ResetVlaues()
    {
        totalCoins = addCoins = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (addNow)
        {
            totalCoins = SaveData.Instance.Coins;
            addNow = !addNow;
            StartCoroutine(CoinsAddition());
            if (coinsAnim)
            {
                coinsAnim.SetActive(true);
            }
        }
        if (resetNow)
        {
            resetNow = !resetNow;
            ResetVlaues();
        }
    }
}
