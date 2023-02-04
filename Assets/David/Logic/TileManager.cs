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
        foreach(RootBaseData rootBase in rootBaseList)
        {
            rootBases[rootBase.rootType] = rootBase;
        }

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
        return board.ContainsKey(pos);
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

        if (tileData.root != null)
        {
            Debug.Log("Tried to plant root where root already exists!");
            return false;
        }
        RootData rootData = GetDefaultRootData(type);
        tileData.root = rootData;
        rootTilemap.SetTile((Vector3Int) pos, rootData.rootTile);
        return true;

    }

    public void SpreadRoots() {
        List<GrowthResult> growthResults = new();

        foreach(Vector2Int pos in board.Keys)
        {
            TileData tileData = board[pos];
            GrowAttempt(pos, ref growthResults);
        }

        foreach(GrowthResult res in growthResults)
        {
            PlantRoot(res.type, res.pos, true);
        }
    }

    public void GrowFlowers()
    {
        foreach(Vector2Int pos in board.Keys)
        {
            TileData tileData = board[pos];

        }
    }

    private void GrowAttempt(Vector2Int pos, ref List<GrowthResult> growthResults)
    {
        //Debug.Log($"Growth attempt of {rootType} at {pos}");
        if (!IsInBounds(pos))
        {
            Debug.Log($"Error in growth attempt. Pos {pos} out of bounds.");
            return;
        }

        TileData tileData = board[pos];
        RootData rootData = board[pos].root;

        if (rootData == null)
        {
            Debug.Log($"Error in growth attempt. No rootData at pos {pos}");
            return;
        }

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
        if (!IsInBounds(targetPos)) return;
        Debug.Log($"Growing root {rootData.rootType}from {pos} to {targetPos}");
        if (board[targetPos].root == null)
        {
            // No fusion
            GrowthResult result = new();
            result.pos = targetPos;
            result.type = rootData.rootType;
            growthResults.Add(result);
            return;
        }
        RootType targetRoot = board[targetPos].root.rootType;
        FuseRoots(targetPos, rootData, targetRoot, ref growthResults);
    }

    private void FuseRoots(Vector2Int targetPos, RootData growingRoot, RootType targetRoot, ref List<GrowthResult> growthResults)
    {
        RootFusion[] fusions = growingRoot.fusions;
        foreach(RootFusion fusion in fusions)
        {
            if (fusion.FusionPartner == targetRoot)
            {
                Debug.Log($"Fusing {growingRoot.rootType} with {targetRoot} {fusion.FusionPartner} to make {fusion.Result}");
                GrowthResult result = new();
                result.pos = targetPos;
                result.type = fusion.Result;
                growthResults.Add(result);
                break;
            }
        }
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
    public RootData root;

    public TileData()
    {
        roots = new Dictionary<RootType, RootData>();
    }

}