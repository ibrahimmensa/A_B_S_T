using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class CardHandler : MonoBehaviour
{
    public CardValues cardValue;
    public Sprite cover;
    public Image image;
    public Image valueImage;
    bool isClicked = false;
    public GameObject vfx;
    public bool isVisible = true;

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
        valueImage.gameObject.SetActive(true);
        valueImage.sprite = cardValue.Image;
        image.sprite = null;
    }

    public void hideCardValue()
    {
        image.sprite = cover;
        image.color = Color.white;
        valueImage.gameObject.SetActive(false);
        isClicked = false;
    }

    public void hideCardValueWithDelay()
    {
        image.color = Color.red;
        Invoke("hideCardValue", 2);
    }

    public void destroyCardWithDelay()
    {
        isVisible = false;
        image.color = Color.green;
        Invoke("destroyCard", 2);
    }

    public void destroyCard()
    {
        GameObject effect = Instantiate(vfx, Vector3.zero, Quaternion.identity, this.transform);
        effect.transform.position = Vector3.zero;
        effect.transform.localPosition = Vector3.zero;
        Destroy(effect, 0.7f);
        image.color = new Color(0, 0, 0, 0);
        valueImage.color = new Color(0, 0, 0, 0);
        valueImage.gameObject.SetActive(false);
        isVisible = false;
    }
}
