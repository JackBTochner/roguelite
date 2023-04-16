using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float accel;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * accel);
    }
}
