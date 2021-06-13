﻿using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Vector2 mapSize = new Vector2(20, 20);
    public int startingActionPoints = 3;
    public ActionPoints actionPoints;

    private void Start()
    {
        actionPoints.startingPoints = startingActionPoints;
    }
}