using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board
{
    public const int BoardWidth = 4;
    public const int BoardLength = 4;
    public Dictionary<Vector2Int, TileData> board = new Dictionary<Vector2Int, TileData>();

    public bool IsInBounds(Vector2Int pos)
    {
        return board.ContainsKey(pos);
    }
    
    public RootData GetRootAt(Vector2Int pos) {
        if (!IsInBounds(pos) && !board.ContainsKey(pos)) return null;
        return board[pos].root;
    }

    public void SetRootAt(Vector2Int pos, RootData root) {
        if (!IsInBounds(pos) && !board.ContainsKey(pos)) return;
        board[pos].root = root;
    }

    public FlowerData GetFlowerAt(Vector2Int pos)
    {
        if (!IsInBounds(pos) && !board.ContainsKey(pos)) return null;
        return board[pos].flower;
    }

    public void SetFlowerAt(Vector2Int pos, FlowerData flower) {
        if (!IsInBounds(pos) && !board.ContainsKey(pos)) return;
        board[pos].flower = flower;
    }

    public Board()
    {
        InitBoard();
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
}

public class TileData
{
    public RootData root;
    public FlowerData flower;
}