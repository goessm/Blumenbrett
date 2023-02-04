using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootData
{
    public RootType rootType;

    public TileBase rootTile;

    public FlowerType flowerType;

    public float spreadChance;
    public Vector2Int[] spreadDirections;

    public int maxAge;

    public RootFusion[] fusions;

    public int age;

    public RootData(RootBaseData fromBase)
    {
        rootType = fromBase.rootType;
        rootTile = fromBase.rootTile;
        flowerType = fromBase.flowerType;
        spreadChance = fromBase.spreadChance;
        spreadDirections = fromBase.spreadDirections;
        fusions = fromBase.fusions;
        maxAge = fromBase.maxAge;

        age = 0;
    }

}


