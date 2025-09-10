using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    public CardValues cardValue;
    public Sprite cover;
    public Image image;
    bool isClicked = false;
    public GameObject vfx;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void onClickCard()
    {
        if (isClicked == true)
            return;
        isClicked = true;
        GamePlayManager.Instance.onCardClicked(this);
        revealCardValue();
        SoundManager.Instance.playSound(sfx_type.CARD_FLIP);
    }

    public void revealCardValue()
    {
        image.sprite = cardValue.Image;
    }

    public void hideCardValue()
    {
        image.sprite = cover;
        isClicked = false;
    }

    public void hideCardValueWithDelay()
    {
        
        Invoke("hideCardValue", 2);
    }

    public void destroyCardWithDelay()
    {
        Invoke("destroyCard", 2);
    }

    public void destroyCard()
    {
        GameObject effect = Instantiate(vfx, Vector3.zero, Quaternion.identity, this.transform);
        effect.transform.position = Vector3.zero;
        effect.transform.localPosition = Vector3.zero;
        Destroy(effect, 0.7f);
        image.color = new Color(0, 0, 0, 0);
    }
}
