using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

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
    public static bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Where(r => r.gameObject.layer == 5).Count() > 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
        {
            if (!DeckSystem.Instance.currentTower) return;
             float distance;
             var ray = mainCam.ScreenPointToRay(Input.mousePosition);
                 
             if (plane.Raycast(ray, out distance))
             {
                 worldPoint = ray.GetPoint(distance);
             }
         // get the collision point of the ray with the z = 0 plane
            worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);

            if (GridManager.Instance.WithinRange(worldPoint))
            {
                GridManager.Instance.SetBuilding(worldPoint, DeckSystem.Instance.currentTower);
                DeckSystem.Instance.PlacedTower();
            }

        }
    }
}
