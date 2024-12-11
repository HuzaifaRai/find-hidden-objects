using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProps
{
    public string playerName;
    public int playerHealth;
    public int playerDamage;
    public int playerRange;
    public bool isLocked = true;
}
[System.Serializable]
public class SareeProps
{
    public List<bool> dressLocked = new List<bool>();
    public List<bool> hairLocked = new List<bool>();
    public List<bool> blushLocked = new List<bool>();
    public List<bool> eyesLocked = new List<bool>();
    public List<bool> lipStickLocked = new List<bool>();
    public List<bool> necklaceLocked = new List<bool>();
    public List<bool> eyerowsLocked = new List<bool>();
    public List<bool> braceletLocked = new List<bool>();
    public List<bool> earringLocked = new List<bool>();
    public List<bool> bagLocked = new List<bool>();
    public List<bool> shoesLocked = new List<bool>();
    public List<bool> bGLocked = new List<bool>();
}
[System.Serializable]
public class LehngaProps
{
    public List<bool> dressLocked = new List<bool>();
    public List<bool> hairLocked = new List<bool>();
    public List<bool> blushLocked = new List<bool>();
    public List<bool> eyesLocked = new List<bool>();
    public List<bool> lipStickLocked = new List<bool>();
    public List<bool> necklaceLocked = new List<bool>();
    public List<bool> eyerowsLocked = new List<bool>();
    public List<bool> braceletLocked = new List<bool>();
    public List<bool> earringLocked = new List<bool>();
    public List<bool> bagLocked = new List<bool>();
    public List<bool> shoesLocked = new List<bool>();
    public List<bool> bGLocked = new List<bool>();
}
[System.Serializable]
public class CasualProps
{
    public List<bool> dressLocked = new List<bool>();
    public List<bool> hairLocked = new List<bool>();
    public List<bool> blushLocked = new List<bool>();
    public List<bool> eyesLocked = new List<bool>();
    public List<bool> lipStickLocked = new List<bool>();
    public List<bool> necklaceLocked = new List<bool>();
    public List<bool> eyerowsLocked = new List<bool>();
    public List<bool> braceletLocked = new List<bool>();
    public List<bool> earringLocked = new List<bool>();
    public List<bool> bagLocked = new List<bool>();
    public List<bool> shoesLocked = new List<bool>();
    public List<bool> bGLocked = new List<bool>();
}
[System.Serializable]
public class ModeProps
{
    public List<bool> ModeLocked = new List<bool>();
    public List<bool> LevelCompleted = new List<bool>();
}


[System.Serializable]
public class SaveData
{

    public static SaveData instance;
    public static SaveData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveData();
            }
            return instance;
        }
    }
    public bool RemoveAds = false;
    public int LevelsUnlocked = 1;
    public int EventsUnlocked = 0;
    public int SelectedPlayerAvatar = 0;
    public int OpponentIndex;
    public string ProfileName = "Player";
    public string OpponentName = "Opoounent";
    public bool ProfileCreated = false;
    public bool isSound = true, isMusic = true, isVibration = true, isRightControls = true;
    public int Coins = 0;
    public List<PlayerProps> Players = new List<PlayerProps>();
    public ModeProps modeProps = new ModeProps();
    public SareeProps sareeProps = new SareeProps();
    public LehngaProps lehngaProps = new LehngaProps();
    public CasualProps casualProps = new CasualProps();
    public string hashOfSaveData;

    //Constructor to save actual GameData
    public SaveData() { }

    //Constructor to check any tampering with the SaveData
    public SaveData(bool ads, int levelsUnlocked, int eventsUnlocked, int coins, bool soundOn, bool musicOn, bool vibrationOn, bool rightControls, List<PlayerProps> _players,
        ModeProps _modeProps, SareeProps _sareeProps, LehngaProps _lehngaProps, CasualProps _casualProps)
    {
        RemoveAds = ads;
        LevelsUnlocked = levelsUnlocked;
        EventsUnlocked = eventsUnlocked;
        Coins = coins;
        isSound = soundOn;
        isMusic = musicOn;
        isVibration = vibrationOn;
        isRightControls = rightControls;
        Players = _players;
        modeProps = _modeProps;
        sareeProps = _sareeProps;
        lehngaProps = _lehngaProps;
        casualProps = _casualProps;
    }
}