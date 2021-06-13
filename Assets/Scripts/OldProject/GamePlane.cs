using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlane : Singleton<GamePlane>
{        
    public GameObject collideEffect;

    public class Tile
    {
        public GridObject gridBuilding;
        public List<GridObject> _collisionHandler;
        private bool hasBuilding = false;

        public void AddGridObject(GridObject gridObject)
        {
            if (gridObject is Building)
            {
                Debug.Log("added building");
                hasBuilding = true;
                this.gridBuilding = gridObject;
            }
            else
            {
                _collisionHandler.Add(gridObject);
            }
        }

        public void HandleCollision()
        {
            bool unitLost = false;
            bool buildingLost = false;
            foreach (GridObject g in _collisionHandler)
            {
                if (g is Unit)
                {
                    if (hasBuilding)
                    {
                        Debug.Log("collide with building");
                        Instantiate(GamePlane.Instance.collideEffect, g.transform.position, g.transform.rotation);
                        var swapDirection = Unit.SwapDirection(g._direction);
                        g.SetDirection(swapDirection);
                        unitLost = g.LoseHp();
                        buildingLost = gridBuilding.LoseHp();
                    } 
                    else if (_collisionHandler.Count > 1)
                    {
                        Debug.Log("collide with block");
                        Instantiate(GamePlane.Instance.collideEffect, g.transform.position, g.transform.rotation);
                        unitLost = g.LoseHp();
                        var swapDirection = Unit.SwapDirection(g._direction);
                        g.SetDirection(swapDirection);
                    } 
                    else if (_collisionHandler.Count == 1)
                    {
                        //gridObject = g;
                    }
                }
                
                if (unitLost)
                {
                    GamePlane.Instance._units.Remove((Unit) g);
                    unitLost = false;
                }
                if (buildingLost)
                {
                    GamePlane.Instance._buildings.Remove((Building) gridBuilding);
                    gridBuilding = null;
                    hasBuilding = false;
                    buildingLost = false;
                }
            }
            _collisionHandler.Clear();

            
        }

        public GridObject RemoveUnitObject()
        {
            if (!hasBuilding)
            {
                var g = gridBuilding;
                gridBuilding = null;
                return g;
            }
            else
            {
                return null;
            }
        }

        public bool HasBuilding()
        {
            return gridBuilding != null && hasBuilding;
        }

        public Tile()
        {
            _collisionHandler = new List<GridObject>();
        }
    }

    private Tile[,] _gameGrid;
    private List<Building> _buildings;
    private List<Unit> _units;

    private int planeScale = 10;
    private float planeLength;
    public int length = 20;
    public float _gameSpeed = 1;
    public float _gameTick;
    public Color colorLine;

    #region Init

    // Start is called before the first frame update
    void Start()
    {
        _gameTick = (15f / length) / _gameSpeed; 
        _gameGrid = new Tile[length, length];
        
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                _gameGrid[i,j] = new Tile();
                
            }
        }

        var f = 1 / ((float) length / 10);
        GetComponent<Grid>().cellSize = new Vector3(f,f,f);
        
        _buildings = new List<Building>();
        _units = new List<Unit>();
        planeLength = gameObject.transform.localScale.x * planeScale; //assuming square
        CreateGrid();
        StartCoroutine(GameTick());
    }
    
    

    private void CreateGrid()
    {
        var radius = planeLength / 2;
        var origin = gameObject.transform.position - new Vector3(radius, 0, radius);
        
        var boxLength = planeLength / length;
        
        for (int i = 0; i < length + 1; i++)
        {
            var startPtX = origin + new Vector3(i * boxLength, 0 , 0);
            var startPtZ = origin + new Vector3(0, 0 , i * boxLength);

            CreateLine(startPtX, startPtX + new Vector3(0, 0, planeLength));
            CreateLine(startPtZ, startPtZ + new Vector3(planeLength, 0, 0));

        }
    }

    private void CreateLine(Vector3 origin, Vector3 end)
    {
        var o = new GameObject("Line");
        o.transform.SetParent(gameObject.transform);
        LineRenderer lineRenderer = o.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.endColor = colorLine;
        lineRenderer.startColor = colorLine;
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[] { origin, end});
    }
    
    #endregion

    IEnumerator GameTick()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                _gameGrid[i,j].HandleCollision();
            }
        }
        foreach (var unit in _units)
        {
            Unit.Direction direction = unit.Move();
            
            MoveTo(unit, direction);
        }
        
        foreach (var bui in _buildings)
        {
            bui.Spawn();
        }
        yield return new WaitForSeconds(_gameTick);
        StartCoroutine(GameTick());
    }
    
    private void MoveTo(Unit unit, Unit.Direction direction)
    {
        Vector2 directVector = Unit.GetDirectionVector(direction);
        var unitXPos = unit.xPos + (int) directVector.x;
        var unitYPos = unit.yPos + (int) directVector.y;
        
        //xPos is the pos next tick lol
        //Out of bounds
        var left = unitXPos < 0;
        var right = unitXPos >= length;
        var bot = unitYPos < 0;
        var top = unitYPos >= length;
        
        if (left || right || bot || top)
        {
            
            //will automaticically swap
            Unit.WallType wallType = left ? Unit.WallType.Left :
                right ? Unit.WallType.Right :
                bot ? Unit.WallType.Bottom: Unit.WallType.Top;

            if (left && top)
            {
                wallType = Unit.WallType.TopLeftCorner;
            }
            if (left && bot)
            {
                wallType = Unit.WallType.BottomLeftCorner;
            }
            if (right && top)
            {
                wallType = Unit.WallType.TopRightCorner;
            }
            if (right && bot)
            {
                wallType = Unit.WallType.BottomRightCorner;
            }
            
            
            var swapDirection = Unit.SwapDirection(direction, wallType);
            unit.SetDirection(swapDirection);
            directVector = Unit.GetDirectionVector(swapDirection);
            unitXPos = unit.xPos + (int) directVector.x;
            unitYPos = unit.yPos + (int) directVector.y;
        }

        // if (_gameGrid[unitXPos, unitYPos].HasObject())
        // {
        //     //will automaticically swap
        //     var swapDirection = Unit.SwapDirection(direction);
        //     unit.SetDirection(swapDirection);
        //     directVector = Unit.GetDirectionVector(swapDirection);
        //     unitXPos = unit.xPos + (int) directVector.x;
        //     unitYPos = unit.yPos + (int) directVector.y;
        // }

        _gameGrid[unit.xPos, unit.yPos].RemoveUnitObject();

        unit.xPos = unitXPos;
        unit.yPos = unitYPos;
        try
        {        
            var tile = _gameGrid[unitXPos, unitYPos];
            tile.AddGridObject(unit);
        }
        catch (Exception e)
        {
            Debug.Log(unit.xPos + " " + unit.yPos + " " + directVector);
            throw;
        }
    }

    
    private void AddGridObject(GridObject gridObject)
    {
        var tile = _gameGrid[gridObject.xPos, gridObject.yPos];
        tile.AddGridObject(gridObject);
    }
    
    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }
    
    public void AddBuilding(Building building)
    {
        _buildings.Add(building);
        AddGridObject(building);
    }

    public void SpawnUnit(Vector2 pos, Unit.Direction direction, GameObject unit)
    {
        Vector3 spawnLocation = GetSpawnLocation(Utility.ConvertGridPos(pos, true));
        var u = Instantiate(unit, spawnLocation, Quaternion.identity);
        u.transform.eulerAngles = Unit.GetRotationVector(direction);

        var component = u.GetComponent<Unit>();
        if (component == null)
        {
            Debug.LogError("Not a unit");
        }
        component.SetDirection(direction);
        component.SetPos(pos);
        AddUnit(component);
        MoveTo(component, direction);
        
    }

    /*
     * Used with the controls
     */
    public void SpawnBuilding(Vector2 pos, Unit.Direction direction, GameObject building)
    {
        if (pos.x < 0 || pos.x >= length || pos.y < 0 || pos.y >= length)
        {
            return;
        }

        if (_gameGrid[(int) pos.x, (int) pos.y].HasBuilding())
        {
            Debug.Log("Spot used");
            return;
        }

        Vector3 spawnLocation = GetSpawnLocation(Utility.ConvertGridPos(pos, true));

        var u = Instantiate(building, spawnLocation, Quaternion.identity);
      
        var component = u.GetComponent<Building>();
        if (component == null)
        {
            Debug.LogError("Not a building");
        }
        component.SetDirection(direction);
        component.SetPos(pos);
        AddBuilding(component);
    }

    /*
     * Uses the ray tracing vector 2 pos
     */
    public Vector3 GetSpawnLocation(Vector2 pos)
    {
        
        var radius = planeLength / 2;
        var origin = gameObject.transform.position;
        var gridOnOneSide= length / 2;
        var lengthPerHalfGrid = (radius / gridOnOneSide) /2;
        Vector3 spawnPoint = new Vector3(origin.x + pos.x * radius / gridOnOneSide + lengthPerHalfGrid,0.01f, 
            origin.z + pos.y * radius / gridOnOneSide + lengthPerHalfGrid);
        return spawnPoint;
    }

}
