using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    private static LevelsManager instance;
    public static LevelsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelsManager();
            }
            return instance;
        }
    }
    int screwIndex;
    [Header("Arrays")]
    public MRS_Manager[] screw;
    public Transform[] Holes;
    public RectTransform[] Sticks;
    [Header("GameObject")]
    public GameObject NeedMoreCoinsPanel;
    public GameObject videoNotAvalible;
    public GameObject LevelCompelete;
    public GameObject adPanel;
    public GameObject TimeEndPenal;
    [Header("Mix")]
    public GameObject LoadingPenal;
    public Image fillbar;
    public CoinsAdder coinsAdder;
    public Text Minttext;
    public Text Sectext;
    public int stikdropIndex = -1;
    bool IsScrewUp = false;
    public int GivenMints = 2;
    public int GivenSec = 60;

    private enum RewardType
    {
        none, Coins, AddTime
    }

    private RewardType rewardType;

    #region Awake
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    #region Start
    private void Start()
    {
        for (int i = 0; i < screw.Length; i++)
        {
            int Index = i;
            if (screw[i])
            {
                screw[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    ScrewOut(Index);
                });
            }
        }
        for (int i = 0; i < Holes.Length; i++)
        {
            int Index = i;
            if (Holes[i])
            {
                Holes[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    ScrewDownx(Index);
                });
            }
        }
        StartCoroutine(Timmer());
    }
    #endregion

    #region OnEnableDisable
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

    public void ScrewOut(int index)
    {
        if (IsScrewUp == false)
        {
            //if (AudioManager.Instance.ScrewSfx) AudioManager.Instance.ScrewSfx.Play();
            screw[index].GetComponent<Animator>().Play("ScrewOut"); 
            screwIndex = index;
            IsScrewUp = true;
        }
        else
        {
            if (index == screwIndex)
            {
                ScrewDownx();
                IsScrewUp = false;
            }
        }
    }

    public void ScrewDownx(int index)
    {
        if (IsScrewUp)
        {
            screw[screwIndex].transform.parent = Holes[index];
            screw[screwIndex].gameObject.GetComponent<MRS_Manager>().MoveTo(new Vector3(0, 0, 0), 0.3f, true, false); ;
            screw[screwIndex].gameObject.GetComponent<CircleCollider2D>().enabled = false;
            IsScrewUp = false;
            Invoke("ScrewDownx",0.25f);
        }
    }

    public void ScrewDownx()
    {
        screw[screwIndex].gameObject.GetComponent<CircleCollider2D>().enabled = true; ;
        //if (AudioManager.Instance.ScrewSfx) AudioManager.Instance.ScrewSfx.Play();
        screw[screwIndex].GetComponent<Animator>().Play("ScrewTight");
    }

    public void Check()
    {
        if(stikdropIndex >= Sticks.Length)
        {
            if (AudioManager.Instance.CelebrateSfx) AudioManager.Instance.CelebrateSfx.Play();
            LevelCompelete.SetActive(true);
            StopAllCoroutines();
        }
    }

    public void Play(string str)
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        LoadingPenal.SetActive(true);
        showinterstitial();
        StartCoroutine(Loading(str));

    }

    IEnumerator Loading(string str)
    {
        fillbar.fillAmount = 0;
        while (fillbar.fillAmount < 1)
        {

            fillbar.fillAmount += Time.deltaTime / 4.5f;
            yield return null;
        }
        SceneManager.LoadScene(str);

    }

    IEnumerator Timmer()
    {
        while (0 < GivenMints)
        {
            GivenMints--;
            Minttext.text = GivenMints.ToString() + ":";
            while (0 < GivenSec)
            {
                yield return new WaitForSeconds(1f);
                GivenSec--;
                Sectext.text = GivenSec.ToString();
            }
            GivenSec = 60;
            Sectext.text = GivenSec.ToString();
        }
        TimeEndPenal.SetActive(true);
        //f (AudioManager.Instance.LoseSFX) AudioManager.Instance.LoseSFX.Play();

    }

    public void Timeadd()
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        GivenMints = 1;
        GivenSec = 30;
        TimeEndPenal.SetActive(false);
        StartCoroutine(Timmer());
    }

    public void showinterstitial()
    {
        StartCoroutine(loadAd());
    }

    IEnumerator loadAd()
    {
        if (adPanel) adPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (adPanel) adPanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        if (MyAdsManager.Instance)
        {
            MyAdsManager.Instance.ShowInterstitialAds();
        }
    }

    public void needmorepanel(bool Istrue)
    {
        if (Istrue == true)
        {
            if (AudioManager.Instance.PopinSfx) AudioManager.Instance.PopinSfx.Play();
        }
        else
        {
            if (AudioManager.Instance.PopOutSfx) AudioManager.Instance.PopOutSfx.Play();
        }
        NeedMoreCoinsPanel.SetActive(Istrue);
        rewardType = RewardType.Coins;
    }
    public void WatchadForTime()
    {
        rewardType = RewardType.AddTime;
        CheckVideoStatus();

    }

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

    #region RewardedVideoCompleted
    public void OnRewardedVideoComplete()
    {
        if (rewardType == RewardType.Coins)
        {
            StartCoroutine(AddCoins(2000, 0.1f));
            Rai_SaveLoad.SaveProgress();
        }
        else if (rewardType == RewardType.AddTime)
        {
            Timeadd();
            TimeEndPenal.SetActive(false);
        }
        NeedMoreCoinsPanel.SetActive(false);
        rewardType = RewardType.none;
        if (AudioManager.Instance.purchaseSFX) AudioManager.Instance.purchaseSFX.Play();
    }
    #endregion

    #region videoPanelOf
    public void videoPanelOf()
    {
        videoNotAvalible.SetActive(false);
    }
    #endregion

    IEnumerator AddCoins(int Coins, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (coinsAdder)
        {
            coinsAdder.addCoins = Coins;
            coinsAdder.addNow = true;
        }
    }
}
