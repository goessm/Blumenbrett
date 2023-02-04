using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum FlowerType
{
    None, Yellow, Green, Red, Purple
}

[CreateAssetMenu]
public class FlowerData: ScriptableObject
{
    public FlowerType FlowerType;
    public TileBase flowerTile;
    public float value;

}