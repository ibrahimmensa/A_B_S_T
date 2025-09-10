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
    public int clickCount = 0;
    public int score = 0;
    public CardHandler previouslyClickedCard;
    public bool isLoadingSavedLevel = false;
    public int cardCoverIndex;
    public bool resetLevels = false;
    #endregion

    //Using OnEnable To Pass all sprites of gems and indexes for card values
    private void OnEnable()
    {
        if (resetLevels)
        {
            PlayerPrefs.SetInt("level", 0);
        }
        level = PlayerPrefs.GetInt("level", 0);
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
        //if user is not loading a saved level regular levels will be played in sequence
        if (!isLoadingSavedLevel)
        {
            level = PlayerPrefs.GetInt("level", 0);
            if (level >= allGrids.Length)
            {
                int index = allGrids.Length - 1;
                allGrids[index].gameObject.SetActive(true);
            }
            else
            {
                allGrids[level].gameObject.SetActive(true);
            }
        }
        else
        {
            //if user loaded a previous level then this part will be executed
            SaveLoadManager.Instance.load();
            int loadedLevel = SaveLoadManager.Instance.levelData.roundNumber;
            if (loadedLevel >= allGrids.Length)
            {
                int index = allGrids.Length - 1;
                allGrids[index].gameObject.SetActive(true);
            }
            else
            {
                allGrids[loadedLevel].gameObject.SetActive(true);
            }
        }
    }


    #region Grid_Setup
    //This function will setup grid place card prefabs in the grid and assign card values
    public void setupGrid(GridHandler grid, int rows, int columns)
    {
        currentGrid = grid;
        //if user is not loading a saved file new level with random values will be initiated
        if (!isLoadingSavedLevel)
        {
            int totalCards = rows * columns;
            int cardCover = UnityEngine.Random.Range(0, 2);
            cardCoverIndex = cardCover;
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
            for (int i = 0; i < totalCards / 2; i++)
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
                spawnedCardHandler.cover = cardCovers[cardCover];
                spawnedCardHandler.cardValue = temp_GridCards[i].cardValue;
                grid.allCards.Add(spawnedCardHandler);
                grid.totalCards = totalCards;
                clickCount = 0;
                score = 0;
            }
        }
        else
        {
            //If user loaded a saved level all the data will be retrieved from the save file
            level = SaveLoadManager.Instance.levelData.roundNumber;
            MenuManager.Instance.updateRound(level);
            score = SaveLoadManager.Instance.levelData.score;
            MenuManager.Instance.updateScore(score);
            for (int i = 0; i < SaveLoadManager.Instance.levelData.allCards.Length; i++)
            {
                GameObject SpawnedCard = Instantiate(cardPrefab, grid.transform);
                SpawnedCard.GetComponent<Image>().sprite = cardCovers[SaveLoadManager.Instance.levelData.coverIndex];
                CardHandler spawnedCardHandler = SpawnedCard.GetComponent<CardHandler>();
                spawnedCardHandler.cover = cardCovers[SaveLoadManager.Instance.levelData.coverIndex];
                spawnedCardHandler.cardValue = AllCardValues[SaveLoadManager.Instance.levelData.allCards[i].cardValueIndex];
                //SpawnedCard.GetComponent<Image>().sprite = spawnedCardHandler.cardValue.Image; 
                grid.allCards.Add(spawnedCardHandler);
                currentGrid.totalCards = SaveLoadManager.Instance.levelData.totalCards;
                if (!SaveLoadManager.Instance.levelData.allCards[i].cardVisible)
                {
                    spawnedCardHandler.image.color = new Color(0, 0, 0, 0);
                    spawnedCardHandler.isVisible = false;
                }
            }
            //turning this check off to prevent reloading of the same level
            isLoadingSavedLevel = false;
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
    #endregion

    #region GamePlay
    public void onCardClicked(CardHandler clickedCard)
    {
        //Making sure the same card is not clicked twice
        if(clickCount % 2 == 1)
        {
            if (previouslyClickedCard == clickedCard)
                return;
        }
        clickCount++;
        //If click count is odd it means first card is clicked
        if (clickCount % 2 == 1)
        {
            previouslyClickedCard = clickedCard;
        }
        else
        {
            //if click count is even it means its the second card and now we will compare 2 cards if they have same images or values
            if (previouslyClickedCard.cardValue.index == clickedCard.cardValue.index)
            {
                Debug.Log("Card Matched");
                score += 10;
                MenuManager.Instance.updateScore(score);
                previouslyClickedCard.destroyCardWithDelay();
                clickedCard.destroyCardWithDelay();
                currentGrid.pairFound();
                StartCoroutine(playSoundWithDelay(sfx_type.CARD_MATCHED));
            }
            else
            {
                Debug.Log("Card not matched");
                previouslyClickedCard.hideCardValueWithDelay();
                clickedCard.hideCardValueWithDelay();
                StartCoroutine(playSoundWithDelay(sfx_type.CARD_NOT_MATCHED));
            }
        }
    }

    IEnumerator playSoundWithDelay(sfx_type type)
    {
        yield return new WaitForSeconds(1);
        SoundManager.Instance.playSound(type);
    }

    //When all cards are found game will end
    public void allCardsFound()
    {
        currentGrid.resetGrid();
        currentGrid.gameObject.SetActive(false);
        level++;
        PlayerPrefs.SetInt("level", level);
        MenuManager.Instance.onSwitchMenu(Menus.WINPOPUP);
        SoundManager.Instance.playSound(sfx_type.WIN_SFX);
    }

    public void onClickNextLevel()
    {
        OnClickPlay();
    }
    #endregion

    #region SAVE&LOAD_LEVEL

    //This function will save current state of the level
    public void onClickSave()
    {
        SaveLoadManager.Instance.save(currentGrid.allCards, level, score, cardCoverIndex);
    }
    #endregion
}
