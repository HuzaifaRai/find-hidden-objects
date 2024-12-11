using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.IO;
using Coffee.UIEffects;

[System.Serializable]
public class LehngaUiElements
{
    [Header("Panels")]
    public GameObject GamePlayPanel;
    public GameObject RewardCollectPanel;
    public GameObject RewardCoinPanel;
    public GameObject AdPenl;
    public GameObject SS_Panel;
    [Header("Scrollers")]
    public GameObject aLLScrollers;
    public GameObject itemsScroller, dressScroller, hairScroller, lipstickScroller, eyeScroller, blushScroller, necklaceScroller, 
                      BagScroller, BraceletScroller, EyeBrowsScroller, EarringScroller, shoeScroller, bGScroller;
    [Header("UI")]
    public GameObject needMoreCoins;
    public GameObject videoNotAvalible, unlockPanel, levelComplete, levelWin, levelFailed;
    [Header("Images")]
    public Image screenShotImg;
    public Image rewardImage;
    public Image dressImage;
    public Image hairImage, lipstickImage, eyeImage, blushImage, necklaceImage, BagImage, BraceletImage, EyeBrowsImage, EarringImage, shoeImage, bGImage;
    public Image DefaultdressImage, DefaultHairs, DefaultLips, DefaultEyeShades, DefaultEyeBrow;
    [Header("Button")]
    public GameObject scorePanel;
    public GameObject CoinSloat;
}
[System.Serializable]
public class LehngaOpponent
{
    [Header("GameObject")]
    public GameObject previewButton;
    public GameObject myInfo;
    public GameObject oppoInfo;
    [Header("Images")]
    public Image oppoDressImage;
    public Image oppoHairImage, oppoLipstickImage, oppoEyeImage, oppoBlushImage, oppoNecklaceImage, oppoBagImage, oppoBraceletImage, oppoEyeBrowsImage,
                 oppoEarringImage, oppoShoeImage;
    [Header("Text")]
    public Text playerTotal;
    public Text oppoTotal, requirdCoins, unlockPanelScore;
    [Header("Lables")]
    public Image ItemIcon;
}
[System.Serializable]
public enum LehngaSelectedItem
{
    Dress, Hair, Blush, EyeBrows, Eye, LipStick, NeckLace, Earring, Bracelet, Bag, Shoes, BG,
}
public class LehngaMode : MonoBehaviour
{
    public LehngaSelectedItem selectedItem;
    public LehngaUiElements uIElements;
    public LehngaOpponent oppElements;
    [Header("PlayerInfo")]
    public Image AvatarInMode;
    public Image oppoAvatarInMode;
    public Text PlayerNameInMode;
    public Text PlayerNameInJudgement;
    public Text opponentNameInMode;
    public Text opponentNameInJudgement;
    [Header("Loading")]
    public GameObject loadingPanel;
    public Image fillBar;
    [Header("Panels")]
    public GameObject GamePanel;
    public GameObject exitPanel;
    public GameObject preViewPanel;
    public GameObject judgementPanel;
    [Header("Elements")]
    public MRS_Manager CharactorMover;
    public MRS_Manager OpponentMover;
    public CoinsAdder coinsAdder;
    [Header("Text")]
    public Text TotalCoins;
    public Text totalScore;
    [Header("Particals")]
    public GameObject Confetti;
    public GameObject scorePartical;
    [Header("Arrays")]
    public GameObject[] categories;
    [Header("Sprites")]
    public Sprite[] dressSprites;
    public Sprite[] hairSprites;
    public Sprite[] lipStickSprites;
    public Sprite[] eyeSprites;
    public Sprite[] blushSprites;
    public Sprite[] necklaceSprites;
    public Sprite[] BagSprite;
    public Sprite[] BraceletSprites;
    public Sprite[] EyeBrowsSprires;
    public Sprite[] EarringSprites;
    public Sprite[] shoesSprites;
    public Sprite[] bGSprites;
    public Sprite[] botSprites;
    public Sprite greyTickSprites;
    public Sprite GreenTickSprites;
    private List<ItemInfo> dressList = new List<ItemInfo>();
    private List<ItemInfo> hairList = new List<ItemInfo>();
    private List<ItemInfo> lipstickList = new List<ItemInfo>();
    private List<ItemInfo> eyeList = new List<ItemInfo>();
    private List<ItemInfo> blushList = new List<ItemInfo>();
    private List<ItemInfo> necklaceList = new List<ItemInfo>();
    private List<ItemInfo> BagList = new List<ItemInfo>();
    private List<ItemInfo> BraceletList = new List<ItemInfo>();
    private List<ItemInfo> EyeBrowsList = new List<ItemInfo>();
    private List<ItemInfo> EarringList = new List<ItemInfo>();
    private List<ItemInfo> shoesList = new List<ItemInfo>();
    private List<ItemInfo> bGList = new List<ItemInfo>();

    private ItemInfo tempItem;
    private int selectedIndex;
    private int catindex;
    [HideInInspector]
    bool ADTime = true;
    bool IsDressTrue, IsHairTrue, IsLipsTrue, IsNecklacetrue = false;
    private bool canShowInterstitial;
    private int dressValue, hairValue, lipstickValue, eyeValue, blushValue, neckLaceValue, BagValue,
                BraceletValue, EyeBrowsValue, EarringValue, shoesValue, bGValue;
    private int oppoDressValue, oppoHairValue, oppoLipstickValue, oppoEyeValue, oppoBlushValue, oppoNeckLaceValue,
                oppoBagValue, oppoBraceletValue, oppoEyeBrowsValue, oppoEarringValue, oppoShoesValue;
    //Dressup
    private int[] DressScore = { 8200, 8300, 9100, 9300, 9400, 9500, 9450, 9550, 8600, 9150, 9250, 8250, 8200, 9350, 9650 };
    private int[] HairScore  = { 7200, 7300, 8100, 8300, 8400, 8500, 8450, 8550, 7600, 8250, 8150, 7250, 7200, 8350, 8650 };
    private int[] ShoesScore = { 5200, 5300, 6100, 6300, 6400, 6500, 6450, 6550, 5600, 6250, 6150, 5250, 5200, 6350, 6650 };
    private int[] BGScore    = { 2200, 2300, 3100, 3300, 3400, 3500, 3450, 3550, 2600, 3250, 3150, 2250, 2200, 3350, 3650 };
    //makerup
    private int[] LipstickScore    = { 2200, 2300,  3100, 3300, 3400, 3500, 3450, 3550, 2600, 3250, 3150, 2250, 2200, 3350, 3650 };
    private int[] EyeScore         = { 2200, 2300,  3100, 3300, 3400, 3500, 3450, 3550, 2600, 3250, 3150, 2250, 2200, 3350, 3650 };
    private int[] BlushScore       = { 2200, 2300,  3100, 3300, 3400, 3500, 3450, 3550, 2600, 3250, 3150, 2250, 2200, 3350, 3650 };
    //accessories
    private int[] NeckLaceScore  = { 4200, 4300, 5100, 5300, 5400, 5500, 5450, 5550, 4600, 5250, 5150, 4250, 4200, 5350, 5650 };
    private int[] BagScore       = { 4200, 4300, 5100, 5300, 5400, 5500, 5450, 5550, 4600, 5250, 5150, 4250, 4200, 5350, 5650 };
    private int[] BraceletScore  = { 4200, 4300, 5100, 5300, 5400, 5500, 5450, 5550, 4600, 5250, 5150, 4250, 4200, 5350, 5650 };
    private int[] EyeBrowsScore = { 4200, 4300, 5100, 5300, 5400, 5500, 5450, 5550, 4600, 5250, 5150, 4250, 4200, 5350, 5650 };
    private int[] EarringScore   = { 4200, 4300, 5100, 5300, 5400, 5500, 5450, 5550, 4600, 5250, 5150, 4250, 4200, 5350, 5650 };

    bool CanShowAd = false;
    int adelay = 30;
    private enum RewardType
    {
        none, Coins, SelectionItem
    }
    private RewardType rewardType;


    #region start
    private void Start()
    {
        if (GAManager.Instance) GAManager.Instance.LogDesignEvent("Scene:" + SceneManager.GetActiveScene().name + SceneManager.GetActiveScene().buildIndex);
        if (GameManager.Instance.Initialized == false)
        {
            GameManager.Instance.Initialized = true;
            Rai_SaveLoad.LoadProgress();
        }
        selectedItem = LehngaSelectedItem.Dress;
        uIElements.dressScroller.SetActive(true);
        TotalCoins.text = SaveData.Instance.Coins.ToString();
        GamePanel.gameObject.SetActive(false);
        StartCoroutine(ObjectActivation(GamePanel,0f,true));
        SetInitialValues();
        GetItemsInfo();
        StartCoroutine(AdDelay(adelay));
        AvatarInMode.sprite = botSprites[SaveData.Instance.SelectedPlayerAvatar];
        oppoAvatarInMode.sprite = botSprites[SaveData.Instance.OpponentIndex];
        PlayerNameInJudgement.text = PlayerNameInMode.text = SaveData.Instance.ProfileName;
        opponentNameInJudgement.text = opponentNameInMode.text = SaveData.Instance.OpponentName;
        CharactorMover.Move(new Vector3(DressUpPos.x, DressUpPos.y, DressUpPos.z), 0.5f, true, false);
        Invoke("CanShowTrue", 10f);
    }
    public void CanShowTrue()
    {
        CanShowAd = true;
    }
    #endregion

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

    #region Exit
    public void Exit(bool IsTrue)
    {
        if (IsTrue == true)
        {
            if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
        }
        else if (IsTrue == false)
        {
            if (AudioManager.Instance.PopOutSfx) AudioManager.Instance.PopOutSfx.Play();
        }
        exitPanel.SetActive(IsTrue);
    }
    #endregion

    #region SetInitialValues
    private void SetInitialValues()
    {

        #region Initialing Dress
        if (uIElements.dressScroller)
        {
            var dressinfo = uIElements.dressScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < dressinfo.Length; i++)
            {
                dressList.Add(dressinfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.dressLocked, dressList);
        SetItemIcon(dressList, dressSprites);
        #endregion

        #region Initialing hair
        if (uIElements.hairScroller)
        {
            var hairInfo = uIElements.hairScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < hairInfo.Length; i++)
            {
                hairList.Add(hairInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.hairLocked, hairList);
        SetItemIcon(hairList, hairSprites);
        #endregion

        #region Initialing LipsTick
        if (uIElements.lipstickScroller)
        {
            var lipsTickInfo = uIElements.lipstickScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < lipsTickInfo.Length; i++)
            {
                lipstickList.Add(lipsTickInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.lipStickLocked, lipstickList);
        SetItemIcon(lipstickList, lipStickSprites);
        #endregion

        #region Initialing Eye
        if (uIElements.eyeScroller)
        {
            var topinfo = uIElements.eyeScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < topinfo.Length; i++)
            {
                eyeList.Add(topinfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.eyesLocked, eyeList);
        SetItemIcon(eyeList, eyeSprites);
        #endregion

        #region Initialing Blush
        if (uIElements.blushScroller)
        {
            var MehndiInfo = uIElements.blushScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < MehndiInfo.Length; i++)
            {
                blushList.Add(MehndiInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.blushLocked, blushList);
        SetItemIcon(blushList, blushSprites);
        #endregion
        
        #region Initialing NeckLace
        if (uIElements.necklaceScroller)
        {
            var neckLaceInfo = uIElements.necklaceScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < neckLaceInfo.Length; i++)
            {
                necklaceList.Add(neckLaceInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.necklaceLocked, necklaceList);
        SetItemIcon(necklaceList, necklaceSprites);
        #endregion 
        
        #region Initialing Bag
        if (uIElements.BagScroller)
        {
            var jhumkaInfo = uIElements.BagScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < jhumkaInfo.Length; i++)
            {
                BagList.Add(jhumkaInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.bagLocked, BagList);
        SetItemIcon(BagList, BagSprite);
        #endregion

        #region Initialing Bracelet
        if (uIElements.BraceletScroller)
        {
            var BindiInfo = uIElements.BraceletScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < BindiInfo.Length; i++)
            {
                BraceletList.Add(BindiInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.braceletLocked, BraceletList);
        SetItemIcon(BraceletList, BraceletSprites);
        #endregion

        #region Initialing EyeBrows
        if (uIElements.EyeBrowsScroller)
        {
            var malaInfo = uIElements.EyeBrowsScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < malaInfo.Length; i++)
            {
                EyeBrowsList.Add(malaInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.eyerowsLocked, EyeBrowsList);
        SetItemIcon(EyeBrowsList, EyeBrowsSprires);
        #endregion

        #region Initialing Earring
        if (uIElements.EarringScroller)
        {
            var handThingsInfo = uIElements.EarringScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < handThingsInfo.Length; i++)
            {
                EarringList.Add(handThingsInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.earringLocked, EarringList);
        SetItemIcon(EarringList, EarringSprites);
        #endregion

        #region Initialing Shoes
        if (uIElements.shoeScroller)
        {
            var ShoesInfo = uIElements.shoeScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < ShoesInfo.Length; i++)
            {
                shoesList.Add(ShoesInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.shoesLocked, shoesList);
        SetItemIcon(shoesList, shoesSprites);
        #endregion

        #region Initialing BG
        if (uIElements.bGScroller)
        {
            var BGInfo = uIElements.bGScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < BGInfo.Length; i++)
            {
                bGList.Add(BGInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.lehngaProps.bGLocked, bGList);
        SetItemIcon(bGList, bGSprites);
        #endregion

        Rai_SaveLoad.SaveProgress();
    }
    #endregion

    #region SetupItemData
    private void SetupItemData(List<bool> unlockItems, List<ItemInfo> _ItemsInfo)
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

    #region SetItemIcon
    private void SetItemIcon(List<ItemInfo> refList, Sprite[] btnIcons)
    {
        if (refList != null)
        {
            for (int i = 0; i < refList.Count; i++)
            {
                if (btnIcons.Length > i)
                {
                    if (btnIcons[i] && refList[i].itemIcon)
                    {
                        refList[i].itemIcon.sprite = btnIcons[i];
                    }
                }
            }
        }
    }
    #endregion

    #region SelectedCatagory
    private void DisableScrollers()
    {
        for (int i = 0; i < categories.Length; i++)
        {
            categories[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        uIElements.dressScroller.SetActive(false);
        uIElements.hairScroller.SetActive(false);
        uIElements.lipstickScroller.SetActive(false);
        uIElements.eyeScroller.SetActive(false);
        uIElements.blushScroller.SetActive(false);
        uIElements.necklaceScroller.SetActive(false);
        uIElements.BagScroller.SetActive(false);
        uIElements.BraceletScroller.SetActive(false);
        uIElements.EyeBrowsScroller.SetActive(false);
        uIElements.EarringScroller.SetActive(false);
        uIElements.shoeScroller.SetActive(false);
        uIElements.bGScroller.SetActive(false);
    }
    Vector3 DressUpPos = new Vector3(100,-100,400);
    Vector3 MakeUpPos = new Vector3(50,-450,-1000);
    public void SelectedCategory(int index)
    {
        uIElements.unlockPanel.SetActive(false);
        catindex = index;
        DisableScrollers();
        if (AudioManager.Instance.CategorySelectSFX) AudioManager.Instance.CategorySelectSFX.Play();
        categories[index].transform.GetChild(0).gameObject.SetActive(true);
        if (index == (int)LehngaSelectedItem.Dress)
        {
            selectedItem = LehngaSelectedItem.Dress;
            uIElements.dressScroller.SetActive(true);
            CharactorMover.Move(new Vector3(DressUpPos.x,DressUpPos.y,DressUpPos.z), 0.5f, true, false);
        }
        else if (index == (int)LehngaSelectedItem.Hair)
        {
            selectedItem = LehngaSelectedItem.Hair;
            uIElements.hairScroller.SetActive(true);
            CharactorMover.Move(new Vector3(MakeUpPos.x, MakeUpPos.y, MakeUpPos.z), 0.5f, true, false);
        }
        else if (index == (int)LehngaSelectedItem.Bag)
        {
            selectedItem = LehngaSelectedItem.Bag;
            uIElements.BagScroller.SetActive(true);
            CharactorMover.Move(new Vector3(DressUpPos.x,DressUpPos.y,DressUpPos.z), 0.5f, true, false);

        }
        else if (index == (int)LehngaSelectedItem.Shoes)
        {
            selectedItem = LehngaSelectedItem.Shoes;
            uIElements.shoeScroller.SetActive(true);
            CharactorMover.Move(new Vector3(DressUpPos.x,DressUpPos.y,DressUpPos.z), 0.5f, true, false);
        }
        else if (index == (int)LehngaSelectedItem.BG)
        {
            selectedItem = LehngaSelectedItem.BG;
            uIElements.bGScroller.SetActive(true);
            CharactorMover.Move(new Vector3(DressUpPos.x,DressUpPos.y,DressUpPos.z), 0.5f, true, false);

        }
        else if (index == (int)LehngaSelectedItem.Blush)
        {
            selectedItem = LehngaSelectedItem.Blush;
            uIElements.blushScroller.SetActive(true);
            CharactorMover.Move(new Vector3(MakeUpPos.x, MakeUpPos.y, MakeUpPos.z), 0.5f, true, false);
        }
        else if (index == (int)LehngaSelectedItem.Eye)
        {
            selectedItem = LehngaSelectedItem.Eye;
            uIElements.eyeScroller.SetActive(true);
            CharactorMover.Move(new Vector3(MakeUpPos.x, MakeUpPos.y, MakeUpPos.z), 0.5f, true, false);
        }
        else if (index == (int)LehngaSelectedItem.LipStick)
        {
            selectedItem = LehngaSelectedItem.LipStick;
            uIElements.lipstickScroller.SetActive(true);
            CharactorMover.Move(new Vector3(MakeUpPos.x, MakeUpPos.y, MakeUpPos.z), 0.5f, true, false);
        }
        else if (index == (int)LehngaSelectedItem.NeckLace)
        {
            selectedItem = LehngaSelectedItem.NeckLace;
            uIElements.necklaceScroller.SetActive(true);
            CharactorMover.Move(new Vector3(MakeUpPos.x, MakeUpPos.y, MakeUpPos.z), 0.5f, true, false);

        }
        else if (index == (int)LehngaSelectedItem.EyeBrows)
        {
            selectedItem = LehngaSelectedItem.EyeBrows;
            uIElements.EyeBrowsScroller.SetActive(true);
            CharactorMover.Move(new Vector3(MakeUpPos.x, MakeUpPos.y, MakeUpPos.z), 0.5f, true, false);

        }
        else if (index == (int)LehngaSelectedItem.Bracelet)
        {
            selectedItem = LehngaSelectedItem.Bracelet;
            uIElements.BraceletScroller.SetActive(true);
            CharactorMover.Move(new Vector3(DressUpPos.x, DressUpPos.y, DressUpPos.z), 0.5f, true, false);
        }
        else if (index == (int)LehngaSelectedItem.Earring)
        {
            selectedItem = LehngaSelectedItem.Earring;
            uIElements.EarringScroller.SetActive(true);
            CharactorMover.Move(new Vector3(MakeUpPos.x, MakeUpPos.y, MakeUpPos.z), 0.5f, true, false);
        }
        GetItemsInfo();
    }
    #endregion

    #region GetItemsInfo
    private void GetItemsInfo()
    {
        if (selectedItem == LehngaSelectedItem.Dress)
        {
            SetItemsInfo(dressList, DressScore);
        }
        else if (selectedItem == LehngaSelectedItem.Hair)
        {
            SetItemsInfo(hairList, HairScore);
        }
        else if (selectedItem == LehngaSelectedItem.LipStick)
        {
            SetItemsInfo(lipstickList, LipstickScore);
        }
        else if (selectedItem == LehngaSelectedItem.Eye)
        {
            SetItemsInfo(eyeList, EyeScore);
        }
        else if (selectedItem == LehngaSelectedItem.Blush)
        {
            SetItemsInfo(blushList, BlushScore);
        }
        else if (selectedItem == LehngaSelectedItem.NeckLace)
        {
            SetItemsInfo(necklaceList, NeckLaceScore);
        }
        else if (selectedItem == LehngaSelectedItem.Bag)
        {
            SetItemsInfo(BagList, BagScore);
        }
        else if (selectedItem == LehngaSelectedItem.Bracelet)
        {
            SetItemsInfo(BraceletList, BraceletScore);
        }
        else if (selectedItem == LehngaSelectedItem.EyeBrows)
        {
            SetItemsInfo(EyeBrowsList, EyeBrowsScore);
        }
        else if (selectedItem == LehngaSelectedItem.Earring)
        {
            SetItemsInfo(EarringList, EarringScore);
        }
        else if (selectedItem == LehngaSelectedItem.Shoes)
        {
            SetItemsInfo(shoesList, ShoesScore);
        }
        else if (selectedItem == LehngaSelectedItem.BG)
        {
            SetItemsInfo(bGList, BGScore);
        }
    }
    #endregion

    #region SetItemsInfo
    private void SetItemsInfo(List<ItemInfo> _ItemInfo, int[] ScoureArray)
    {
        if (_ItemInfo == null) return;
        for (int i = 0; i < _ItemInfo.Count; i++)
        {
            if (_ItemInfo[i].scoreText)
            {
                _ItemInfo[i].scoreText.text = ScoureArray[i].ToString();
            }
            if (_ItemInfo[i].isLocked)
            {
                if(_ItemInfo[i].LockIcon) _ItemInfo[i].LockIcon.SetActive(true);
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
                if(_ItemInfo[i].LockIcon) _ItemInfo[i].LockIcon.SetActive(false);
                if (_ItemInfo[i].VideoSlot) _ItemInfo[i].VideoSlot.SetActive(false);
                if (_ItemInfo[i].coinSlot) _ItemInfo[i].coinSlot.SetActive(false);
            }
        }
    }
    #endregion

    #region SelectItem
    public void SelectItem(int index)
    {
        if (selectedItem == LehngaSelectedItem.Dress)
        {
            CheckSelectedItem(dressList, dressSprites, uIElements.dressImage);
        }
        else if (selectedItem == LehngaSelectedItem.Hair)
        {
            CheckSelectedItem(hairList, hairSprites, uIElements.hairImage);
        }
        else if (selectedItem == LehngaSelectedItem.LipStick)
        {
            CheckSelectedItem(lipstickList, lipStickSprites, uIElements.lipstickImage);
        }
        else if (selectedItem == LehngaSelectedItem.Eye)
        {
            CheckSelectedItem(eyeList, eyeSprites, uIElements.eyeImage);
        }
        else if (selectedItem == LehngaSelectedItem.Blush)
        {
            CheckSelectedItem(blushList, blushSprites, uIElements.blushImage);
        }
        else if (selectedItem == LehngaSelectedItem.NeckLace)
        {
            CheckSelectedItem(necklaceList, necklaceSprites, uIElements.necklaceImage);
        }
        else if (selectedItem == LehngaSelectedItem.Bag)
        {
            CheckSelectedItem(BagList, BagSprite, uIElements.BagImage);
        }
        else if (selectedItem == LehngaSelectedItem.Bracelet)
        {
            CheckSelectedItem(BraceletList, BraceletSprites, uIElements.BraceletImage);
        }
        else if (selectedItem == LehngaSelectedItem.EyeBrows)
        {
            CheckSelectedItem(EyeBrowsList, EyeBrowsSprires, uIElements.EyeBrowsImage);
        }
        else if (selectedItem == LehngaSelectedItem.Earring)
        {
            CheckSelectedItem(EarringList, EarringSprites, uIElements.EarringImage);
        }
        else if (selectedItem == LehngaSelectedItem.Shoes)
        {
            CheckSelectedItem(shoesList, shoesSprites, uIElements.shoeImage);
        }
        else if (selectedItem == LehngaSelectedItem.BG)
        {
            CheckSelectedItem(bGList, bGSprites, uIElements.bGImage);
        }
        TotalCoins.text = SaveData.Instance.Coins.ToString();
        GetItemsInfo();
    }
    #endregion

    #region CheckSelectedItem
    private void CheckSelectedItem(List<ItemInfo> itemInfoList, Sprite[] itemSprites, Image itemImage)
    {
        rewardType = RewardType.SelectionItem;
        if (itemInfoList.Count > selectedIndex)
        {
            tempItem = itemInfoList[selectedIndex];
            if (itemInfoList[selectedIndex].isLocked)
            {
                if (itemInfoList[selectedIndex].videoUnlock)
                {
                    CheckVideoStatus();
                }
                if (itemInfoList[selectedIndex].coinsUnlock)
                {
                    if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
                    oppElements.ItemIcon.sprite = itemInfoList[selectedIndex].itemIcon.sprite;
                    oppElements.requirdCoins.text = itemInfoList[selectedIndex].requiredCoins.ToString();
                    oppElements.unlockPanelScore.text = itemInfoList[selectedIndex].scoreText.text;
                    uIElements.unlockPanel.SetActive(true);
                    rewardType = RewardType.Coins;
                }
            }
            else
            {
                if (itemSprites.Length > selectedIndex)
                {
                    if (itemSprites[selectedIndex])
                    {
                        if (itemImage)
                        {
                            categories[catindex].transform.GetChild(2).GetComponent<Image>().sprite = GreenTickSprites;
                            if (selectedItem == LehngaSelectedItem.Dress)
                            {
                                IsDressTrue = true;
                                dressValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                                uIElements.DefaultdressImage.gameObject.SetActive(false);
                            }
                            else if (selectedItem == LehngaSelectedItem.Hair)
                            {
                                IsHairTrue = true;
                                hairValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                                uIElements.DefaultHairs.gameObject.SetActive(false);
                            }
                            else if (selectedItem == LehngaSelectedItem.LipStick)
                            {
                                lipstickValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                                uIElements.DefaultLips.gameObject.SetActive(false);
                                IsLipsTrue = true;
                            }
                            else if (selectedItem == LehngaSelectedItem.Eye)
                            {
                                eyeValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                            }
                            else if (selectedItem == LehngaSelectedItem.Blush)
                            {
                                blushValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                            }
                            else if (selectedItem == LehngaSelectedItem.NeckLace)
                            {
                                neckLaceValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                                IsNecklacetrue = true;
                            }
                            else if (selectedItem == LehngaSelectedItem.Bag)
                            {
                                BagValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                            }
                            else if (selectedItem == LehngaSelectedItem.Bracelet)
                            {
                                BraceletValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                            }
                            else if (selectedItem == LehngaSelectedItem.EyeBrows)
                            {
                                uIElements.DefaultEyeBrow.gameObject.SetActive(false);
                                EyeBrowsValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                            }
                            else if (selectedItem == LehngaSelectedItem.Earring)
                            {
                                EarringValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                            }
                            else if (selectedItem == LehngaSelectedItem.Shoes)
                            {
                                shoesValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                            }
                            else if (selectedItem == LehngaSelectedItem.BG)
                            {
                                bGValue = int.Parse(itemInfoList[selectedIndex].scoreText.text);
                            }
                            StartCoroutine(SoureParticalPlay(itemInfoList));
                            for (int i = 0; i < itemInfoList.Count; i++)
                            {
                                itemInfoList[i].itemBtn.interactable = true;
                                if(i == selectedIndex)
                                {
                                    itemInfoList[i].itemBtn.interactable = true;
                                    itemInfoList[i].transform.GetChild(0).gameObject.SetActive(true);
                                }
                                else
                                {
                                    itemInfoList[i].transform.GetChild(0).gameObject.SetActive(false);
                                    itemInfoList[selectedIndex].itemBtn.interactable = false;
                                }
                            }
                            if(itemImage) itemImage.gameObject.SetActive(false);
                            if(itemImage) itemImage.gameObject.SetActive(true);
                            if (itemImage) itemImage.GetComponent<UIShiny>().Play();
                            if(itemImage) itemImage.sprite = itemSprites[selectedIndex];
                            if (AudioManager.Instance.itemSelectSFX) AudioManager.Instance.itemSelectSFX.Play();
                            if (IsDressTrue == true && IsHairTrue == true && IsLipsTrue == true && IsNecklacetrue == true)
                            {
                                if(oppElements.previewButton.activeInHierarchy == false)
                                {
                                    if (AudioManager.Instance.AppearSfx) AudioManager.Instance.AppearSfx.Play();
                                }
                                oppElements.previewButton.SetActive(true);
                            }
                        }
                    }
                }
                CheckInterstitialAD();
            }
        }
    }
    public void UnSelectItem(List<ItemInfo> itemInfoList)
    {
        for (int i = 0; i < itemInfoList.Count; i++)
        {
            itemInfoList[i].itemBtn.interactable = true;
            itemInfoList[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void UnEquipItem()
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        categories[catindex].transform.GetChild(2).GetComponent<Image>().sprite = greyTickSprites;
        if (selectedItem == LehngaSelectedItem.Dress)
        {
            UnSelectItem(dressList);
            dressValue = 0;
            IsDressTrue = false;
            uIElements.DefaultdressImage.gameObject.SetActive(true);
            uIElements.dressImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.Hair)
        {
            UnSelectItem(hairList);
            hairValue = 0;
            IsHairTrue = false;
            uIElements.DefaultHairs.gameObject.SetActive(true);
            uIElements.hairImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.LipStick)
        {
            UnSelectItem(lipstickList);
            lipstickValue = 0;
            uIElements.DefaultLips.gameObject.SetActive(true);
            uIElements.lipstickImage.gameObject.SetActive(false);
            IsLipsTrue = false;
        }
        else if (selectedItem == LehngaSelectedItem.Eye)
        {
            UnSelectItem(eyeList);
            eyeValue = 0;
            uIElements.eyeImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.Blush)
        {
            UnSelectItem(blushList);
            blushValue = 0;
            uIElements.blushImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.NeckLace)
        {
            UnSelectItem(necklaceList);
            neckLaceValue = 0;
            IsNecklacetrue = false;
            uIElements.necklaceImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.Bag)
        {
            UnSelectItem(BagList);
            BagValue = 0;
            uIElements.BagImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.Bracelet)
        {
            UnSelectItem(BraceletList);
            BraceletValue = 0;
            uIElements.BraceletImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.EyeBrows)
        {
            UnSelectItem(EyeBrowsList);
            EyeBrowsValue = 0;
            uIElements.EyeBrowsImage.gameObject.SetActive(false);
            uIElements.DefaultEyeBrow.gameObject.SetActive(true);
        }
        else if (selectedItem == LehngaSelectedItem.Earring)
        {
            UnSelectItem(EarringList);
            EarringValue = 0;
            uIElements.EarringImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.Shoes)
        {
            UnSelectItem(shoesList);
            shoesValue = 0;
            uIElements.shoeImage.gameObject.SetActive(false);
        }
        else if (selectedItem == LehngaSelectedItem.BG)
        {
            UnSelectItem(bGList);
            bGValue = 0;
            uIElements.bGImage.sprite = bGSprites[0];
        }
        totalScore.text = (dressValue + hairValue + EarringValue + BraceletValue + eyeValue + lipstickValue 
        + blushValue + neckLaceValue + shoesValue + BagValue + bGValue + EyeBrowsValue).ToString();
    }

    #endregion

    #region Instantiate
    IEnumerator SoureParticalPlay(List<ItemInfo> itemInfo)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject partical = Instantiate(scorePartical, itemInfo[selectedIndex].transform);
        partical.GetComponent<Text>().text = itemInfo[selectedIndex].scoreText.text;
        partical.transform.parent = totalScore.transform;
        partical.SetActive(true);
        particalmove();
        yield return new WaitForSeconds(1);
        totalScore.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        if (AudioManager.Instance.CollectSfx) AudioManager.Instance.CollectSfx.Play();
        yield return new WaitForSeconds(0.5f);
        totalScore.text = (dressValue + hairValue + EarringValue + BraceletValue + eyeValue + lipstickValue 
        + blushValue + neckLaceValue + shoesValue + BagValue + bGValue + EyeBrowsValue).ToString();
    }
    #endregion

    #region Btnfunctions
    public void Play(string str)
    {
        if (CanShowAd == true) StartCoroutine(ShowInterstitialAD());
        uIElements.GamePlayPanel.SetActive(false);
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        if (Confetti) Confetti.gameObject.SetActive(false);
        StartCoroutine(LoadingScene(str));
    }
    
    public void Back()
    {
        uIElements.CoinSloat.SetActive(true);
        uIElements.scorePanel.SetActive(true);
        uIElements.aLLScrollers.SetActive(true);
        preViewPanel.SetActive(false);
    }
    public void Preview()
    {
        uIElements.CoinSloat.SetActive(false);
        uIElements.scorePanel.SetActive(false);
        uIElements.aLLScrollers.SetActive(false);
        StartCoroutine(PreviewAnim());
    }
    IEnumerator PreviewAnim()
    {
        CharactorMover.Move(new Vector3(0, -50, 0), 0.3f, true, false);
        yield return new WaitForSeconds(1f);
        CharactorMover.Move(new Vector3(0, -700, -1000), 0.5f, true, false);
        yield return new WaitForSeconds(0.7f);
        CharactorMover.Move(new Vector3(0, 500, -1000), 1f, true, false);
        yield return new WaitForSeconds(1.1f);
        CharactorMover.Move(new Vector3(0, -50, 0), 0.3f, true, false);
        preViewPanel.SetActive(true);
    }
    public void Submit()
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        StartCoroutine(ShowInterstitialAD());
        preViewPanel.SetActive(false);
        uIElements.scorePanel.SetActive(false);
        uIElements.aLLScrollers.SetActive(false);
        oppElements.oppoInfo.GetComponent<RectTransform>().DOAnchorPos(new Vector2(400f, -100f), 1f).SetEase(Ease.Linear);
        oppElements.myInfo.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-400f, -100f), 1f).SetEase(Ease.Linear);
        judgementPanel.SetActive(true);
        OpponentMover.gameObject.SetActive(true);
        OpponentMover.Move(new Vector3(250, -150, 0), 0.5f, true, false);
        OpponentMover.transform.localScale = new Vector3(-1, 1, 1);
        CharactorMover.Move(new Vector3(-250, -150, 0), 0.5f, true, false);
        CharactorMover.transform.localScale = new Vector3(1, 1, 1);
        DressUpOpponent();
        SaveData.instance.LevelsUnlocked++;
        StartCoroutine(StartComparing());

    }
    #endregion

    #region ObjectActivation
    IEnumerator ObjectActivation(GameObject activateObject, float _Delay, bool isTrue)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.SetActive(isTrue);
    }
    #endregion

    #region GetRewardedCoins
    public void GetRewardedCoins()
    {
        rewardType = RewardType.Coins;
        CheckVideoStatus();
    }
    public void NeedMoreCoins(bool Istrue)
    {
        if(Istrue == true)
        {
            if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
        }
        else if(Istrue == false)
        {
            if (AudioManager.Instance.PopOutSfx) AudioManager.Instance.PopOutSfx.Play();
        }
        uIElements.needMoreCoins.SetActive(Istrue);
    }
    #endregion

    #region IEnumerator
    IEnumerator AddCoins(float delay, int Coins)
    {
        yield return new WaitForSeconds(delay);
        if (coinsAdder)
        {
            coinsAdder.addCoins = Coins;
            coinsAdder.addNow = true;
        }
    }

    IEnumerator LoadingScene(string str)
    {
        loadingPanel.SetActive(true);
        fillBar.fillAmount = 0;
        while (fillBar.fillAmount < 1)
        {
            fillBar.fillAmount += Time.deltaTime / 4;
            yield return null;
        }
        SceneManager.LoadScene(str);
    }
    #endregion

    #region UnlockSingleItem
    public void UnlockSingleItem()
    {
        if (selectedItem == LehngaSelectedItem.Dress)
        {
            SaveData.Instance.lehngaProps.dressLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.Hair)
        {
            SaveData.Instance.lehngaProps.hairLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.LipStick)
        {
            SaveData.Instance.lehngaProps.lipStickLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.Eye)
        {
            SaveData.Instance.lehngaProps.eyesLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.Blush)
        {
            SaveData.Instance.lehngaProps.blushLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.NeckLace)
        {
            SaveData.Instance.lehngaProps.necklaceLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.Bag)
        {
            SaveData.Instance.lehngaProps.bagLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.Bracelet)
        {
            SaveData.Instance.lehngaProps.braceletLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.EyeBrows)
        {
            SaveData.Instance.lehngaProps.eyerowsLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.Earring)
        {
            SaveData.Instance.lehngaProps.earringLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.Shoes)
        {
            SaveData.Instance.lehngaProps.shoesLocked[selectedIndex] = false;
        }
        else if (selectedItem == LehngaSelectedItem.BG)
        {
            SaveData.Instance.lehngaProps.bGLocked[selectedIndex] = false;
        }
        Rai_SaveLoad.SaveProgress();
    }
    #endregion

    #region CheckVideoStatus
    public void CheckVideoStatus()
    {
        if (MyAdsManager.Instance != null)
        {
            if (MyAdsManager.Instance.IsRewardedAvailable())
            {
                MyAdsManager.Instance.ShowRewardedVideos();
            }
            else
            {
                if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
                uIElements.videoNotAvalible.SetActive(true);
                Invoke("videoPanelOf", 1.3f);
            }
        }
        else
        {
            if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
            uIElements.videoNotAvalible.SetActive(true);
            Invoke("videoPanelOf", 1.3f);
        }
    }
    #endregion

    #region RewardedVideoCompleted
    public void OnRewardedVideoComplete()
    {
        if (canShowInterstitial)
        {
            canShowInterstitial = !canShowInterstitial;
            StartCoroutine(AdDelay(adelay));
        }
        if (rewardType == RewardType.SelectionItem)
        {
            if (tempItem != null)  tempItem.isLocked = false;            
            UnlockSingleItem();
            SelectItem(selectedIndex);
        }
        else if (rewardType == RewardType.Coins)
        {
            uIElements.RewardCoinPanel.SetActive(true);
        }
        GetItemsInfo();
        uIElements.unlockPanel.SetActive(false);
        uIElements.needMoreCoins.SetActive(false);
        rewardType = RewardType.none;
        if (AudioManager.Instance.purchaseSFX) AudioManager.Instance.purchaseSFX.Play();
    }
    public void collectRewardedCoins()
    {
        uIElements.RewardCoinPanel.SetActive(false);
        StartCoroutine(AddCoins(0, 2000));
    }
    #endregion

    #region videoPanelOf
    public void videoPanelOf()
    {
        uIElements.videoNotAvalible.SetActive(false);
    }
    #endregion

    #region CoinUnlocks
    public void CoinUnlock()
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        if (selectedItem == LehngaSelectedItem.Dress)
        {
            CheckCoinUnlock(dressList);
        }
        else if (selectedItem == LehngaSelectedItem.Hair)
        {
            CheckCoinUnlock(hairList);
        }
        else if (selectedItem == LehngaSelectedItem.LipStick)
        {
            CheckCoinUnlock(lipstickList);
        }
        else if (selectedItem == LehngaSelectedItem.Eye)
        {
            CheckCoinUnlock(eyeList);
        }
        else if (selectedItem == LehngaSelectedItem.Blush)
        {
            CheckCoinUnlock(blushList);
        }
        else if (selectedItem == LehngaSelectedItem.NeckLace)
        {
            CheckCoinUnlock(necklaceList);
        }
        else if (selectedItem == LehngaSelectedItem.Bag)
        {
            CheckCoinUnlock(BagList);
        }
        else if (selectedItem == LehngaSelectedItem.Bracelet)
        {
            CheckCoinUnlock(BraceletList);
        }
        else if (selectedItem == LehngaSelectedItem.EyeBrows)
        {
            CheckCoinUnlock(EyeBrowsList);
        }
        else if (selectedItem == LehngaSelectedItem.Earring)
        {
            CheckCoinUnlock(EarringList);
        }
        else if (selectedItem == LehngaSelectedItem.Shoes)
        {
            CheckCoinUnlock(shoesList);
        }
        else if (selectedItem == LehngaSelectedItem.BG)
        {
            CheckCoinUnlock(bGList);
        }
    }
    public void CheckCoinUnlock(List<ItemInfo> itemInfoList)
    {
        if (itemInfoList[selectedIndex].coinsUnlock)
        {
            if (SaveData.Instance.Coins >= itemInfoList[selectedIndex].requiredCoins)
            {
                itemInfoList[selectedIndex].isLocked = false;
                SaveData.Instance.Coins -= itemInfoList[selectedIndex].requiredCoins;
                TotalCoins.text = SaveData.Instance.Coins.ToString();
                Rai_SaveLoad.SaveProgress();
                UnlockSingleItem();
                if (AudioManager.Instance.purchaseSFX) AudioManager.Instance.purchaseSFX.Play();
                uIElements.unlockPanel.SetActive(false);
                SelectItem(selectedIndex);
            }
            else
            {
                if (uIElements.needMoreCoins)
                    uIElements.needMoreCoins.SetActive(true);
                if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
            }
        }
    }
    #endregion

    #region ShowInterstitialAD
    private void CheckInterstitialAD()
    {
        if (MyAdsManager.Instance != null)
        {
            Debug.Log("ffff");
            if (MyAdsManager.Instance.IsInterstitialAvailable() && canShowInterstitial)
            {
                canShowInterstitial = !canShowInterstitial;
                StartCoroutine(AdDelay(adelay));
                StartCoroutine(ShowInterstitialAD());
            }
        }
    }
    IEnumerator ShowInterstitialAD()
    {
        if (MyAdsManager.Instance)
        {
            if (MyAdsManager.Instance.IsInterstitialAvailable())
            {
                if (uIElements.AdPenl)
                {
                    uIElements.AdPenl.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                    uIElements.AdPenl.SetActive(false);
                }
                MyAdsManager.Instance.ShowInterstitialAds();
            }
        }
    }
    IEnumerator AdDelay(float _Delay)
    {
        yield return new WaitForSeconds(_Delay);
        canShowInterstitial = !canShowInterstitial;
    }
    #endregion

    #region DressOpponent
    private void DressUpOpponent()
    {
        int randomIndex = 0;
        if (dressValue > 1)
        {
            randomIndex = Random.Range(0, dressList.Count);
            if (dressList[randomIndex] && oppElements.oppoDressImage)
            {
                oppElements.oppoDressImage.gameObject.SetActive(true);
                oppElements.oppoDressImage.sprite = dressSprites[randomIndex];
                oppoDressValue = int.Parse(dressList[randomIndex].scoreText.text);
            }
        }
        if (hairValue > 1)
        {
            randomIndex = Random.Range(0, hairList.Count);
            if (hairList[randomIndex] && oppElements.oppoHairImage)
            {
                oppElements.oppoHairImage.gameObject.SetActive(true);
                oppElements.oppoHairImage.sprite = hairSprites[randomIndex];
                oppoHairValue = int.Parse(hairList[randomIndex].scoreText.text);
            }
        }
        if (lipstickValue > 1)
        {
            randomIndex = Random.Range(0, lipstickList.Count);
            if (lipstickList[randomIndex] && oppElements.oppoLipstickImage)
            {
                oppElements.oppoLipstickImage.gameObject.SetActive(true);
                oppElements.oppoLipstickImage.sprite = lipStickSprites[randomIndex];
                oppoLipstickValue = int.Parse(lipstickList[randomIndex].scoreText.text);
            }
        }
        if (eyeValue > 1)
        {
            randomIndex = Random.Range(0, eyeList.Count);
            if (eyeList[randomIndex] && oppElements.oppoEyeImage)
            {
                oppElements.oppoEyeImage.gameObject.SetActive(true);
                oppElements.oppoEyeImage.sprite = eyeSprites[randomIndex];
                oppoEyeValue = int.Parse(eyeList[randomIndex].scoreText.text);
            }
        }
        if (blushValue > 1)
        {
            randomIndex = Random.Range(0, blushList.Count);
            if (blushList[randomIndex] && oppElements.oppoBlushImage)
            {
                oppElements.oppoBlushImage.gameObject.SetActive(true);
                oppElements.oppoBlushImage.sprite = blushSprites[randomIndex];
                oppoBlushValue = int.Parse(blushList[randomIndex].scoreText.text);
            }
        }
        if (neckLaceValue > 1)
        {
            randomIndex = Random.Range(0, necklaceList.Count);
            if (necklaceList[randomIndex] && oppElements.oppoNecklaceImage)
            {
                oppElements.oppoNecklaceImage.gameObject.SetActive(true);
                oppElements.oppoNecklaceImage.sprite = necklaceSprites[randomIndex];
                oppoNeckLaceValue = int.Parse(necklaceList[randomIndex].scoreText.text);
            }
        }
        if (BagValue > 1)
        {
            randomIndex = Random.Range(0, BagList.Count);
            if (BagList[randomIndex] && oppElements.oppoBagImage)
            {
                oppElements.oppoBagImage.gameObject.SetActive(true);
                oppElements.oppoBagImage.sprite = BagSprite[randomIndex];
                oppoBagValue = int.Parse(BagList[randomIndex].scoreText.text);
            }
        }
        if (BraceletValue > 1)
        {
            randomIndex = Random.Range(0, BraceletList.Count);
            if (BraceletList[randomIndex] && oppElements.oppoBraceletImage)
            {
                oppElements.oppoBraceletImage.gameObject.SetActive(true);
                oppElements.oppoBraceletImage.sprite = BraceletSprites[randomIndex];
                oppoBraceletValue = int.Parse(BraceletList[randomIndex].scoreText.text);
            }
        }
        if (EyeBrowsValue > 1)
        {
            randomIndex = Random.Range(0, EyeBrowsList.Count);
            if (EyeBrowsList[randomIndex] && oppElements.oppoEyeBrowsImage)
            {
                oppElements.oppoEyeBrowsImage.gameObject.SetActive(true);
                oppElements.oppoEyeBrowsImage.sprite = EyeBrowsSprires[randomIndex];
                oppoEyeBrowsValue = int.Parse(EyeBrowsList[randomIndex].scoreText.text);
            }
        }
        if (EarringValue > 1)
        {
            randomIndex = Random.Range(0, EarringList.Count);
            if (EarringList[randomIndex] && oppElements.oppoEarringImage)
            {
                oppElements.oppoEarringImage.gameObject.SetActive(true);
                oppElements.oppoEarringImage.sprite = EarringSprites[randomIndex];
                oppoEarringValue = int.Parse(EarringList[randomIndex].scoreText.text);
            }
        }
        if (shoesValue > 1)
        {
            randomIndex = Random.Range(0, shoesList.Count);
            if (shoesList[randomIndex] && oppElements.oppoShoeImage)
            {
                oppElements.oppoShoeImage.gameObject.SetActive(true);
                oppElements.oppoShoeImage.sprite = shoesSprites[randomIndex];
                oppoShoesValue = int.Parse(shoesList[randomIndex].scoreText.text);
            }
        }
    }
    #endregion

    #region Compare
    IEnumerator scoreImage(Image img, int Value)
    {
        img.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        img.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        img.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Value.ToString();
        particalmove();
        yield return new WaitForSeconds(1.1f);
        img.transform.GetChild(0).gameObject.SetActive(false);
        if(isplayerTurn == true)
        {
            oppElements.playerTotal.text = playerTotal.ToString();
            oppElements.playerTotal.transform.GetChild(0).GetComponent<ParticleSystem>().Play();

        }
        else
        {
            oppElements.oppoTotal.text = opTotal.ToString();
            oppElements.oppoTotal.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
        if (AudioManager.Instance.CollectSfx) AudioManager.Instance.CollectSfx.Play();
    }
    int playerTotal = 0, opTotal = 0;
    bool isplayerTurn = false;
    int soundindex = 0;
    public void cheerUp()
    {
        if (AudioManager.Instance)
        {
            if (AudioManager.Instance.chearSfx[soundindex]) AudioManager.Instance.chearSfx[soundindex].Play();
            soundindex++;
            if (soundindex >= AudioManager.Instance.chearSfx.Length) soundindex = 0;
        }
    }
    int particalindex = 0;
    public void particalmove()
    {
        if (AudioManager.Instance)
        {
            if (AudioManager.Instance.ParticalMoveSfx[particalindex]) AudioManager.Instance.ParticalMoveSfx[particalindex].Play();
            particalindex++;
            if (particalindex >= AudioManager.Instance.chearSfx.Length) particalindex = 0;
        }
    }
    IEnumerator StartComparing()
    {
        float delay = 0.5f;
        yield return new WaitForSeconds(3f);
        CharactorMover.transform.SetSiblingIndex(-1);
        CharactorMover.Move(new Vector3(0, -150, 0), 0.5f, true, false);
        OpponentMover.ScaleTo(new Vector3(-0.7f, 0.7f, 0.7f), 0.5f, true, false);
        isplayerTurn = true;
        judgementPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (dressValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.dressImage,dressValue));
        }
        playerTotal += dressValue + bGValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (shoesValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.shoeImage,shoesValue));
        }
        playerTotal += shoesValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (EyeBrowsValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.EyeBrowsImage, EyeBrowsValue));
        }
        playerTotal += EyeBrowsValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (BagValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.BagImage,BagValue));
        }
        playerTotal += BagValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (eyeValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.eyeImage,eyeValue));
        }
        playerTotal += eyeValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (lipstickValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.lipstickImage,lipstickValue));
        }
        playerTotal += lipstickValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (hairValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.hairImage,hairValue));
        }
        playerTotal += hairValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (blushValue> 1) StartCoroutine(scoreImage(uIElements.blushImage,blushValue));
        playerTotal += blushValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (EarringValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.EarringImage, EarringValue));
        }
        playerTotal += EarringValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (neckLaceValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.necklaceImage,neckLaceValue));
        }
        playerTotal += neckLaceValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (BraceletValue > 1)
        {
            StartCoroutine(scoreImage(uIElements.BraceletImage,BraceletValue));
        }
        playerTotal += BraceletValue;
        yield return new WaitForSeconds(1);
        //opponent
        CharactorMover.Move(new Vector3(-250, -150, 0), 0.5f, true, false);
        CharactorMover.ScaleTo(new Vector3(0.7f, 0.7f, 0.7f), 0.5f, true, false);
        OpponentMover.transform.SetSiblingIndex(-1);
        OpponentMover.Move(new Vector3(0, -150, 0), 0.5f, true, false);
        OpponentMover.ScaleTo(new Vector3(-1f, 1f, 1f),0.5f,true,false);


        yield return new WaitForSeconds(1);
        isplayerTurn = false;
        if (oppoDressValue > 1) StartCoroutine(scoreImage(oppElements.oppoDressImage,oppoDressValue));
        opTotal += oppoDressValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoShoesValue > 1) StartCoroutine(scoreImage(oppElements.oppoShoeImage,oppoShoesValue));
        opTotal += oppoShoesValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoEyeBrowsValue > 1) StartCoroutine(scoreImage(oppElements.oppoEyeBrowsImage,oppoEyeBrowsValue));
        opTotal += oppoEyeBrowsValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoBagValue > 1) StartCoroutine(scoreImage(oppElements.oppoBagImage,oppoBagValue));
        opTotal += oppoBagValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoEyeValue > 1) StartCoroutine(scoreImage(oppElements.oppoEyeImage,oppoEyeValue));
        opTotal += oppoEyeValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoLipstickValue > 1) StartCoroutine(scoreImage(oppElements.oppoLipstickImage,oppoLipstickValue));
        opTotal += oppoLipstickValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoHairValue > 1) StartCoroutine(scoreImage(oppElements.oppoHairImage,oppoHairValue));
        opTotal += oppoHairValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoBlushValue > 1) StartCoroutine(scoreImage(oppElements.oppoBlushImage,oppoBlushValue));
        opTotal += oppoBlushValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoEarringValue > 1) StartCoroutine(scoreImage(oppElements.oppoEarringImage,oppoEarringValue));
        opTotal += oppoEarringValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoNeckLaceValue > 1) StartCoroutine(scoreImage(oppElements.oppoNecklaceImage,oppoNeckLaceValue));
        opTotal += oppoNeckLaceValue;
        yield return new WaitForSeconds(delay);
        cheerUp();
        if (oppoBraceletValue > 1) StartCoroutine(scoreImage(oppElements.oppoBraceletImage,oppoBraceletValue));
        opTotal += oppoBraceletValue;
        yield return new WaitForSeconds(2);
        judgementPanel.SetActive(false);
        uIElements.CoinSloat.SetActive(true);
        uIElements.CoinSloat.GetComponent<Button>().interactable = false;
        uIElements.CoinSloat.transform.GetChild(2).gameObject.SetActive(false);
        if (playerTotal >= opTotal)
        {
            //player win
            if (AudioManager.Instance.CelebrateSfx) AudioManager.Instance.CelebrateSfx.Play();
            yield return new WaitForSeconds(1f);
            CharactorMover.transform.SetSiblingIndex(-1);
            yield return new WaitForSeconds(1f);
            OpponentMover.Move(new Vector3(1500, -150, 0), 0.5f, true, false);
            yield return new WaitForSeconds(0.3f);
            CharactorMover.ScaleTo(new Vector3(1, 1, 1), 0.5f, true, false);
            CharactorMover.Move(new Vector3(0, -150, 0), 0.5f, true, false);
            yield return new WaitForSeconds(1f);
            Confetti.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            uIElements.levelWin.SetActive(true);
            StartCoroutine(AddCoins(0f, 2000));
        }
        else
        {
            if (AudioManager.Instance.LoseSfx) AudioManager.Instance.LoseSfx.Play();
            yield return new WaitForSeconds(1f);
            OpponentMover.transform.SetSiblingIndex(-1);
            yield return new WaitForSeconds(1f);
            CharactorMover.Move(new Vector3(-1500, -150, 0), 0.5f, true, false);
            yield return new WaitForSeconds(0.3f);
            OpponentMover.ScaleTo(new Vector3(-1, 1, 1), 0.5f, true, false);
            OpponentMover.Move(new Vector3(0, -150, 0), 0.5f, true, false);
            yield return new WaitForSeconds(1f);
            StartCoroutine(AddCoins(0f, 5000));
            uIElements.levelFailed.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        uIElements.levelComplete.SetActive(true);
        uIElements.CoinSloat.SetActive(false);
    }
    #endregion

    #region Share ScreenShot
    public void Sharing()
    {
        if (AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
        StartCoroutine(TakeScreenShotAndShare());
    }
    IEnumerator TakeScreenShotAndShare()
    {
        yield return new WaitForEndOfFrame();
        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();
        string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");//image name
        File.WriteAllBytes(path, tx.EncodeToPNG());
        Destroy(tx); //to avoid memory leaks
        new NativeShare()
            .AddFile(path)
            //.SetSubject("This is my score")
            //.SetText("share your score with your friends")
            .Share();
    }
    #endregion

    #region TakeScreenShot
    public void TakeScreenShot()
    {
        if (AudioManager.Instance) AudioManager.Instance.CaptureSfx.Play();
        uIElements.screenShotImg.transform.parent.localScale = Vector3.one;
        preViewPanel.SetActive(false);
        StartCoroutine(TakeScreenShotNow());
    }
    Texture2D _Taxture;
    IEnumerator TakeScreenShotNow()
    {
        yield return new WaitForEndOfFrame();
        Texture2D _Texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        _Texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _Texture.Apply();
        _Texture.LoadImage(_Texture.EncodeToPNG());
        Sprite sprite = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), new Vector2(_Texture.width / 2, _Texture.height / 2));
        if (uIElements.screenShotImg)
        {
            uIElements.screenShotImg.sprite = sprite;
            uIElements.SS_Panel.SetActive(true);
        }
        _Taxture = _Texture;
        Invoke("DownloadImage", 0.8f);
    }
    public void DownloadImage()
    {
        string picturName = "ScreenShot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        NativeGallery.SaveImageToGallery(_Taxture, "My Pictures", picturName);
        Invoke("PictureSaved", 0.8f);
    }
    private void PictureSaved()
    {
        uIElements.SS_Panel.SetActive(false);
        preViewPanel.SetActive(true);
        Destroy(_Taxture);
    }
    #endregion

    #region SpecialItem
    public void CloseRewardCollectPanel(bool IsTrue)
    {
        if(IsTrue == true)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopinSfx.Play();
        }
        else if(IsTrue == false)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopOutSfx.Play();
        }
        uIElements.RewardCollectPanel.SetActive(IsTrue);
    }
    public void CloseUnlockPanel(bool IsTrue)
    {
        if(IsTrue == true)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopinSfx.Play();
        }
        else if(IsTrue == false)
        {
            if (AudioManager.Instance) AudioManager.Instance.PopOutSfx.Play();
        }
        uIElements.unlockPanel.SetActive(IsTrue);
    }
    #endregion
}

