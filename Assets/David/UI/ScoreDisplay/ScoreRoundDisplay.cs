using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreRoundDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = GameLoop.Instance.roundScore.ToString();
    }
}
