using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GamePlayHandler : MonoBehaviour
{
    public TMP_Text score_Text;
    public TMP_Text counter_Text;
    public TMP_Text round_Text;
    public GameObject CountDown_GameObject;
    // Start is called before the first frame update

    public void updateScore(int score)
    {
        score_Text.text = score.ToString();
    }

    public void updateRound(int round)
    {
        round_Text.text = round.ToString();
    }
}
