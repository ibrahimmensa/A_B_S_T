using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    public Button loadBtn;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("havesafefile", 0) == 1)
        {
            loadBtn.interactable = true;
        }
        else
        {
            loadBtn.interactable = false;
        }
    }

    public void onClickPlay()
    {
        GamePlayManager.Instance.isLoadingSavedLevel = false;
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        MenuManager.Instance.onSwitchMenu(Menus.GAMEPLAY);
        GamePlayManager.Instance.OnClickPlay();
    }

    public void onClickSettings()
    {
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        MenuManager.Instance.onSwitchMenu(Menus.SETTINGS);
    }

    public void onClickLoadBtn()
    {
        GamePlayManager.Instance.isLoadingSavedLevel = true;
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        MenuManager.Instance.onSwitchMenu(Menus.GAMEPLAY);
        GamePlayManager.Instance.OnClickPlay();
    }
}
