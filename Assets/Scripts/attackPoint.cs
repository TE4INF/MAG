using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class attackPoint : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public float radius = 1f;
    Plane plane = new Plane(Vector3.up, 0);

    public float zOffset1 = 2.5f;
    public float zOffset2 = 1.5f;
    private Vector3 localPos;

    void Start()
    {
        localPos = transform.localPosition;
    }
    void Update()
    {
        //Get mouse position
        screenPosition = Input.mousePosition;

        //create ray from mouse position
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        //raycast on plane and get position
        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        //get position as local position
        worldPosition = transform.parent.InverseTransformPoint(worldPosition);

        //get direction from player to mouse, normalize and multiply by radius
        Vector2 worldPosition2 = new Vector2(worldPosition.x, worldPosition.z);
        Vector2 localPos2 = new Vector2(localPos.x, localPos.z);
        localPos2 = worldPosition2 - localPos2;
        Vector3 newPos = new Vector3(localPos2.x, 0f, localPos2.y);
        newPos = newPos.normalized * radius;

        //change z value to create ellipse
        if (newPos.z < 0)
        {
            newPos.z = newPos.z * zOffset1;
        }
        else
        {
            newPos.z = newPos.z * zOffset2;
        }

        //set position to new position
        transform.localPosition = newPos;
    }
}
