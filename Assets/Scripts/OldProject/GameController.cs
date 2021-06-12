// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class GameController : MonoBehaviour
// {
//     public Grid grid;
//     public GameObject testBuilding;
//     private GameObject transBuilding;
//     public Unit.Direction Direction;
//
//     void Start()
//     {
//         transBuilding = Instantiate(testBuilding);
//         var renderer = transBuilding.GetComponentInChildren<Renderer>();
//         Color color = renderer.material.color;
//         color.a = 0.5f;
//         renderer.material.color = color;
//     }
//
//     int mod(int k, int n)
//     {
//         return ((k %= n) < 0) ? k+n : k;
//     }
//     
//     // Update is called once per frame
//     void Update()
//     {
//         Vector3 worldPoint = new Vector3();// = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         //Plane plane = new Plane(Vector3.up, 0);
//
//         Plane plane = new Plane(Vector3.up, 0);
//         float distance;
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//             
//         if (plane.Raycast(ray, out distance))
//         {
//             worldPoint = ray.GetPoint(distance);
//         }
//         // get the collision point of the ray with the z = 0 plane
//         //Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
//
//         Vector3 position = grid.WorldToCell(worldPoint);
//         var pos2 = new Vector2(position.x, position.z);
//         position = GamePlane.Instance.GetSpawnLocation(pos2);
//         transBuilding.transform.position = position;
//         transBuilding.transform.eulerAngles = Unit.GetRotationVector(Direction);
//         
//         if (Input.GetButtonDown("Left"))
//         {
//             Direction = (Unit.Direction) (mod((int) Direction - 1,  8)); // magic
//         }
//         if (Input.GetButtonDown("Right"))
//         {
//             Direction = (Unit.Direction) (mod((int) Direction + 1,  8)); // magic
//         }
//         
//         if (Input.GetButtonDown("Click"))
//         {
//             // get the grid by GetComponent or saving it as public field
//             // save the camera as public field if you using not the main camera
//             
//             GamePlane.Instance.SpawnBuilding(Utility.ConvertGridPos(pos2, false), Direction, testBuilding);
//         }
//     }
// }
