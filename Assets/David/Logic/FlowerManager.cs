using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    [SerializeField]
    FlowerInfo[] flowerInfos;

    public FlowerInfo GetFlowerInfo(FlowerType type)
    {
        foreach(FlowerInfo flowerInfo in flowerInfos)
        {
            if (flowerInfo.type == type)
            {
                return flowerInfo;
            }
        }
        return null;
    }
}

[System.Serializable]
public class FlowerInfo
{
    public FlowerType type;
    public FlowerData flowerData;
}
