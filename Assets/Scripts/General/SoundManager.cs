using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum sfx_type
{
    BTN_CLICK,
    CARD_FLIP,
    WIN_SFX,
    CARD_MATCHED,
    CARD_NOT_MATCHED,
}
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource BGMusic;
    public AudioSource SFX;
    public AudioClip btn_Click, card_Flip, win_SFX, card_Matched_SFX, card_Not_Matched_SFX;
    public AudioClip bg_Music;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(sfx_type type)
    {
        if (PlayerPrefs.GetInt("sfx", 1) == 0)
            return;
        switch (type)
        {
            case sfx_type.BTN_CLICK:
                SFX.PlayOneShot(btn_Click);
                break;
            case sfx_type.CARD_FLIP:
                SFX.PlayOneShot(card_Flip);
                break;
            case sfx_type.CARD_MATCHED:
                SFX.PlayOneShot(card_Matched_SFX);
                break;
            case sfx_type.CARD_NOT_MATCHED:
                SFX.PlayOneShot(card_Not_Matched_SFX);
                break;
            case sfx_type.WIN_SFX:
                SFX.PlayOneShot(win_SFX);
                break;
        }
    }
}
