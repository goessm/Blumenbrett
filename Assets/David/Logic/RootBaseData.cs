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

    public float spreadChance;
    public Vector2Int[] spreadDirections;

    public int maxAge;

}


