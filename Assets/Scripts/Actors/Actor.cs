using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public static Actor Instance;

    public bool staticUnique = false;

    void Awake()
    {
        if(staticUnique)
        {
            if(Instance == null)
            {
                Instance = this;
            } else 
            {
                Destroy(gameObject);
            }
        }
    }
}
