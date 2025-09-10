using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenHandler : MonoBehaviour
{
    public TMP_Text scoreText;
    public void onClickNextRound()
    {
        MenuManager.Instance.startNextLevel();
    }

    public void onClickMainMenu()
    {
        MenuManager.Instance.onSwitchMenu(Menus.MAINMENU);
    }
}
