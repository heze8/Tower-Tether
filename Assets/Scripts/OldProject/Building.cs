using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : GridObject
{
   private BuildingType _buildingType;
   public GameObject unitToSpawn;
   protected int countToSpawn;
   private int count;
   
   public void Spawn()
   {
       count--;
       if (count < 0)
       {
          count = countToSpawn;
          GamePlane.Instance.SpawnUnit(new Vector2(xPos, yPos), _direction, unitToSpawn);
       }
   }
   
   
}



public enum BuildingType
{
   Spawner
   
}
