using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Create Wall", fileName = "Wall", order = 0)]
public class Wall : Tile, IObstacle

{
    public Vector2 pos;

}