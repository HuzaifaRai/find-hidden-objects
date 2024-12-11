using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    public GameObject AdPenl;
    public GameObject GetRewardedCoinsPanel;
    public GameObject ModeScroller;
    public GameObject needMoreCoins;
    public GameObject videoNotAvalible;
    public GameObject loadingPanel;
    public GameObject VsPanel;
    public GameObject StartBtn;
    public CoinsAdder coinsAdder;
    public Image fillBar;
    public int loadingTime;
    public Image OpponentAvater;
    public Text OpponentName;
    public Image playerAvater;
    public Text TotalCoins;
    public Text PlayerName;
    public Sprite[] AvatarSprites;
    private string[] oppoNames = { "Jackline", "Anizish", "Diana", "Muskan", "Jannat", "Iqra", "Dipti", "Maria" };
    private string[] SceneNames = { "CasualMode" , "LehngaMode", "SareeMode", };
    int oppoIndex, oppoNameNum;
    private int selectedIndex;
    private List<ItemInfo> modeList = new List<ItemInfo>();
    private void Start()
    {
        if (GAManager.Instance) GAManager.Instance.LogDesignEvent("Scene:" + SceneManager.GetActiveScene().name + SceneManager.GetActiveScene().buildIndex);
        if (GameManager.Instance.Initialized == false)
        {
            GameManager.Instance.Initialized = true;
            Rai_SaveLoad.LoadProgress();
        }
        PlayerName.text = SaveData.Instance.ProfileName;
        playerAvater.sprite = AvatarSprites[SaveData.Instance.SelectedPlayerAvatar];
        TotalCoins.text = SaveData.Instance.Coins.ToString();
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
    private void SetupData(List<bool> unlockItems, List<ItemInfo> _ItemsInfo)
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
        TotalCoins.text = SaveData.Instance.Coins.ToString();
        GetItemsInfo();
    }
    #endregion

    #region CheckSelectedItem
    private void CheckSelectedItem(List<ItemInfo> itemInfoList)
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
                        TotalCoins.text = SaveData.Instance.Coins.ToString();
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
                itemInfoList[selectedIndex].TickIcon.SetActive(true);
                if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
                GameManager.Instance.modeName = SceneNames[selectedIndex];
                StartCoroutine(VSAnim());
            }
        }
    }
    #endregion


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
        StartCoroutine(AddCoins(0, 2000));
        if (AudioManager.Instance) AudioManager.Instance.purchaseSFX.Play();
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
        StartCoroutine(VSAnim());
    }
    public void Play(string scene)
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        StartCoroutine(ShowInterstitialAD());
        StartCoroutine(Loading(scene));
    }
    public void Back()
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        StartCoroutine(Loading("MyMainMenu"));
    }
    IEnumerator Loading(string scene)
    {
        loadingPanel.SetActive(true);
        fillBar.fillAmount = 0f;
        yield return new WaitForSeconds(0.1f);
        while (fillBar.fillAmount < 1)
        {
            fillBar.fillAmount += Time.deltaTime / loadingTime;
            yield return null;
        }
        SceneManager.LoadScene(scene);
    }
    IEnumerator VSAnim()
    {
        VsPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (AudioManager.Instance) AudioManager.Instance.VsSfx.Play();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < Random.Range(25, 30); i++)
        {
            oppoIndex = Random.Range(0, AvatarSprites.Length);
            OpponentAvater.gameObject.SetActive(false);
            if (AudioManager.Instance.IconChangeSfx) AudioManager.Instance.IconChangeSfx    .Play();
            OpponentAvater.gameObject.SetActive(true);
            OpponentAvater.sprite = AvatarSprites[oppoIndex];
            oppoNameNum = Random.Range(0, 7);
            OpponentName.gameObject.SetActive(false); ;
            OpponentName.gameObject.SetActive(true); ;
            if (AudioManager.Instance.IDChangdSfx) AudioManager.Instance.IDChangdSfx.Play();
            OpponentName.text = oppoNames[oppoNameNum].ToString();
            yield return new WaitForSeconds(0.1f);
        }
        SaveData.Instance.OpponentIndex = oppoIndex;
        SaveData.Instance.OpponentName = oppoNames[oppoNameNum].ToString();
        OpponentAvater.sprite = AvatarSprites[oppoIndex];
        OpponentName.text = oppoNames[oppoNameNum].ToString();
        yield return new WaitForSeconds(0.5f);
        if (AudioManager.Instance.AppearSfx) AudioManager.Instance.AppearSfx.Play();
        StartBtn.SetActive(true);
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
                if (_ItemInfo[i].videoUnlock)
                {
                    if (_ItemInfo[i].VideoSlot)
                    {
                        _ItemInfo[i].VideoSlot.SetActive(true);
                    }
                    if (_ItemInfo[i].coinSlot)
                    {
                        _ItemInfo[i].coinSlot.SetActive(false);
                    }
                }
                else if (_ItemInfo[i].coinsUnlock)
                {
                    if (_ItemInfo[i].VideoSlot)
                    {
                        _ItemInfo[i].VideoSlot.SetActive(false);
                    }
                    if (_ItemInfo[i].coinSlot)
                    {
                        _ItemInfo[i].coinSlot.SetActive(true);
                        if (_ItemInfo[i].unlockCoins)
                        {
                            _ItemInfo[i].unlockCoins.text = _ItemInfo[i].requiredCoins.ToString();
                        }
                    }
                }
            }
            else
            {
                if (_ItemInfo[i].VideoSlot) _ItemInfo[i].VideoSlot.SetActive(false);
                if (_ItemInfo[i].coinSlot) _ItemInfo[i].coinSlot.SetActive(false);
            }
        }
    }
    #endregion
}
