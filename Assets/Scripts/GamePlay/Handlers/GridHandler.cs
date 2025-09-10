using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script will handle all the grid details and cards in that specific grid
public class GridHandler : MonoBehaviour
{
    [SerializeField]
    public int rows, column;
    public List<CardHandler> allCards;
    public int totalCards;


    private void OnEnable()
    {
        initGrid();
        
    }
    //This function will call gamemanager to spawn all cards in the grid and assign values to cards
    public void initGrid()
    {
        resetGrid();
        GamePlayManager.Instance.setupGrid(this, rows, column);
        //totalCards = rows * column;
    }

    public void resetGrid()
    {
        if (allCards == null)
            return;
        for (int i = 0; i < allCards.Count; i++)
        {
            Destroy(allCards[i].gameObject);
        }
        allCards.Clear();
    }

    public void pairFound()
    {
        totalCards = totalCards - 2;
        if (totalCards == 0)
        {
            StartCoroutine(finishMatchWithDelay());
        }
    }

    IEnumerator finishMatchWithDelay()
    {
        yield return new WaitForSeconds(3);
        GamePlayManager.Instance.allCardsFound();
    }
}
