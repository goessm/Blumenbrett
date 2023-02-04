using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    RootManager rootManager;

    [SerializeField]
    FlowerManager flowerManager;

    [SerializeField]
    Tilemap rootTilemap;
    
    [SerializeField]
    Board board = new Board();

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
        } 
    }

    public bool PlantRoot(RootType type, Vector2Int pos, bool automatic = false)
    {
        Debug.Log($"Trying to plant {type} at {pos}");

        if (!board.IsInBounds(pos)) return false;

        RootData rootData = rootManager.GetRootInfo(type)?.GetDefaultRootData();
        board.SetRootAt(pos, rootData);
        RedrawTilemap();
        return true;

    }

    private void RedrawTilemap()
    {
        foreach(Vector2Int pos in board.board.Keys)
        {
            TileBase rootTile = board.GetRootAt(pos)?.rootTile;
            if (!rootTile) continue;
            rootTilemap.SetTile((Vector3Int) pos, rootTile);
        }
    }

    public void SpreadRoots() {
        List<GrowthResult> growthResults = new();

        foreach(Vector2Int pos in board.board.Keys)
        {
            GrowAttempt(pos, ref growthResults);
        }

        foreach(GrowthResult res in growthResults)
        {
            PlantRoot(res.type, res.pos, true);
        }
    }

    public void GrowFlowers()
    {
        foreach(Vector2Int pos in board.board.Keys)
        {
            RootData root = board.GetRootAt(pos);
            if (root == null) continue;
            FlowerType flowerType = root.flowerType;
            board.SetFlowerAt(pos, flowerManager.GetFlowerInfo(flowerType).GetDefaultFlowerData());
        }
    }

    private void GrowAttempt(Vector2Int pos, ref List<GrowthResult> growthResults)
    {
        //Debug.Log($"Growth attempt of {rootType} at {pos}");
        if (!board.IsInBounds(pos))
        {
            Debug.Log($"Error in growth attempt. Pos {pos} out of bounds.");
            return;
        }

        RootData rootData = board.GetRootAt(pos);

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
        if (!board.IsInBounds(targetPos)) return;
        Debug.Log($"Growing root {rootData.rootType}from {pos} to {targetPos}");
        if (board.GetRootAt(targetPos) == null)
        {
            // No fusion
            GrowthResult result = new();
            result.pos = targetPos;
            result.type = rootData.rootType;
            growthResults.Add(result);
            return;
        }
        RootType targetRoot = board.GetRootAt(targetPos).rootType;
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
