using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject tile;
    MeshRenderer mesh;
    public static Vector3 dim;
    public BoxCollider hitbox;
    public TileSet board;

    public GameObject unit;
    public TileSet.Units unitName = TileSet.Units.nil;
    public int[] gridPos;

    public enum State
    {
        none,
        selected,
        move,
        attack
    }
    public State state;
    private static Material[] stateMats = new Material[4];

    public void Start()
    {
        tile = transform.gameObject;
        hitbox = transform.GetComponent<BoxCollider>();
        hitbox.isTrigger = true;
        mesh = tile.GetComponent<MeshRenderer>();

        if (stateMats[0] == null) // Initialise tile state materials
        {
            stateMats[0] = Resources.Load("DefaultTile") as Material;
            stateMats[1] = Resources.Load("SelectedTile") as Material;
            stateMats[2] = Resources.Load("MoveTile") as Material;
            stateMats[3] = Resources.Load("AttackTile") as Material;
        }
    }
    public void SpawnUnit(TileSet.Units unitName, int team = 0)
    {
        if (unit == null)
        {
            GameObject obj = GameObject.Instantiate(TileSet.units[(int)unitName], transform.position + new Vector3(0, dim.y / 2, 0), Quaternion.identity);
            obj.transform.Rotate(new Vector3(-90, 0, 0), Space.World);
            obj.GetComponent<Unit>().board = board;
            obj.GetComponent<Unit>().currentTile = this; obj.GetComponent<Unit>().SetTeam(team);
            obj.transform.parent = transform;
            unit = obj;
            this.unitName = unitName;
        }
        else
        {
            Debug.Log("Unit already exists at this position");
        }
    }

    public void SetUnit(GameObject newUnit)
    {
        newUnit.transform.parent = transform;
        newUnit.transform.position += TileSet.Flatten(transform.position - newUnit.transform.position);

        unit = newUnit;
        unitName = unit.GetComponent<Unit>().unitName;
        unit.GetComponent<Unit>().currentTile = this;
    }

    public bool[,] GetMoves()
    {
        return unit.GetComponent<Unit>().GetMoves();
    }

    public bool[,] GetAttacks()
    {
        return unit.GetComponent<Unit>().GetAttacks();
    }

    public void RemoveUnit()
    {
        unit = null; unitName = TileSet.Units.nil;
    }

    public void SetState(State state)
    {
        this.state = state;
        mesh.material = stateMats[(int)state];
    }
}
