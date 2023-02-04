using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootData
{
    public RootType rootType;

    public TileBase rootTile;

    public float spreadChance;
    public Vector2Int[] spreadDirections;

    public int maxAge;

    public int age;

    public RootData(RootBaseData fromBase)
    {
        rootType = fromBase.rootType;
        rootTile = fromBase.rootTile;
        spreadChance = fromBase.spreadChance;
        spreadDirections = fromBase.spreadDirections;
        maxAge = fromBase.maxAge;

        age = 0;
    }

}


