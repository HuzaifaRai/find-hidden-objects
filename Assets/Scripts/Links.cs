using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Links : MonoBehaviour
{

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Innovative+Games+Studio&hl=en_UShttps://play.google.com/store/apps/developer?id=Innovative+Games+Studio&hl=en_US");
        if(AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
    }
    public void RateUS()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.innovative.dolldressup.makeover.games");
        if(AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
    }
    public void PP()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Innovative+Games+Studio&hl=en_US");
        if(AudioManager.Instance) AudioManager.Instance.BtnSfx.Play();
    }

}
