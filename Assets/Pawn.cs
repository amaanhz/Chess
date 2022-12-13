using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Unit
{
    public new const TileSet.Units unitName = TileSet.Units.pawn;


    override public bool[,] GetMoves()
    {
        int[] pos = currentTile.gridPos;
        int dir = team == 0 ? 1 : -1; // forward for the unit, relative to the board and team
        int startRow = team == 0 ? 1 : 6;

        bool[,] moves = new bool[8, 8];

        if (pos[1] + dir > -1 && pos[1] + dir < 8)
        {
            if (board.GetUnit(pos[0], pos[1] + dir) == TileSet.Units.nil)
            {
                moves[pos[0], pos[1] + dir] = true;
                if (pos[1] == startRow && board.GetUnit( pos[0], pos[1] + dir * 2 ) == TileSet.Units.nil)
                {
                    moves[pos[0], pos[1] + dir * 2] = true;
                }
            }
        }
        return moves;
    }

    override public bool[,] GetAttacks()
    {
        int[] pos = currentTile.gridPos;
        int dir = team == 0 ? 1 : -1;
        int enemy = team == 0 ? 1 : 0;

        int yCoord = pos[1] + dir > 0 && pos[1] + dir < 8 ? pos[1] + dir : -1;

        // Only two possible attack positions
        int[,] attackPositions = { { pos[0] - 1 >= 0 ? pos[0] - 1 : -1, yCoord }, { pos[0] + 1 <= 7 ? pos[0] + 1 : -1, yCoord } };
        bool[,] attacks = new bool[8, 8];

        for (int i = 0; i < 2; i++)
        {
            if (attackPositions[i, 0] != -1)
            {
                if (board.GetUnit(attackPositions[i, 0], attackPositions[i, 1]) != TileSet.Units.nil)
                {
                    if (board.GetTeam(attackPositions[i, 0], attackPositions[i, 1]) == enemy)
                    {
                        attacks[attackPositions[i, 0], attackPositions[i, 1]] = true;
                    }
                }
            }
        }
        return attacks;
    }
}