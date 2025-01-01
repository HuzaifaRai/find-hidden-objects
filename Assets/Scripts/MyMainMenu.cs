using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyMainMenu : MonoBehaviour
{
    [Header("Profile")]
    public GameObject AdPenl;
    public GameObject LoadingPanel;
    public GameObject needMoreCoins;
    public GameObject GetRewardedCoinsPanel;
    public GameObject videoNotAvalible;
    public GameObject SettingPanel;
    public GameObject RewardedPanel;
    public GameObject RewardPanel;
    public GameObject NeedMoreCoinsPanel;
    public Text TotalCoins;
    public CoinsAdder coinsAdder;
    [Header("Loading")]
    public Image fillBar;
    public Image fillImage;
    [Range(4, 10)]
    public float TimeToLoad;
    public Slider BGMSlider;
    public Slider VolumeSlider;
    float volumevalue;
    float bgmvalue;
    public void Start()
    {
        if (GAManager.Instance) GAManager.Instance.LogDesignEvent("Scene:" + SceneManager.GetActiveScene().name + SceneManager.GetActiveScene().buildIndex);
        if (GameManager.Instance.Initialized == false)
        {
            GameManager.Instance.Initialized = true;
            Rai_SaveLoad.LoadProgress();
        }
    }
    #region EnableDisable
    void OnEnable()
    {
        if (MyAdsManager.Instance != null)
        {
            MyAdsManager.Instance.onRewardedVideoAdCompletedEvent += OnRewardedVideoComplete;
        }
    }

    void OnDisable()
    {
        if (MyAdsManager.Instance != null)
        {
            MyAdsManager.Instance.onRewardedVideoAdCompletedEvent -= OnRewardedVideoComplete;
        }
    }
    #endregion
    public void Save()
    {
        if (AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
        SaveData.Instance.ProfileCreated = true;

        Rai_SaveLoad.SaveProgress();
    }


    public void SettingIsTrue(bool IsTrue)
    {
        if(IsTrue == true)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopinSfx.Play();
        }
        else if(IsTrue == false)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopOutSfx.Play();
        }
        SettingPanel.SetActive(IsTrue);
    }
    public void RewardedPanelIstrue(bool IsTrue)
    {
        if(IsTrue == true)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopinSfx.Play();
        }
        else if(IsTrue == false)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopOutSfx.Play();
        }
        RewardedPanel.SetActive(IsTrue);
    }
    public void NeedMoreCoins(bool IsTrue)
    {
        if(IsTrue == true)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopinSfx.Play();
        }
        else if(IsTrue == false)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopOutSfx.Play();
        }
        NeedMoreCoinsPanel.SetActive(IsTrue);
    }
    public void Volume()
    {
        volumevalue = VolumeSlider.value;
        AudioManager.Instance.Sound(volumevalue);
    }
    public void BGM()
    {
        print(bgmvalue);
        bgmvalue = BGMSlider.value;
        AudioManager.Instance.Music(bgmvalue);
    }
    public void Play()
    {
        if(AudioManager.Instance)AudioManager.Instance.BtnSfx.Play();
        StartCoroutine(Loading("LevelSelection"));
        LoadingPanel.gameObject.SetActive(true);
    }
    IEnumerator Loading(string str)
    {
        fillBar.fillAmount = 0;
        fillImage.fillAmount = 0;
        while (fillBar.fillAmount < 1 && fillImage.fillAmount < 1)
        {
            fillBar.fillAmount += Time.deltaTime / TimeToLoad;
            fillImage.fillAmount += Time.deltaTime / TimeToLoad;
            yield return null;
        }

        SceneManager.LoadScene(str);
    }


    #region CheckVideoStatus
    public void CheckVideoStatus()
    {
        if (MyAdsManager.Instance != null)
        {
            if (MyAdsManager.Instance.IsRewardedAvailable())
            {
                StartCoroutine(ShowRewardedAd());
            }
            else
            {
                if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
                videoNotAvalible.SetActive(true);
                Invoke("videoPanelOf", 1.3f);
            }
        }
        else
        {
            if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
            videoNotAvalible.SetActive(true);
            Invoke("videoPanelOf", 1.3f);
        }
    }
    IEnumerator ShowRewardedAd()
    {
        if (MyAdsManager.Instance)
        {
            if (MyAdsManager.Instance.IsRewardedAvailable())
            {
                if (AdPenl)
                {
                    AdPenl.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                    AdPenl.SetActive(false);
                }
                MyAdsManager.Instance.ShowRewardedVideos();
            }
        }
    }
    #endregion

    #region RewardedVideoCompleted
    public void OnRewardedVideoComplete()
    {
        needMoreCoins.SetActive(false);
        GetRewardedCoinsPanel.SetActive(true);
        if (AudioManager.Instance) AudioManager.Instance.purchaseSFX.Play();
    }
    public void ClaimRewardedCoins()
    {
        GetRewardedCoinsPanel.SetActive(false);
        StartCoroutine(AddCoins(0,2000));
        if (AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
    }
    public void ClaimDailyRewardCoinsCoins()
    {
        RewardPanel.SetActive(false);
        StartCoroutine(AddCoins(0, GameManager.Instance.dailyRewardCoins));
        if (AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
    }
    #endregion

    #region videoPanelOf
    public void videoPanelOf()
    {
        videoNotAvalible.SetActive(false);
    }
    #endregion

    IEnumerator AddCoins(float delay, int Coins)
    {
        yield return new WaitForSeconds(delay);
        if (coinsAdder)
        {
            coinsAdder.addCoins = Coins;
            coinsAdder.addNow = true;
        }
    }
}
