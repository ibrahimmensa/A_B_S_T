using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickMainMenu()
    {
        GamePlayManager.Instance.currentGrid.resetGrid();
        GamePlayManager.Instance.currentGrid.gameObject.SetActive(false);
        MenuManager.Instance.onSwitchMenu(Menus.MAINMENU);
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        this.gameObject.SetActive(false);
    }

    public void onClickReturn()
    {
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        this.gameObject.SetActive(false);
    }

    public void onClickSave()
    {
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        GamePlayManager.Instance.onClickSave();
    }
}
