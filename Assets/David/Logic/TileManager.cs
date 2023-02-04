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
        } 
    }

    private void InitRootBases()
    {
        rootBases = new Dictionary<RootType, RootBaseData>();
        rootBases[RootType.Yellow] = rootBaseList[0];
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
    Dictionary<Vector2, TileData> board = new Dictionary<Vector2, TileData>
    {
        {new Vector2(0, 0), new TileData()},
        {new Vector2(0, 1), new TileData()},
        {new Vector2(0, 2), new TileData()},
        {new Vector2(0, 3), new TileData()},
        {new Vector2(0, 4), new TileData()},
    };

    public bool IsInBounds(Vector2 pos)
    {
        return (pos.x <= BoardWidth && pos.y <= BoardLength);
    }
    public RootData GetDefaultRootData(RootType type)
    {
        return new RootData(rootBases[type]);
    }
    public bool PlantRoot(RootType type, Vector2 pos, bool automatic = false)
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
        // Bro why do 2D tilemaps use Vector3Int bro pls
        rootTilemap.SetTile(new Vector3Int((int) pos.x, (int) pos.y, 0), rootData.rootTile);
        return true;

    }

    public void SpreadRoots() {
        foreach(Vector2 pos in board.Keys)
        {
            TileData tileData = board[pos];
            foreach(RootType rootType in tileData.roots.Keys)
            {
                GrowAttempt(pos, rootType);
            }
        }
    }

    private void GrowAttempt(Vector2 pos, RootType rootType)
    {
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
            GrowRoot(pos, rootData);
        }
    }

    private void GrowRoot(Vector2 pos, RootData rootData)
    {
        Vector2[] spreadDirections = rootData.spreadDirections;
        if (spreadDirections.Length == 0) return;
        Vector2 growDir = spreadDirections[Random.Range(0, spreadDirections.Length)];
        Vector2 targetPos = pos + growDir;
        PlantRoot(rootData.rootType, targetPos, true);
    }
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