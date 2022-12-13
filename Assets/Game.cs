using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject board;
    public camera camScript;

    public int activePlayer = 0; // 0 is white

    public static Vector3 yUp(Vector3 vec) // Compensate for Blender Z-Up to Unity Y-Up
    {
        return new Vector3(vec.x, vec.z, vec.y);
    }

    public static Vector3 absVec(Vector3 vec)
    {
        return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
    }

    void Start()
    {
        board = GameObject.Find("chessboard");
        camScript = GameObject.Find("Main Camera").GetComponent<camera>();
        camScript.enabled = true;
        
    }

    void Update()
    {
        
    }
}
