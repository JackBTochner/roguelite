using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using DG.Tweening;

public class StoreRoomZone : MonoBehaviour
{
    public GameObject bedroom;
    public float fallTime;
    public float fallDistance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 bedroomposition = bedroom.transform.position;
            if(bedroomposition.y == 0)
            {
                Transform storetransform = bedroom.transform;
                storetransform.DOMove(storetransform.position - new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
            }

            if(bedroomposition.y == -100)
            {
                Transform storetransform = bedroom.transform;
                storetransform.DOMove(storetransform.position + new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
            }
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        OnTriggerEnter(other);
    //    }
    //}


}
