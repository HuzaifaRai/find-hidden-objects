using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Button itemBtn;
    public Image itemIcon;
    public Text scoreText;
    public GameObject TickIcon;
    public GameObject LockIcon;
    public GameObject coinSlot;
    public GameObject VideoSlot;
    public Text unlockCoins;
    public bool isLocked;
    public bool videoUnlock;
    public bool coinsUnlock;
    [Range(0, 50000)]
    public int requiredCoins;
}
