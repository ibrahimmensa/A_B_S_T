using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenHandler : MonoBehaviour
{
    public TMP_Text scoreText;
    public void onClickNextRound()
    {
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        MenuManager.Instance.startNextLevel();
    }

    public void onClickMainMenu()
    {
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        MenuManager.Instance.onSwitchMenu(Menus.MAINMENU);
    }
}
