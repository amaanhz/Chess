using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    TileSet board;
    Camera cam;
    Ray ray;

    void Start()
    {
        board = GameObject.Find("chessboard").GetComponent<TileSet>();
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
            {
                if (hitInfo.collider.gameObject.GetComponent<Tile>())
                {
                    board.selectTile(hitInfo.collider.gameObject.GetComponent<Tile>());
                    return;
                }
            }
            board.deSelect();
        }
    }
}
