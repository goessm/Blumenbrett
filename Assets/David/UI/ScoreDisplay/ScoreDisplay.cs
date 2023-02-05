using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = GameLoop.Instance.score.ToString();
    }
}
