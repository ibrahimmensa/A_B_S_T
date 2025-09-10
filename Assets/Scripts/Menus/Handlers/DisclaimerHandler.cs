using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisclaimerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickClose()
    {
        SoundManager.Instance.playSound(sfx_type.BTN_CLICK);
        MenuManager.Instance.onSwitchMenu(Menus.MAINMENU);
    }
}
