using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using DG.Tweening; //Animation engine for Unity

public class BedRoomZone : MonoBehaviour
{
    // When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    public TransformAnchor playerTransformAnchor = default;
    // The object that we want to fall, so this is the bedroom zone, we want move the storefront here.
    public GameObject storefront;
    // The speed time when falling.
    public float fallTime;
    // The distance the storefront falls. "Test": have to test, if the storefront is at this point, our camera should not see storefront anymore.
    public float fallDistance;
    // We need to know the storefront is moving up or down. up is true, down is false.
    private bool isGoingUp = false;

    //When the object collider touches something.
    private void OnTriggerEnter(Collider other)
    {
        // When the object collider touches the "Player".
        if (other.CompareTag("Player"))
        {
            // Get the position of the store front.
            Vector3 storeroomposition = storefront.transform.position;
            // When the storefront is 0 and the player is lower than the position.
            // This will not be used because the store front will always be at the bottom, if the player is in the bedroom - 
            // - and not touching the collider yet.
            if(storeroomposition.y == 0 && playerTransformAnchor.Value.position.x < -14.45f)
            {
                //Storefront will be falling, so set to false.
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
                DOTween.Kill(storefront.transform);
                storefront.transform.DOMove(new Vector3(storefront.transform.position.x, 0, storefront.transform.position.z), fallTime).SetEase(Ease.OutQuint);
            }
            else if (storeroomposition.y < 0 && storeroomposition.y > -100 && isGoingUp == true)
            {
                isGoingUp = false;
                DOTween.Kill(storefront.transform);
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
                        DOTween.Kill(storefront.transform);
                        storefront.transform.DOMove(new Vector3(storefront.transform.position.x, -100, storefront.transform.position.z), fallTime).SetEase(Ease.OutQuint);
                    }
                    else if (storeroomposition.y >= -100 && playerTransformAnchor.Value.position.x < -14.45f && isGoingUp == false)
                    {
                        isGoingUp = true;
                        DOTween.Kill(storefront.transform);
                        storefront.transform.DOMove(new Vector3(storefront.transform.position.x, 0, storefront.transform.position.z), fallTime).SetEase(Ease.OutQuint);
                    }
                }
            }
        }
    }
}