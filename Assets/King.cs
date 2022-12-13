using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Unit
{
    public new const TileSet.Units unitName = TileSet.Units.king;
    public bool check = false;

    private bool[,] attacks = new bool[8, 8];


    public void Awake()
    {
        isKing = true;
    }

    override public bool[,] GetMoves()
    {
        int[] pos = currentTile.gridPos;
        int enemy = team == 0 ? 1 : 0;

        attacks = new bool[8, 8];

        bool[,] moves = new bool[8, 8];

        int[,,] adjacent = TileSet.adjacentTiles(pos);
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (adjacent[row, col, 0] > -1)
                {
                    if (board.GetUnit(adjacent[row, col, 0], adjacent[row, col, 1]) != TileSet.Units.nil)
                    {
                        if (board.GetTeam(adjacent[row, col, 0], adjacent[row, col, 1]) == enemy)
                        {
                            attacks[adjacent[row, col, 0], adjacent[row, col, 1]] = true;
                        }
                    }
                    else
                    {
                        moves[adjacent[row, col, 0], adjacent[row, col, 1]] = true;
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
