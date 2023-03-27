using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 offset = new(0,1,0);
        Gizmos.DrawWireCube(transform.position + offset, new(1,2,1));
    }
}
