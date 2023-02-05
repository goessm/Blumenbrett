using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainButton : MonoBehaviour
{
    public Button btn;
    void Update()
    {
        btn.interactable = GameLoop.Instance.gameState == GameState.RAIN_PLS;
    }

    public void MakeItRain()
    {
        GameLoop.Instance.gameState = GameState.GROWING;
        GameLoop.Instance.ProcessState();
    }
}
