using System;
using UnityEngine;

public partial class Unit {
    [Serializable]
    public enum Direction
    {
        Left,
        UpperLeft,
        Up,
        UpperRight,
        Right,
        LowerRight,
        Down,
        LowerLeft
    }
    public static Vector2 GetDirectionVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return new Vector2(-1, 0);
            case Direction.Right:
                return new Vector2(1, 0);
            case Direction.Up:
                return new Vector2(0, 1);
            case Direction.Down:
                return new Vector2(0, -1);
            case Direction.UpperLeft:
                return new Vector2(-1, 1);
            case Direction.UpperRight:
                return new Vector2(1, 1);
            case Direction.LowerLeft:
                return new Vector2(-1, -1);
            case Direction.LowerRight:
                return new Vector2(1, -1);
            default:
                return new Vector2();
        }
    }
    
    /*
     * Assumes default is facing up
     */
    public static Vector3 GetRotationVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return new Vector3(0, -90, 0);
            case Direction.Right:
                return new Vector3(0, 90, 0);
            case Direction.Up:
                return new Vector3();
            case Direction.Down:
                return new Vector3(0, -180,0);
            case Direction.UpperLeft:
                return new Vector3(0, -45,0);
            case Direction.UpperRight:
                return new Vector3(0, 45,0);
            case Direction.LowerLeft:
                return new Vector3(0, -135,0);
            case Direction.LowerRight:
                return new Vector3(0, 135,0);
            default:
                return new Vector2();
        }
    }

    public enum WallType
    {
        Nil,
        Bottom,
        Left,
        Right,
        Top,
        TopLeftCorner,
        TopRightCorner,
        BottomLeftCorner,
        BottomRightCorner
        
    }
    public static Direction SwapDirection(Direction direction, WallType wallType = WallType.Nil)
    {
        switch (direction)
        {
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.UpperLeft:
                return (wallType == WallType.TopLeftCorner)? Direction.LowerRight 
                    : (wallType == WallType.Left)? Direction.UpperRight: Direction.LowerLeft;
            case Direction.UpperRight:
                return (wallType == WallType.TopRightCorner)? Direction.LowerLeft
                    : (wallType == WallType.Right)? Direction.UpperLeft: Direction.LowerRight; // does not work for all walls
            case Direction.LowerLeft:
                return (wallType == WallType.BottomLeftCorner)? Direction.UpperRight
                    : (wallType == WallType.Left)? Direction.LowerRight: Direction.UpperLeft;
            case Direction.LowerRight:
                return (wallType == WallType.BottomRightCorner)? Direction.UpperLeft
                    : (wallType == WallType.Right) ? Direction.LowerLeft : Direction.UpperRight;
            
            default:
                return Direction.Right;
        }
    }
}