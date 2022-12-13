using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour
{
    [SerializeField]
    private GameObject tileObj;
    public enum Units
    {
        pawn, rook, knight, bishop, queen, king, nil
    }
    public static GameObject[] units = new GameObject[6];

    public Unit[] whiteTeam = new Unit[16];
    public Unit[] blackTeam = new Unit[16];

    private GameObject[,] tiles = new GameObject[8, 8];

    private Tile selectedTile;



    public static Vector3 Flatten(Vector3 vec)
        {
            return new Vector3(vec.x, 0, vec.z);
        }

    public static int[,,] adjacentTiles(int[] pos)
    {
        int[,,] adjacent = new int[3, 3, 2] { { { -1, -1 }, { -1, -1 }, { -1, -1 } },
                                              { { -1, -1 }, { -1, -1 }, { -1, -1 } },
                                              { { -1, -1 }, { -1, -1 }, { -1, -1 } } };

        int maxYOffset = pos[1] < 7 ? 2 : 1;
        int maxXOffset = pos[0] < 7 ? 2 : 1;

        for (int y = pos[1] > 0 ? -1 : 0; y < maxYOffset; y++)
        {
            for (int x = pos[0] > 0 ? -1 : 0; x < maxXOffset; x++)
            {
                if (y == 0 && x == 0)
                {
                    continue;
                }
                adjacent[1 + y, 1 + x, 0] = pos[0] + x; adjacent[1 + y, 1 + x, 1] = pos[1] + y;
            }
        }
        return adjacent;
    }



    public bool tileInside(Tile tile, int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i, 0] == tile.gridPos[0] && array[i, 1] == tile.gridPos[1])
            {
                return true;
            }
        }
        return false;
    }

    public Units GetUnit(int x, int y)
    {
        return tiles[x, y].GetComponent<Tile>().unitName;
    }

    public int GetTeam(int x, int y)
    {
        return tiles[x, y].GetComponent<Tile>().unit.GetComponent<Unit>().team;
    }

    public void selectTile(Tile tile)
    {

        if (selectedTile != null)
        {
            if (tile.state == Tile.State.move)
            {
                GameObject unit = selectedTile.unit;

                selectedTile.RemoveUnit();
                tile.SetUnit(unit);
                deSelect();
            }
            else
            {
                deSelect();
                selectTile(tile);
            }
        }
        else
        {
            selectedTile = tile;
            
            if (selectedTile.unitName != Units.nil)
            {
                //moveTiles = tile.GetMoves(); attackTiles = tile.GetAttacks();

                bool[,] temp = tile.GetMoves();
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if (temp[x, y]) 
                        {
                            tiles[x, y].GetComponent<Tile>().SetState(Tile.State.move);
                        }
                    }
                }
                temp = tile.GetAttacks();
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if (temp[x, y])
                        {
                            tiles[x, y].GetComponent<Tile>().SetState(Tile.State.attack);
                        }
                    }
                }
                tile.SetState(Tile.State.selected);
            }
        }
    }

    public void deSelect()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                tiles[x, y].GetComponent<Tile>().SetState(Tile.State.none);
            }
        }

        selectedTile = null;
    }

    private void reCheck()
    {
        
    }

    private void Start()
    {
        units[0] = Resources.Load("pawn") as GameObject;
        units[1] = Resources.Load("rook") as GameObject;
        units[4] = Resources.Load("queen") as GameObject;
        units[5] = Resources.Load("king") as GameObject;
        tileObj = Resources.Load("Tile") as GameObject;

        //grid = transform.gameObject.GetComponent<ChessUI>();

        Tile.dim = Game.yUp(tileObj.GetComponent<BoxCollider>().size);
        Vector3 topleft = transform.position + new Vector3(-Tile.dim.x * 3.5f, 0, Tile.dim.z * 3.5f);

        Vector3 increment = Flatten(Tile.dim); increment.z *= -1;
        Vector3 pos = topleft;

        for (int x = 0; x < 8; x++)
        {
            for (int z = 0; z < 8; z++)
            {
                tiles[x, z] = GameObject.Instantiate(tileObj); tiles[x, z].transform.position = pos;
                tiles[x, z].transform.SetParent(transform, true);
                tiles[x, z].GetComponent<Tile>().gridPos = new int[2] { x, z };
                tiles[x, z].GetComponent<Tile>().board = this;
                pos.z += increment.z;
            }
            pos.x += increment.x;
            pos.z -= increment.z * 8;
        }


        // SPAWNING UNITS //

        tiles[0, 0].GetComponent<Tile>().SpawnUnit(Units.rook); tiles[7, 0].GetComponent<Tile>().SpawnUnit(Units.rook);
        tiles[0, 7].GetComponent<Tile>().SpawnUnit(Units.rook, 1); tiles[7, 7].GetComponent<Tile>().SpawnUnit(Units.rook, 1);

        tiles[4, 0].GetComponent<Tile>().SpawnUnit(Units.king);
        tiles[4, 7].GetComponent<Tile>().SpawnUnit(Units.king, 1);

        tiles[3, 0].GetComponent<Tile>().SpawnUnit(Units.queen);
        tiles[3, 7].GetComponent<Tile>().SpawnUnit(Units.queen, 1);


        for (int x = 0; x < 8; x++)
        {
            tiles[x, 1].GetComponent<Tile>().SpawnUnit(Units.pawn);
        }
        for (int x = 0; x < 8; x++)
        {
            tiles[x, 6].GetComponent<Tile>().SpawnUnit(Units.pawn, 1);
        }

    }
    private void Update()
    {
        
    }
}
