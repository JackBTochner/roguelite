using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour
{
    public float time;
    public bool destroy = true;

    void OnEnable()
    {
        if (destroy)
        {
            Destroy(gameObject, time);
        }
        else
        {
            Invoke("Delete", time);
        }
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }
}
