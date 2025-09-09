using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashHandler : MonoBehaviour
{

    public float delayToShowMainMenu = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //calling display main menu function to start timer
    private void OnEnable()
    {
        displayDisclaimerWithDelay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayDisclaimerWithDelay()
    {
        Invoke("showDisclaimer", delayToShowMainMenu);
    }

    public void showDisclaimer()
    {
        MenuManager.Instance.onSwitchMenu(Menus.DISCLAIMER);
    }
}
