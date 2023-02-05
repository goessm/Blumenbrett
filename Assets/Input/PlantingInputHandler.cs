using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlantingInputHandler : MonoBehaviour
{
    public void OnClick(InputAction.CallbackContext context)
    {
        if (GameLoop.Instance.gameState != GameState.PLANTING)
        {
            return;
        }
        if(context.performed)
        {
            Debug.Log(context);
            Vector3 mousePos = Mouse.current.position.ReadValue();   
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);
            //Debug.Log(Worldpos);

            Tilemap rootMap = TileManager.Instance.rootTilemap;
            var tilePos = rootMap.WorldToCell(Worldpos);
            //Debug.Log(tilePos);
            if (!TileManager.Instance.board.IsInBounds((Vector2Int) tilePos))
            {
                return;
            }
            RootType rootToPlant = GameLoop.Instance.RootToPlant;
            TileManager.Instance.PlantRoot(rootToPlant, (Vector2Int) tilePos);
        }
    }
}
