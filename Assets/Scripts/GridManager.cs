using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : Singleton<GridManager>
{
    private Tilemap _tileMap;

    public TileBase[] _tileBases;

    public List<Tower> towers;
    
    // Start is called before the first frame update
    void Start()
    {
        _tileMap = GetComponent<Tilemap>();
        towers = new List<Tower>();
    }

    public void SetBuilding(Vector3 coordinate, Tower tile)
    {
        var cellPos = _tileMap.WorldToCell(coordinate);
        // var positions = new Vector3Int[4];
        // positions[0] = cellPos;
        // positions[1] = cellPos.AddVector(new Vector2Int(1, 0));
        // positions[2] = cellPos.AddVector(new Vector2Int(0, 1));
        // positions[3] = cellPos.AddVector(new Vector2Int(1, 1));
        //
        // _tileMap.SetTiles(positions, _tileBases);
        
        _tileMap.SetTile(cellPos, tile);
        var tower = _tileMap.GetTile<Tower>(cellPos);
        tower.pos = new Vector2(cellPos.x, cellPos.y);
        towers.Add(tower);
    }
    

    
    // Update is called once per frame
    void Update()
    {
        foreach (var t in towers)
        {
            t.Update();
        }
    }
}
