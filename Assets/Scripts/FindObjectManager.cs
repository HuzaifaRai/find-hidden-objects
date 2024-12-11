using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FindObjectManager : MonoBehaviour
{
    private static FindObjectManager instance;
    public static FindObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FindObjectManager();
            }
            return instance;
        }
    }

    public GameObject[] itemsPlaced;
    public GameObject[] itemsToFind;
    public Text TimeLeft;
    public Text timeMinus;
    public GameObject levelCompletePanel;
    public GameObject timeEndPanel;
    int itemsCounter;
    public float timerDuration = 120;
    [HideInInspector]
    public float timeRemaining;

    public GameObject adPanel;
    public GameObject videoNotAvalible;
    public GameObject loadingPanel;
    public Image fillBar;
    public Image fillImage;
    [Range(4, 10)]
    public float TimeToLoad;

    public GameObject tempParent;
    public GameObject ViewPort;
    public GameObject Content;
    public GameObject BG;
    public Button hintButton;
    private enum RewardType
    {
        none, Coins, Hint, AddTime
    }

    private RewardType rewardType;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        hintButton.onClick.AddListener(() =>
        {
            HintOnAdd();
            //StartCoroutine(HintObj()); 
        });

        for (int i = 0; i < itemsPlaced.Length; i++)
        {
            int Index = i;
            if (itemsPlaced[i])
            {
                itemsPlaced[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    StartCoroutine(FoundObject(Index));
                });
            }
        }

        itemsCounter = itemsPlaced.Length;
        timeRemaining = timerDuration;
        UpdateTimerDisplay();
        levelCompletePanel.SetActive(false);
        timeEndPanel.SetActive(false);
        StartCoroutine(Timmer());
        // Initialize objectsFound with the same length as itemsPlaced
        objectsFound = new bool[itemsPlaced.Length];

        // By default, all objects are not found
        for (int i = 0; i < itemsPlaced.Length; i++)
        {
            objectsFound[i] = false;
        }
    }

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

    public void Home()
    {
        if (AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
        loadingPanel.gameObject.SetActive(true);
        showinterstitial();
        StartCoroutine(Loading("MyMainMenu"));
        loadingPanel.gameObject.SetActive(true);
    }
    public void BackBtn()
    {
        if (AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
        loadingPanel.gameObject.SetActive(true);
        showinterstitial();
        StartCoroutine(Loading("LevelSelection"));
        loadingPanel.gameObject.SetActive(true);
    }

    public void Next(string str)
    {
        if (AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
        loadingPanel.gameObject.SetActive(true);
        showinterstitial();
        StartCoroutine(Loading(str));
    }

    public void Retry(string str)
    {
        if (AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
        loadingPanel.gameObject.SetActive(true);
        showinterstitial();
        StartCoroutine(Loading(str));
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

    float initialposx;
    float initialposy;
    int currentHintIndex = 0;
    bool[] objectsFound;
    IEnumerator HintObj()
    {
        // Ensure we are within bounds
        while (currentHintIndex < itemsPlaced.Length)
        {
            GameObject currentObject = itemsPlaced[currentHintIndex];

            // Check if the current object is valid
            if (currentObject != null)
            {
                if (!objectsFound[currentHintIndex])
                {

                    float initialPosX = currentObject.transform.localPosition.x;
                    float initialPosY = currentObject.transform.localPosition.y;

                    currentObject.transform.parent = tempParent.transform;
                    yield return new WaitForSeconds(0.1f);

                    Content.transform.parent = currentObject.transform;
                    yield return new WaitForSeconds(0.1f);

                    currentObject.GetComponent<MRS_Manager>().MoveTo(new Vector3(0, initialPosY, 0), 0.5f);
                    yield return new WaitForSeconds(0.5f);

                    Content.transform.parent = ViewPort.transform.parent;
                    yield return new WaitForSeconds(0.1f);

                    currentObject.transform.parent = BG.transform;
                    currentObject.transform.localPosition = new Vector3(initialPosX, initialPosY);

                    if (currentObject.transform.childCount > 1)
                    {
                        currentObject.transform.GetChild(1).gameObject.SetActive(true);
                        yield return new WaitForSeconds(2f);
                        currentObject.transform.GetChild(1).gameObject.SetActive(false);
                    }

                    objectsFound[currentHintIndex] = true;

                    yield break; // Stop the coroutine after processing one object
                }
                else
                {
                    Debug.Log($"Object at index {currentHintIndex} is already found. Moving to the next one.");
                    currentHintIndex++;
                }
            }
            else
            {
                Debug.LogWarning($"Object at index {currentHintIndex} is null or destroyed. Skipping.");
                currentHintIndex++;
            }
        }

        Debug.Log("All objects have been hinted.");
    }

    public void HintOnAdd()
    {
        rewardType = RewardType.Hint;
        CheckVideoStatus();
    }

    public void MarkObjectAsFound(int index)
    {
        if (index >= 0 && index < objectsFound.Length)
        {
            objectsFound[index] = true;
            Debug.Log($"Object at index {index} marked as found.");
        }
    }

    //IEnumerator HintObj()
    //{
    //    if (itemsPlaced[leftItemIndex] != null )
    //    {
    //        initialposx = itemsPlaced[leftItemIndex].transform.localPosition.x;
    //        initialposy = itemsPlaced[leftItemIndex].transform.localPosition.y;
    //        itemsPlaced[leftItemIndex].transform.parent = tempParent.transform;
    //        yield return new WaitForSeconds(0.1f);
    //        Content.transform.parent = itemsPlaced[leftItemIndex].transform;
    //        yield return new WaitForSeconds(0.1f);
    //        itemsPlaced[leftItemIndex].GetComponent<MRS_Manager>().MoveTo(new Vector3(0, initialposy, 0), 0.5f);
    //        yield return new WaitForSeconds(0.5f);
    //        Content.transform.parent = ViewPort.transform.parent;
    //        yield return new WaitForSeconds(0.1f);
    //        itemsPlaced[leftItemIndex].transform.parent = BG.transform;
    //        itemsPlaced[leftItemIndex].transform.GetChild(1).gameObject.SetActive(true);
    //        itemsPlaced[leftItemIndex].transform.localPosition = new Vector3(initialposx,initialposy);
    //    }
    //}

    IEnumerator FoundObject(int index)
    {
        // Check if the object has already been processed
        if (itemsPlaced[index] == null || itemsPlaced[index].GetComponent<Button>() == null)
        {
            yield break; // Exit if the object is already handled or destroyed
        }

        // Disable the button to prevent further clicks
        itemsPlaced[index].GetComponent<Button>().interactable = false;

        itemsCounter--;
        itemsPlaced[index].transform.parent = itemsToFind[index].transform;

        if (itemsPlaced[index].transform.GetChild(0))
        {
            itemsPlaced[index].transform.GetChild(0).gameObject.SetActive(true);
        }

        // Animate the found object
        itemsPlaced[index].gameObject.GetComponent<MRS_Manager>().ScaleTo(new Vector3(1.5f, 1.5f, 1.5f), 0.5f, true, false);
        yield return new WaitForSeconds(0.4f);
        itemsPlaced[index].gameObject.GetComponent<MRS_Manager>().MoveTo(new Vector3(0, 0, 0), 1f, true, false);
        yield return new WaitForSeconds(1f);

        Destroy(itemsToFind[index]); // Destroy the associated object
        itemsPlaced[index] = null;  // Mark the object as processed

        // Check if all objects are found
        if (itemsCounter < 1)
        {
            print("Level Complete");

            // Unlock the next level if applicable
            int nextModeIndex = GameManager.Instance.ModeIndex + 1;

            if (nextModeIndex < SaveData.Instance.modeProps.ModeLocked.Count) // Ensure within bounds
            {
                if (SaveData.Instance.modeProps.ModeLocked[nextModeIndex]) // Unlock only if locked
                {
                    SaveData.Instance.modeProps.ModeLocked[nextModeIndex] = false;
                    Rai_SaveLoad.SaveProgress();
                }
            }

            yield return new WaitForSeconds(0.25f);
            if (AudioManager.Instance.CelebrateSfx) AudioManager.Instance.CelebrateSfx.Play();

            levelCompletePanel.SetActive(true); // Show Level Complete Panel
            StopCoroutine(Timmer()); // Stop the timer
        }
    }

    // Rest of your original methods (e.g., TimeMinus, Timer, etc.) go here...

    IEnumerator Timmer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
            if (timeRemaining >= 0)
                UpdateTimerDisplay();
        }

        print("Time End");
        timeEndPanel.SetActive(true); // Show Time's Up Panel
        StopCoroutine(Timmer()); // Stop further updates
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        TimeLeft.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimeAdd()
    {
        if (AudioManager.Instance.BtnSfx) AudioManager.Instance.BtnSfx.Play();
        timeRemaining += 60; // Add 1 minute and 30 seconds
        timeEndPanel.SetActive(false); // Hide Time's Up Panel
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
            //StartCoroutine(AddCoins(2000, 0.1f));
            Rai_SaveLoad.SaveProgress();
        }
        if (rewardType == RewardType.Hint)
        {
            StartCoroutine(HintObj());
        }
        else if (rewardType == RewardType.AddTime)
        {
            TimeAdd();
            StartCoroutine(Timmer());
            timeEndPanel.SetActive(false);
        }
        //GetItemsInfo();
        //NeedMoreCoinsPanel.SetActive(false);
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

    //IEnumerator AddCoins(int Coins, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if (coinsAdder)
    //    {
    //        coinsAdder.addCoins = Coins;
    //        coinsAdder.addNow = true;
    //    }
    //}
}
