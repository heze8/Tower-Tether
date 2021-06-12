using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Unit : GridObject
{
    public GameObject _unitGameObject;
    public void Start()
    {
        _unitGameObject = gameObject;
    }

    public void Update()
    {
        var gameSpeed = GamePlane.Instance._gameSpeed;
        var directionVector = GetDirectionVector(_direction);
        transform.Translate(gameSpeed * new Vector3(directionVector.x, 0, directionVector.y) * Time.deltaTime, GamePlane.Instance.gameObject.transform);
    }
    

    public virtual Direction Move()
    {
        return _direction;
    }
}
