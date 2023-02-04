using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    [SerializeField]
    RootInfo[] rootInfos;

    public RootInfo GetRootInfo(RootType type)
    {
        foreach(RootInfo rootInfo in rootInfos)
        {
            if (rootInfo.type == type)
            {
                return rootInfo;
            }
        }
        return null;
    }
}

[System.Serializable]
public class RootInfo
{
    public RootType type;
    public RootBaseData rootBase;
    public RootData GetDefaultRootData()
    {
        return new RootData(rootBase);
    }
}
