using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelData
{
    public int roundNumber;
    public int score;
    public int coverIndex;
    public cardData[] allCards;
}

[Serializable]
public class cardData
{
    public int cardValueIndex;
    public bool cardVisible;
}
public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public LevelData levelData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void save(List<CardHandler> allCards, int round, int score, int cover)
    {
        levelData.roundNumber = round;
        levelData.score = score;
        levelData.coverIndex = cover;
        levelData.allCards = new cardData[allCards.Count];
        for (int i = 0; i < allCards.Count; i++)
        {
            levelData.allCards[i] = new cardData();
            levelData.allCards[i].cardValueIndex = allCards[i].cardValue.index;
            levelData.allCards[i].cardVisible = allCards[i].isVisible;
        }
        string levelJson = JsonUtility.ToJson(levelData);
        Debug.Log(levelJson);
        PlayerPrefs.SetString("leveldata", levelJson);
        PlayerPrefs.SetInt("havesafefile", 1);
    }

    public void load()
    {
        if (PlayerPrefs.GetInt("havesafefile", 0) == 0)
            return;
        string levelJson = PlayerPrefs.GetString("leveldata");
        levelData = JsonUtility.FromJson<LevelData>(levelJson);
        Debug.Log(levelJson);
    }
}
