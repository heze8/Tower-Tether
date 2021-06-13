using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class GridManager : Singleton<GridManager>
{
    private Tilemap _tileMap;

    public Wall _wall;
    public List<Tower> _Towers;

    public List<Tower> towers;
    public NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        _tileMap = GetComponent<Tilemap>();
        towers = new List<Tower>();
    }

    public bool WithinRange(Vector2 pos)
    {
        var range = GameManager.Instance.mapSize;
            
        if (!(pos.x > range.x || pos.x < -range.x))
        {
            return true;
        }
                
        if (!(pos.y > range.y || pos.y < -range.y))
        {
            return true;
        }
        return false;
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
        var existingTile =_tileMap.GetTile<TileBase>(cellPos);
        if (existingTile is IObstacle)
        {
            return;
        }

        Tower instantiate = Instantiate(tile);
        
        _tileMap.SetTile(cellPos, instantiate);
        var tower= _tileMap.GetTile<Tower>(cellPos);
        var towerPos = new Vector2(cellPos.x, cellPos.y);
        tower.pos = towerPos;
        var min = math.INFINITY;
        Tower closestT = null;
        foreach (Tower t in towers)
        {
            var sqrMagnitude = (t.pos - towerPos).sqrMagnitude;
            if (sqrMagnitude < min)
            {
                min = sqrMagnitude;
                closestT = t;
            }
        }

        if (closestT)
        {
            var destination = new Vector2Int(Mathf.RoundToInt(closestT.pos.x), Mathf.RoundToInt(closestT.pos.y));
            var direction = (closestT.pos - towerPos).normalized;

            Vector2Int v = new Vector2Int(Mathf.RoundToInt(towerPos.x), Mathf.RoundToInt(towerPos.y));
            int i = 0;
            while (true)
            {
                i++;
                var route = direction * i;
                var end = v + new Vector2Int(Mathf.RoundToInt(route.x), Mathf.RoundToInt(route.y));
                
                
                
                if (end == destination)
                {
                    break;
                }
                _tileMap.SetTile(new Vector3Int(end.x, end.y,0), _wall);

            }
            if (closestT.level == tower.level)
            {
                closestT.LevelUp();
                Debug.Log(closestT.level);

                tower.LevelUp();
            }
            
        }

        EnemySpawningSystem.Instance.CheckRoutes();
        towers.Add(tower);
        tower.Start();

    }
    

    
    // Update is called once per frame
    void Update()
    {
        foreach (var t in towers)
        {
           // t.Update();
        }
    }
}
