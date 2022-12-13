using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Unit
{
    public new const TileSet.Units unitName = TileSet.Units.pawn;
    private bool[,] attacks = new bool[8, 8];


    override public bool[,] GetMoves()
    {
        int[] pos = currentTile.gridPos;
        int enemy = team == 0 ? 1 : 0;

        bool[,] moves = new bool[8, 8];
        attacks = new bool[8, 8];


        for (int xdir = -1; xdir < 2; xdir++)
        {
            for (int ydir = -1; ydir < 2; ydir++)
            {
                for (int inc = 1; inc < 9; inc++)
                {
                    int x = pos[0] + xdir * inc; int y = pos[1] + ydir * inc;
                    if (x < 0 || x > 7 || y < 0 || y > 7)
                    {
                        break;
                    }
                    if (board.GetUnit(x, y) != TileSet.Units.nil && board.GetUnit(x, y) != TileSet.Units.king)
                    {
                        if (board.GetTeam(x, y) == enemy)
                        {
                            attacks[x, y] = true;
                        }
                        break;
                    } 
                    else
                    {
                        moves[x, y] = true;
                    }
                }
            }
        }
        return moves;
    }

    override public bool[,] GetAttacks()
    {
        GetMoves();
        return attacks;
    }
}