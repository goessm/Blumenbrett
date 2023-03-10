using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum FlowerType
{
    Yellow, Blue, Red, Purple, Green, Orange
}

[CreateAssetMenu]
public class FlowerData: ScriptableObject
{
    public FlowerType flowerType;
    public TileBase flowerTile;
    public float value;

    public FlowerData(FlowerData from)
    {
        flowerType = from.flowerType;
        flowerTile = from.flowerTile;
        value = from.value;
    }

}