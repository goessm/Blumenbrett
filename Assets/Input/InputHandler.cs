using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log(context);
            Vector3 mousePos = Mouse.current.position.ReadValue();   
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);
            //Debug.Log(Worldpos);

            Tilemap tilemap = GameObject.Find("Root Map").GetComponent<Tilemap>();
            var tilePos = tilemap.WorldToCell(Worldpos);
            //Debug.Log(tilePos);
            
            TileManager.Instance.PlantRoot(RootType.Yellow, (Vector2Int) tilePos);
            TileManager.Instance.SpreadRoots();
        }
    }
}
