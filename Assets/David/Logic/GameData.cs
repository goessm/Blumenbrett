    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public const int BoardWidth = 4;
    public const int BoardLength = 4;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
