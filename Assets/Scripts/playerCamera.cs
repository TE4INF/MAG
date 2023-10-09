using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; 

    // Update is called once per frame
    void Update()
    {
        // Moves the camera to the players position plus an offset
        transform.position = player.position + offset;
    }
}
