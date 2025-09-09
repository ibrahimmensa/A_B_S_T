using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct CardValues
{
    //Image which will be compared
    public Sprite Image;
    public Sprite cover;
    //Number for checking pair
    public int index;
    //Bool used to make sure no 2 pairs of same cards is available in a match
    public bool isUsed;

}
public class GamePlayManager : Singleton<GamePlayManager>
{
    #region Grids_Variables
    //Keeping reference of all available grids
    public GridHandler[] allGrids;
    // Grid for current level
    public GridHandler currentGrid;
    #endregion
    #region Cards_Variables
    //Sprite reference of different type of card backs
    public Sprite[] cardCovers;
    //Card Prefab to be spawned
    public GameObject cardPrefab;
    //An Array of card Values struct
    public List<CardValues> AllCardValues;
    #endregion
    public Sprite[] AllGems;

    #region GampePlay_Variables
    public int level;
    #endregion

    //Using OnEnable To Pass all sprites of gems and indexes for card values
    private void OnEnable()
    {
        for (int i = 0; i < AllGems.Length; i++)
        {
            CardValues temp = new CardValues();
            temp.Image = AllGems[i];
            temp.index = i;
            AllCardValues.Add(temp);
        }
    }



    //This function is called when play button is clicked
    public void OnClickPlay()
    {
        allGrids[allGrids.Length-1].gameObject.SetActive(true);
    }

    //This function will setup grid place card prefabs in the grid and assign card values
    public void setupGrid(GridHandler grid, int rows, int columns)
    {
        int totalCards = rows * columns;
        int cardCover = UnityEngine.Random.Range(0, 2);
        List<CardHandler> temp_GridCards = new List<CardHandler>();

        //Resetting all cards to not used state
        resetCardValues();

        //Making sure each grid have even number of cards to make sure every card have a pair
        if (totalCards % 2 != 0)
        {
            Debug.LogError("This grid don't have even number of cards");
            return;
        }

        // Setting up 2 cards per cycle to make sure there is a pair for every card
        for(int i = 0; i < totalCards/2; i++)
        {
            CardHandler temp_Card1 = new CardHandler();
            temp_Card1.cardValue = getRandomCardValue();
            temp_GridCards.Add(temp_Card1);
            CardHandler temp_Card2 = new CardHandler();
            temp_Card2.cardValue = temp_Card1.cardValue;
            temp_GridCards.Add(temp_Card2);
        }
        //shuffling assigned cards
        temp_GridCards = shuffleCards(temp_GridCards);

        //instantiating cards
        for (int i = 0; i < temp_GridCards.Count; i++)
        {
            GameObject SpawnedCard = Instantiate(cardPrefab, grid.transform);
            SpawnedCard.GetComponent<Image>().sprite = cardCovers[cardCover]; 
            CardHandler spawnedCardHandler = SpawnedCard.GetComponent<CardHandler>();
            spawnedCardHandler.cardValue = temp_GridCards[i].cardValue;
            SpawnedCard.GetComponent<Image>().sprite = spawnedCardHandler.cardValue.Image; 
            grid.allCards.Add(spawnedCardHandler);
        }
    }

    //Resetting all cards to not used state at start of every level
    public void resetCardValues()
    {
        for (int i = 0; i < AllCardValues.Count; i++)
        {
            CardValues temp = AllCardValues[i];
            temp.isUsed = false;
            AllCardValues[i] = temp;
        }
    }

    //Getting a random card value to make sure every time levels have different cards and images to match
    public CardValues getRandomCardValue()
    {
        CardValues temp;
        do
        {
            int index = UnityEngine.Random.Range(0, AllCardValues.Count);
            temp = AllCardValues[index];
            if (temp.isUsed == false)
            {
                CardValues placeholder = AllCardValues[index];
                placeholder.isUsed = true;
                AllCardValues[index] = placeholder;
            }
        } while (temp.isUsed == true);
        return temp;
    }

    //A function to shuffle cards in the array to randomize their positions in the grid
    // Also we can use this function to reset level without reinitiating whole process
    public List<CardHandler> shuffleCards(List<CardHandler> temp)
    {

        int shuffleCount = UnityEngine.Random.Range(80, 150);
        for (int i = 0; i < shuffleCount; i++)
        {
            int index1 = UnityEngine.Random.Range(0, temp.Count);
            int index2 = UnityEngine.Random.Range(0, temp.Count);
            CardHandler placeHolder = temp[index1];
            temp[index1] = temp[index2];
            temp[index2] = placeHolder;
        }
        return temp;
    }
}
