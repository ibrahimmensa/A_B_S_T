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
    Animator anim;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }

    //Called when card is clicked
    public void onClickCard()
    {
        if (isClicked == true)
            return;
        isClicked = true;
        GamePlayManager.Instance.onCardClicked(this);
        revealCardValue();
        SoundManager.Instance.playSound(sfx_type.CARD_FLIP);
    }

    //Reveal the value of the card
    public void revealCardValue()
    {
        //valueImage.gameObject.SetActive(true);
        //valueImage.sprite = cardValue.Image;
        //image.sprite = null;
        anim.SetTrigger("flip");
        StartCoroutine(revealCardWithDelay());
    }

    IEnumerator revealCardWithDelay()
    {
        yield return new WaitForSeconds(0.45f);
        valueImage.gameObject.SetActive(true);
        valueImage.sprite = cardValue.Image;
        image.sprite = null;
    }



    //flip card back
    public void hideCardValue()
    {
        
    }

    public void hideCardValueWithDelay()
    {
        StartCoroutine(hideCardWithDelay());
    }

    IEnumerator hideCardWithDelay()
    {
        yield return new WaitForSeconds(1);
        image.color = Color.red;
        yield return new WaitForSeconds(1);
        image.sprite = cover;
        image.color = Color.white;
        valueImage.gameObject.SetActive(false);
        isClicked = false;
    }

    public void destroyCardWithDelay()
    {
        isVisible = false;
        //image.color = Color.green;
        StartCoroutine(destroyCard());
    }

    IEnumerator destroyCard()
    {
        yield return new WaitForSeconds(1);
        image.color = Color.green;
        yield return new WaitForSeconds(1);
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
