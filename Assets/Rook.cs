using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Unit
{
    public new const TileSet.Units unitName = TileSet.Units.rook;
    private bool[,] attacks = new bool[8, 8];

    public override bool[,] GetMoves()
    {
        int[] pos = currentTile.gridPos;
        bool[,] moves = new bool[8, 8]; bool[,] attacks = new bool[8, 8];

        int enemy = team == 0 ? 1 : 0;

        // track indexes


        // Horizontal loop
        for (int dir = -1; dir < 2; dir += 2)
        {
            for (int x = pos[0] + dir; x > -1 && x < 8; x += dir)
            {
                if (board.GetUnit(x, pos[1]) == TileSet.Units.nil)
                {
                    moves[x, pos[1]] = true;
                }
                else
                {
                    if (board.GetTeam(x, pos[1]) == enemy && board.GetUnit(x, pos[1]) != TileSet.Units.king)
                    {
                        attacks[x, pos[1]] = true;
                    }
                    break;
                }
            }
        }

        // Vertical loop
        for (int dir = -1; dir < 2; dir += 2)
        {
            for (int y = pos[1] + dir; y > -1 && y < 8; y += dir)
            {
                if (board.GetUnit(pos[0], y) == TileSet.Units.nil)
                {
                    moves[pos[0], y] = true;
                }
                else
                {
                    if (board.GetTeam(pos[0], y) == enemy && board.GetUnit(pos[0], y) != TileSet.Units.king)
                    {
                        moves[pos[0], y] = true;
                    }
                    break;
                }
            }
        }
        return moves;
    }

    override public bool[,] GetAttacks()
    {
        GetMoves(); // refresh the attacks list
        return attacks;
    }
}
