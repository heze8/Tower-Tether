using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public int hp = 1;
    public Unit.Direction _direction;
    public Team _team;


    public void SetPos(Vector2 pos)
    {
        xPos = (int) pos.x;
        yPos = (int) pos.y;
    }
    
    public void SetTeam(Team team)
    {
        _team = team;
    }

    public bool LoseHp(int hpLoss = 1)
    {
        if (--hp <= 0)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
    
    public void SetDirection(Unit.Direction direction)
    {
        _direction = direction;
        gameObject.transform.eulerAngles = Unit.GetRotationVector(direction);
    }
}
