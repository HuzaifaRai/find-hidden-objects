using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance;
        }
    }

    public AudioSource BGM;
    public AudioSource itemSelectSFX;
    public AudioSource CategorySelectSFX;
    public AudioSource purchaseSFX;
    public AudioSource AppearSfx;
    public AudioSource BtnSfx;
    public AudioSource PopinSfx;
    public AudioSource PopOutSfx;
    public AudioSource CoinSfx;
    public AudioSource CelebrateSfx;
    public AudioSource taskkSfx;
    public AudioSource fireworkSfx;
    public AudioSource LoseSfx;
    public AudioSource IDChangdSfx;
    public AudioSource IconChangeSfx;
    public AudioSource CollectSfx;
    public AudioSource VsSfx;
    public AudioSource CaptureSfx;
    public AudioSource[] ParticalMoveSfx;
    public AudioSource[] chearSfx;

    #region Awake
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.Instance.Initialized)
        {
            Rai_SaveLoad.LoadProgress();
            GameManager.Instance.Initialized = true;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void Music(float value)
    {
        if (BtnSfx) BtnSfx.Play();
        if(BGM)BGM.volume = value;
    }
    public void Sound(float value)
    {
        if (BtnSfx) BtnSfx.Play();
        if (itemSelectSFX) itemSelectSFX.volume = value;
        if (CategorySelectSFX) CategorySelectSFX.volume = value;
        if (purchaseSFX) purchaseSFX.volume = value;
        if (AppearSfx) AppearSfx.volume = value;
        if (BtnSfx) BtnSfx.volume = value;
        if (taskkSfx) taskkSfx.volume = value;
        if (PopinSfx) PopinSfx.volume = value;
        if (PopOutSfx) PopOutSfx.volume = value;
        if (CoinSfx) CoinSfx.volume = value;
        if (CelebrateSfx) CelebrateSfx.volume = value;
        if (fireworkSfx) fireworkSfx.volume = value;
        if (LoseSfx) LoseSfx.volume = value;
        if (IDChangdSfx) IDChangdSfx.volume = value;
        if (IconChangeSfx) IconChangeSfx.volume = value;
        if (CollectSfx) CollectSfx.volume = value;
        if (CaptureSfx) CaptureSfx.volume = value;
        if (VsSfx) VsSfx.volume = value;
        for (int i = 0; i < chearSfx.Length; i++)
        {
            if (chearSfx[i]) chearSfx[i].volume = value;
        }
        for (int i = 0; i < ParticalMoveSfx.Length; i++)
        {
            if (ParticalMoveSfx[i]) ParticalMoveSfx[1].volume = value;
        }
    }
   
}
