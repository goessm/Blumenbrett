using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum RootType
{
    Yellow, Green, Red, Purple
}

[CreateAssetMenu]
public class RootBaseData: ScriptableObject
{
    public RootType rootType;

    public TileBase rootTile;

    public FlowerType flowerType;

    public float spreadChance;
    public Vector2Int[] spreadDirections;

    public RootFusion[] fusions;

    public int maxAge;

}

[System.Serializable]
public class RootFusion
{
    public RootType FusionPartner;
    public RootType Result;
}


