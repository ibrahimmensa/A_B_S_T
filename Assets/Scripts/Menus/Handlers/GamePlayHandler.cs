using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GamePlayHandler : MonoBehaviour
{
    public TMP_Text score_Text;
    public TMP_Text counter_Text;
    public GameObject CountDown_GameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void upDataeScore(int score)
    {
        score_Text.text = score.ToString();
    }
}
