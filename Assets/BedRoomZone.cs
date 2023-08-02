using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using DG.Tweening;

public class BedRoomZone : MonoBehaviour
{
    public TransformAnchor playerTransformAnchor = default;
    public GameObject storefront;
    public float fallTime;
    public float fallDistance;
    private bool isGoingUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 storeroomposition = storefront.transform.position;
            if(storeroomposition.y == 0 && playerTransformAnchor.Value.position.x < -14.45f)
            {
                isGoingUp = false;
                Debug.Log("isGoingUp set to false");
                Transform storetransform = storefront.transform;
                storetransform.DOMove(storetransform.position - new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
            }

            else if(storeroomposition.y == -100)
            {
                isGoingUp = true;
                Debug.Log("isGoingUp set to true");
                Transform storetransform = storefront.transform;
                storetransform.DOMove(storetransform.position + new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
            }
            else if (storeroomposition.y < 0 && storeroomposition.y > -100 && isGoingUp == false)
            {
                isGoingUp = true;
                DOTween.Kill(storefront.transform); // Interrupt any ongoing tween
                storefront.transform.DOMove(new Vector3(storefront.transform.position.x, 0, storefront.transform.position.z), fallTime).SetEase(Ease.OutQuint);
            }
            else if (storeroomposition.y < 0 && storeroomposition.y > -100 && isGoingUp == true)
            {
                isGoingUp = false;
                DOTween.Kill(storefront.transform); // Interrupt any ongoing tween
                storefront.transform.DOMove(new Vector3(storefront.transform.position.x, -100, storefront.transform.position.z), fallTime).SetEase(Ease.OutQuint);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            if (playerTransformAnchor.Value.position.x < -14.45f)
            {
                if (other.CompareTag("Player"))
                {
                    Vector3 storeroomposition = storefront.transform.position;
                    if (storeroomposition.y == 0 && playerTransformAnchor.Value.position.x < -14.45f)
                    {
                        isGoingUp = false;
                        Transform storetransform = storefront.transform;
                        storetransform.DOMove(storetransform.position - new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
                    }
                    else if (storeroomposition.y >= -100 && playerTransformAnchor.Value.position.x < -14.45f && isGoingUp == true)
                    {
                        isGoingUp = false;
                        DOTween.Kill(storefront.transform); // Interrupt any ongoing tween
                        storefront.transform.DOMove(new Vector3(storefront.transform.position.x, -100, storefront.transform.position.z), fallTime).SetEase(Ease.OutQuint);
                    }
                    else if (storeroomposition.y >= -100 && playerTransformAnchor.Value.position.x < -14.45f && isGoingUp == false)
                    {
                        isGoingUp = true;
                        DOTween.Kill(storefront.transform); // Interrupt any ongoing tween
                        storefront.transform.DOMove(new Vector3(storefront.transform.position.x, 0, storefront.transform.position.z), fallTime).SetEase(Ease.OutQuint);
                    }
                }
            }
        }
    }
}