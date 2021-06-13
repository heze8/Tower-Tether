using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Vector2 mapSize = new Vector2(20, 20);
    public int startingActionPoints = 3;
    public ActionPoints actionPoints;
    public int baseHp = 70;
    public float blobAttackRate = 0.4f;

    private void Start()
    {
        actionPoints.points = startingActionPoints;
    }
}