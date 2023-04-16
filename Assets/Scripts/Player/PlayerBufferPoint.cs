using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBufferPoint : MonoBehaviour
{
    public bool showDebug;
    public Material debugMaterial;

    private Rigidbody rb;
    public Collider groundCollider;

    public Transform playerBuffer;

    private void Update()
    {
        Vector3 closestOnGround = groundCollider.ClosestPoint(transform.position);

        playerBuffer.transform.position = closestOnGround;

        if (showDebug)
        {
            LeaveTrail(closestOnGround, .1f, debugMaterial);
            playerBuffer.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void LeaveTrail(Vector3 point, float scale, Material mat)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Collider>().enabled = false;
        sphere.GetComponent<Renderer>().material = mat;
        sphere.transform.localScale = Vector3.one * scale;
        sphere.transform.position = point;
        Destroy(sphere, 2f);
    }

}
