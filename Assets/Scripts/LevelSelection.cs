using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public static LevelSelection instance;
    public static LevelSelection Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelSelection();
            }
            return instance;
        }
    }


    public GameObject AdPenl;
    public GameObject GetRewardedCoinsPanel;
    public GameObject ModeScroller;
    public GameObject needMoreCoins;
    public GameObject videoNotAvalible;
    public GameObject loadingPanel;
   public Image fillBar;
    public Image fillImage;
    [Range(4, 10)]
    public float TimeToLoad; 
    private string[] SceneNames = { "1. findobjects", "2. findobjects", "3. findobjects", "4. findobjects", "5. findobjects", "6. findobjects", "7. findobjects", "8. findobjects", "9. findobjects", "10. findobjects", "11. findobjects", "12. findobjects", "13. findobjects", "14. findobjects", "15. findobjects", "16. findobjects", "17. findobjects", "18. findobjects"};
    public GameObject[] levels;
    private int selectedIndex;
    [HideInInspector]
    public List<ItemInfo> modeList = new List<ItemInfo>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void Start()
    {
        if (GAManager.Instance) GAManager.Instance.LogDesignEvent("Scene:" + SceneManager.GetActiveScene().name + SceneManager.GetActiveScene().buildIndex);
        if (GameManager.Instance.Initialized == false)
        {
            GameManager.Instance.Initialized = true;
            Rai_SaveLoad.LoadProgress();
        }
        GameManager.Instance.TotalMode = levels.Length;
        #region Initialing Mode
        if (ModeScroller)
        {
            var modeinfo = ModeScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < modeinfo.Length; i++)
            {
                modeList.Add(modeinfo[i]);
            }
        }
        SetupData(SaveData.Instance.modeProps.ModeLocked, modeList);
        #endregion
        GetItemsInfo();
        UpdateLevelTexts();
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

    #region SetupItemData
    public void SetupData(List<bool> unlockItems, List<ItemInfo> _ItemsInfo)
    {
        if (_ItemsInfo.Count > 0)
        {
            if (unlockItems.Count < _ItemsInfo.Count)
            {
                for (int i = 0; i < _ItemsInfo.Count; i++)
                {
                    if (unlockItems.Count <= i)
                    {
                        // Add new data to SaveData file in case the file is empty or new data is available
                        unlockItems.Add(_ItemsInfo[i].isLocked);
                    }
                }
            }
            // Setting up Hairs Properties to actual Properties from SaveData file  
            for (int i = 0; i < _ItemsInfo.Count; i++)
            {
                _ItemsInfo[i].isLocked = unlockItems[i];
            }
            //Adding Click listeners to btns 
            for (int i = 0; i < _ItemsInfo.Count; i++)
            {
                int Index = i;
                if (_ItemsInfo[i].itemBtn)
                {
                    _ItemsInfo[i].itemBtn.onClick.AddListener(() =>
                    {
                        selectedIndex = Index;
                        SelectItem(Index);
                    });
                }
            }
        }
    }
    #endregion

    #region SelectItem
    public void SelectItem(int index)
    {
        CheckSelectedItem(modeList);
        GetItemsInfo();
    }
    #endregion

    #region CheckSelectedItem
    public void CheckSelectedItem(List<ItemInfo> itemInfoList)
    {
        if (itemInfoList.Count > selectedIndex)
        {
            if (itemInfoList[selectedIndex].isLocked)
            {
                if (itemInfoList[selectedIndex].coinsUnlock)
                {
                    if (SaveData.Instance.Coins >= itemInfoList[selectedIndex].requiredCoins)
                    {
                        itemInfoList[selectedIndex].isLocked = false;
                        SaveData.Instance.Coins -= itemInfoList[selectedIndex].requiredCoins;
                        SaveData.Instance.modeProps.ModeLocked[selectedIndex] = false;
                        Rai_SaveLoad.SaveProgress();
                        if (AudioManager.Instance.purchaseSFX) AudioManager.Instance.purchaseSFX.Play();
                        SelectItem(selectedIndex);
                    }
                    else
                    {
                        if (needMoreCoins)
                            needMoreCoins.SetActive(true);
                        if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
                    }
                }
            }
            else
            {
                if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
                GameManager.Instance.modeName = SceneNames[selectedIndex];
                GameManager.Instance.ModeIndex = selectedIndex;
                Play();
            }
        }
    }
    #endregion

    private void UpdateLevelTexts()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            Transform levelChild1 = levels[i].transform.GetChild(1); 

            if (levelChild1 != null)
            {
                Transform grandChild = levelChild1.GetChild(0); 

                if (grandChild != null)
                {
                    Text levelText = grandChild.GetComponent<Text>();

                    if (levelText != null)
                    {
                        levelText.text = "Level " + (i + 1); 
                    }
                }
            }
        }
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
    #endregion

    public void NeedMoreCoinIsTrue(bool IsTrue)
    {
        if (IsTrue == true)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopinSfx.Play();
        }
        else if (IsTrue == false)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopOutSfx.Play();
        }
        needMoreCoins.SetActive(IsTrue);
    }

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
        //StartCoroutine(AddCoins(0, 2000));
        if (AudioManager.Instance) AudioManager.Instance.purchaseSFX.Play();
    }
    #endregion

    //IEnumerator AddCoins(float delay, int Coins)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if (coinsAdder)
    //    {
    //        coinsAdder.addCoins = Coins;
    //        coinsAdder.addNow = true;
    //    }
    //}

    #region videoPanelOf
    public void videoPanelOf()
    {
        videoNotAvalible.SetActive(false);
    }
    #endregion

    public void SelectedScene(string scene)
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        GameManager.Instance.modeName = scene;
    }
    public void Play()
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        StartCoroutine(ShowInterstitialAD());
        StartCoroutine(Loading(GameManager.Instance.modeName));
        loadingPanel.gameObject.SetActive(true);
    }
    public void Back()
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        StartCoroutine(ShowInterstitialAD());
        StartCoroutine(Loading("MyMainMenu"));
        loadingPanel.gameObject.SetActive(true);
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

    IEnumerator ShowInterstitialAD()
    {
        if (MyAdsManager.Instance)
        {
            if (MyAdsManager.Instance.IsInterstitialAvailable())
            {
                if (AdPenl)
                {
                    AdPenl.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                    AdPenl.SetActive(false);
                }
                MyAdsManager.Instance.ShowInterstitialAds();
            }
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

    #region GetItemsInfo
    private void GetItemsInfo()
    {
        SetItemsInfo(modeList);
    }
    #endregion

    #region SetItemsInfo
    private void SetItemsInfo(List<ItemInfo> _ItemInfo)
    {
        if (_ItemInfo == null) return;
        for (int i = 0; i < _ItemInfo.Count; i++)
        {
            if (_ItemInfo[i].isLocked)
            {
                if (_ItemInfo[i].LockIcon) _ItemInfo[i].LockIcon.SetActive(true);
                
            }
            else
            {
                if (_ItemInfo[i].LockIcon) _ItemInfo[i].LockIcon.SetActive(false);
            }
        }
    }
    #endregion
}
