using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static Vector2 ConvertGridPos(Vector2 pos, bool isGridPos)
    {
        var rad = GamePlane.Instance.length / 2;
        if (isGridPos)
        {
            return new Vector2(pos.x - rad, pos.y - rad);
        }
        else
        {
            return new Vector2(pos.x + rad, pos.y + rad);
        }
    }
}
