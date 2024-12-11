using UnityEngine;
using System.Collections;

public class GameManager
{

	private static GameManager instance;

	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameManager ();
			}
			return instance;
		}
	}

	public bool canShowAds = true;
	public bool canShowFirstOpenAd = false;
	public bool Initialized = false;
	public string modeName;
    public int ModeIndex = 0;
    public int dailyRewardCoins;


}