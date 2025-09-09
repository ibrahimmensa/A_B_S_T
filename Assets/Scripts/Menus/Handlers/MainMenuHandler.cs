using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickPlay()
    {
        MenuManager.Instance.onSwitchMenu(Menus.GAMEPLAY);
        GamePlayManager.Instance.OnClickPlay();
    }

    public void onClickSettings()
    {
        MenuManager.Instance.onSwitchMenu(Menus.SETTINGS);
    }
}
