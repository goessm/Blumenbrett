using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{

    public static TileManager Instance { get; private set; }
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;
            InitRootBases();
            InitBoard();
        } 
    }

    private void InitRootBases()
    {
        rootBases = new Dictionary<RootType, RootBaseData>();
        rootBases[RootType.Yellow] = rootBaseList[0];
    }

    private void InitBoard()
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            for (int j = 0; j < BoardLength; j++)
            {
                board.Add(new Vector2Int(i, j), new TileData());
            }
        }
    }

    public const int BoardWidth = 4;
    public const int BoardLength = 4;

    [SerializeField]
    RootBaseData[] rootBaseList;
    Dictionary<RootType, RootBaseData> rootBases;

    [SerializeField]
    TileBase[] rootTiles;

    [SerializeField]
    Tilemap rootTilemap;

    [SerializeField]
    Dictionary<Vector2Int, TileData> board = new Dictionary<Vector2Int, TileData>();

    public bool IsInBounds(Vector2Int pos)
    {
        return (pos.x <= BoardWidth && pos.y <= BoardLength);
    }
    public RootData GetDefaultRootData(RootType type)
    {
        return new RootData(rootBases[type]);
    }
    public bool PlantRoot(RootType type, Vector2Int pos, bool automatic = false)
    {
        Debug.Log($"Trying to plant {type} at {pos}");
        if (!board.ContainsKey(pos)) return false;

        TileData tileData = board[pos];

        if (tileData.roots.ContainsKey(type))
        {
            Debug.Log("Tried to plant root where it already exists!");
            return false;
        }
        RootData rootData = GetDefaultRootData(type);
        tileData.roots.Add(type, rootData);
        rootTilemap.SetTile((Vector3Int) pos, rootData.rootTile);
        return true;

    }

    public void SpreadRoots() {
        List<GrowthResult> growthResults = new();
        
        foreach(Vector2Int pos in board.Keys)
        {
            TileData tileData = board[pos];
            foreach(RootType rootType in tileData.roots.Keys)
            {
                GrowAttempt(pos, rootType, ref growthResults);
            }
        }

        foreach(GrowthResult res in growthResults)
        {
            PlantRoot(res.type, res.pos, true);
        }
    }

    private void GrowAttempt(Vector2Int pos, RootType rootType, ref List<GrowthResult> growthResults)
    {
        Debug.Log($"Growth attempt of {rootType} at {pos}");
        if (!board.ContainsKey(pos))
        {
            Debug.Log($"Error in growth attempt. No data at pos {pos}");
            return;
        }

        TileData tile = board[pos];

        if (!tile.roots.ContainsKey(rootType))
        {
            Debug.Log($"Error in growth attempt: root type {rootType} not at pos {pos}");
            return;
        }

        RootData rootData = tile.roots[rootType];
        if (rootData.spreadChance >= Random.Range(0f, 1f))
        {
            GrowRoot(pos, rootData, ref growthResults);
        }
    }

    private void GrowRoot(Vector2Int pos, RootData rootData, ref List<GrowthResult> growthResults)
    {
        Vector2Int[] spreadDirections = rootData.spreadDirections;
        if (spreadDirections.Length == 0) return;
        Vector2Int growDir = spreadDirections[Random.Range(0, spreadDirections.Length)];
        Vector2Int targetPos = pos + growDir;
        Debug.Log($"Growing root {rootData.rootType}from {pos} to {targetPos}");
        GrowthResult result = new();
        result.pos = targetPos;
        result.type = rootData.rootType;
        growthResults.Add(result);
    }
}

public class GrowthResult
{
    public Vector2Int pos;
    public RootType type;
}

public class TileData
{
    public Dictionary<RootType, RootData> roots;
    private FlowerType flower;

    public TileData()
    {
        roots = new Dictionary<RootType, RootData>();
        flower = FlowerType.None;
    }

}

public enum FlowerType
{
    None, Yellow, Green, Red, Purple
}