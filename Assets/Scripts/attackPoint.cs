using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackPoint : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public float radius = 1f;
    public float offset = 1f;
    Plane plane = new Plane(Vector3.down, 0);
    public GameObject player123;
    private Vector3 localPos;


    void Start()
    {
        localPos = transform.localPosition;
    }
    void Update()
    {

        screenPosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }



        Vector3 direction = transform.InverseTransformPoint(worldPosition) - localPos;
        direction = Quaternion.Euler(0, -180, 0) * direction;


        Vector3 newPos = localPos - (direction.normalized * radius);

        transform.localPosition = new Vector3(newPos.x, offset, newPos.z);
    }
}
