using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }
    public Vector3 screenPos;


    // Update is called once per frame
    void Update()
    {
        screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane + 1;
        Pointerposition = screenPos;
        transform.right = (Pointerposition - (Vector2)transform.position).normalized;
    }


}
