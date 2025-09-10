using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//This class is responsible to saving all of the level data
[Serializable]
public class LevelData
{
    public int roundNumber;
    public int score;
    public int coverIndex;
    public int totalCards;
    public cardData[] allCards;
}

// data of all the remaining cards that will be saved
[Serializable]
public class cardData
{
    public int cardValueIndex;
    public bool cardVisible;
}
public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public LevelData levelData;

    //This function will save all the required level data in form os JSON string in playerprefs
    public void save(List<CardHandler> allCards, int round, int score, int cover)
    {
        levelData.roundNumber = round;
        levelData.score = score;
        levelData.coverIndex = cover;
        levelData.allCards = new cardData[allCards.Count];
        levelData.totalCards = GamePlayManager.Instance.currentGrid.totalCards;
        for (int i = 0; i < allCards.Count; i++)
        {
            levelData.allCards[i] = new cardData();
            levelData.allCards[i].cardValueIndex = allCards[i].cardValue.index;
            levelData.allCards[i].cardVisible = allCards[i].isVisible;
        }
        //converting objects into JSON format so that they can be storable in playerprefs as a string
        string levelJson = JsonUtility.ToJson(levelData);
        Debug.Log(levelJson);
        PlayerPrefs.SetString("leveldata", levelJson);
        //to keep track if there is a save file so that load option on main menu should be interactable
        PlayerPrefs.SetInt("havesafefile", 1);
    }

    //Load all of the save data by extracting from playerprefs and mapping back into object from JSON
    public void load()
    {
        //if there is no save file return
        if (PlayerPrefs.GetInt("havesafefile", 0) == 0)
            return;
        string levelJson = PlayerPrefs.GetString("leveldata");
        levelData = JsonUtility.FromJson<LevelData>(levelJson);
        Debug.Log(levelJson);
    }
}
