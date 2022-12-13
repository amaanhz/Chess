using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    public int team;
    public TileSet board;

    public static Material blackTeamMat;
    public static Material whiteTeamMat;

    public bool isKing = false;
    
    public Tile currentTile;
    public TileSet.Units unitName;
    public int teamIndex;

    public void SetTeam(int team)
    {
        if (blackTeamMat == null)
        {
            blackTeamMat = Resources.Load("BlackTeam") as Material;
            whiteTeamMat = Resources.Load("WhiteTeam") as Material;
        }
        this.team = team;
        transform.GetComponent<MeshRenderer>().material = team == 0 ? whiteTeamMat : blackTeamMat;
        if (team == 0)
        {
            for (int i = 0; i < 16; i++)
            {
                if (board.whiteTeam[i] == null) { board.whiteTeam[i] = this; teamIndex = i; }
            }
        }
        else
        { 
            for (int i = 0; i < 16; i++)
            {
                if (board.blackTeam[i] == null) { board.blackTeam[i] = this; teamIndex = i; }
            }
        }
    }

    virtual public bool[,] GetMoves()
    {
        return new bool[8, 8];
    }
    
    virtual public bool[,] GetAttacks()
    {
        return new bool[8, 8];
    }
}
