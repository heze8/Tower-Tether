using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Plane plane;
    private Camera mainCam;
    private Vector3 worldPoint;

    private void Start()
    {
        plane = new Plane(Vector3.forward, 0);
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
             float distance;
             var ray = mainCam.ScreenPointToRay(Input.mousePosition);
                 
             if (plane.Raycast(ray, out distance))
             {
                 worldPoint = ray.GetPoint(distance);
             }
         // get the collision point of the ray with the z = 0 plane
            worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            GridManager.Instance.SetBuilding(worldPoint);
        }
    }
}
