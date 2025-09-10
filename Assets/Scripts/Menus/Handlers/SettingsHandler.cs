using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    public Toggle music_Toggle, SFX_Toggle;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("music", 1) == 0)
        {
            music_Toggle.isOn = false;
        }
        else
        {
            music_Toggle.isOn = true;
        }
        onToggleMusic();
        if (PlayerPrefs.GetInt("sfx", 1) == 0)
        {
            SFX_Toggle.isOn = false;
        }
        else
        {
            SFX_Toggle.isOn = true;
        }
        onToggleSFX();
    }
    public void onClickClose()
    {
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        MenuManager.Instance.onSwitchMenu(Menus.MAINMENU);
    }

    public void onToggleMusic()
    {
        if (music_Toggle.isOn)
        {
            SoundManager.Instance.BGMusic.gameObject.SetActive(true);
            SoundManager.Instance.BGMusic.Play();
            PlayerPrefs.SetInt("music", 1);
        }
        else
        {
            SoundManager.Instance.BGMusic.Stop();
            SoundManager.Instance.BGMusic.gameObject.SetActive(false);
            PlayerPrefs.SetInt("music", 0);
        }
    }

    public void onToggleSFX()
    { 
        if (SFX_Toggle.isOn)
        {
            SoundManager.Instance.SFX.gameObject.SetActive(true);
            PlayerPrefs.SetInt("sfx", 1);
        }
        else
        {
            SoundManager.Instance.SFX.Stop();
            PlayerPrefs.SetInt("sfx", 0);
        }
    }
}
