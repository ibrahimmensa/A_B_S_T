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
        MenuManager.Instance.onSwitchMenu(Menus.MAINMENU);
    }

    public void onClickReturn()
    {
        
    }
}
