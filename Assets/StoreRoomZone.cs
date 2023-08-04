using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using DG.Tweening;

public class StoreRoomZone : MonoBehaviour
{
    public TransformAnchor playerTransformAnchor = default;
    public GameObject bedroom;
    public float fallTime;
    public float fallDistance;
    private bool isGoingUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 bedroomposition = bedroom.transform.position;
            if (bedroomposition.y == 0 && playerTransformAnchor.Value.position.x > -11.40f)
            {
                isGoingUp = false;
                Debug.Log("isGoingUp set to false");
                Transform storetransform = bedroom.transform;
                storetransform.DOMove(storetransform.position - new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
            }

            else if (bedroomposition.y == -100)
            {
                isGoingUp = true;
                Debug.Log("isGoingUp set to true");
                Transform storetransform = bedroom.transform;
                storetransform.DOMove(storetransform.position + new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
            }
            else if (bedroomposition.y < 0 && bedroomposition.y > -100 && isGoingUp == false)
            {
                isGoingUp = true;
                DOTween.Kill(bedroom.transform);
                bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 0, bedroom.transform.position.z), fallTime).SetEase(Ease.OutQuint);
            }
            else if (bedroomposition.y < 0 && bedroomposition.y > -100 && isGoingUp == true)
            {
                isGoingUp = false;
                DOTween.Kill(bedroom.transform);
                bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, -100, bedroom.transform.position.z), fallTime).SetEase(Ease.OutQuint);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            if (playerTransformAnchor.Value.position.x > -11.40f)
            {
                if (other.CompareTag("Player"))
                {
                    Vector3 bedroomposition = bedroom.transform.position;
                    if (bedroomposition.y == 0 && playerTransformAnchor.Value.position.x > -11.40f)
                    {
                        isGoingUp = false;
                        Transform storetransform = bedroom.transform;
                        storetransform.DOMove(storetransform.position - new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
                    }
                    else if (bedroomposition.y >= -100 && playerTransformAnchor.Value.position.x > -11.40f && isGoingUp == true)
                    {
                        isGoingUp = false;
                        DOTween.Kill(bedroom.transform);
                        bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, -100, bedroom.transform.position.z), fallTime).SetEase(Ease.OutQuint);
                    }
                    else if (bedroomposition.y >= -100 && playerTransformAnchor.Value.position.x > -11.40f && isGoingUp == false)
                    {
                        isGoingUp = true;
                        DOTween.Kill(bedroom.transform);
                        bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 0, bedroom.transform.position.z), fallTime).SetEase(Ease.OutQuint);
                    }
                }
            }
        }
    }
}
