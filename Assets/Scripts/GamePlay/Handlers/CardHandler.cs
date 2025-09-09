using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    public CardValues cardValue;
    public Sprite cover;
    public Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void onClickCard()
    {
        GamePlayManager.Instance.onCardClicked(this);
        revealCardValue();
    }

    public void revealCardValue()
    {
        image.sprite = cardValue.Image;
    }

    public void hideCardValue()
    {
        image.sprite = cover;
    }

    public void hideCardValueWithDelay()
    {
        Invoke("hideCardValue", 2);
    }
}
