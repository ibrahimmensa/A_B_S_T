using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Menus enum to keep track of current menu
public enum Menus
{
    MAINMENU,
    SETTINGS,
    DISCLAIMER,
    WINPOPUP,
    LOSEPOPUP,
    PAUSE,
    SPLASH,
    GAMEPLAY
}

//any kind of communication between menus will be done through this menu manager which has reference for all menus
public class MenuManager : Singleton<MenuManager>
{
    
    #region ALL_MENUS_REFERENCES
    [SerializeField]
    private MainMenuHandler mainMenuHandler;
    [SerializeField]
    private SettingsHandler settingsHandler;
    [SerializeField]
    private DisclaimerHandler disclaimerHandler;
    [SerializeField]
    private WinScreenHandler winScreenHandler;
    [SerializeField]
    private FailScreenHandler failScreenHandler;
    [SerializeField]
    private PauseScreenHandler pauseScreenHandler;
    [SerializeField]
    private SplashHandler splashHandler;
    [SerializeField]
    private GamePlayHandler gamePlayHandler;
    #endregion

    //Keeping it available in inspector for checking purposes
    [SerializeField]
    private Menus currentMenu = Menus.SPLASH;
    private GameObject currentMenuGameObject = null;

    public int currentScore;
    //making sure to start the game with splash screen
    private void OnEnable()
    {
        closeAllMenus();
        onSwitchMenu(Menus.SPLASH);
    }

    //all menus will call this function to switch menus
    public void onSwitchMenu(Menus newMenu)
    {
        //closing current open menu first to avoid menu overlapping
        closeCurrentMenu();
        switch (newMenu)
        {
            case Menus.MAINMENU:
                currentMenuGameObject = mainMenuHandler.gameObject;
                break;
            case Menus.SETTINGS:
                currentMenuGameObject = settingsHandler.gameObject;
                break;
            case Menus.DISCLAIMER:
                currentMenuGameObject = disclaimerHandler.gameObject;
                break;
            case Menus.WINPOPUP:
                currentMenuGameObject = winScreenHandler.gameObject;
                winScreenHandler.scoreText.text = currentScore.ToString();
                break;
            case Menus.LOSEPOPUP:
                currentMenuGameObject = failScreenHandler.gameObject;
                break;
            case Menus.SPLASH:
                currentMenuGameObject = splashHandler.gameObject;
                break;
            case Menus.GAMEPLAY:
                currentMenuGameObject = gamePlayHandler.gameObject;
                updateRound(GamePlayManager.Instance.level);
                updateScore(0);
                break;
        }
        currentMenuGameObject.SetActive(true);
    }

    public void closeCurrentMenu()
    {
        //adding check foor null reference
        if (currentMenuGameObject == null)
            return;
        currentMenuGameObject.SetActive(false);
    }


    //Only run at the start of game to avoid overlaping menus and only for splash to appear
    public void closeAllMenus()
    {
        splashHandler.gameObject.SetActive(false);
        mainMenuHandler.gameObject.SetActive(false);
        pauseScreenHandler.gameObject.SetActive(false);
        winScreenHandler.gameObject.SetActive(false);
        failScreenHandler.gameObject.SetActive(false);
        disclaimerHandler.gameObject.SetActive(false);
        settingsHandler.gameObject.SetActive(false);
        gamePlayHandler.gameObject.SetActive(false);
    }

    public void updateScore(int score)
    {
        gamePlayHandler.updateScore(score);
        currentScore = score;
    }

    public void updateRound(int round)
    {
        gamePlayHandler.updateRound(round + 1);
    }

    public void startNextLevel()
    {
        GamePlayManager.Instance.onClickNextLevel();
        onSwitchMenu(Menus.GAMEPLAY);
    }
}
