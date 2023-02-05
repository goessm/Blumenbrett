using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        text.text = GameLoop.Instance.gameState.ToString();
    }
}
