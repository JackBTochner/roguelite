using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using DG.Tweening; //Animation engine for Unity

public class StoreRoomZone : MonoBehaviour
{
    public TransformAnchor playerTransformAnchor = default;
    public GameObject bedroom;
    public float riseTime;
    public float riseDistance;
    private bool isGoingUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 bedroomposition = bedroom.transform.position;
            //if (bedroomposition.y == 0 && playerTransformAnchor.Value.position.x > 1.5f)
            //{
            //    isGoingUp = true;
            //    Transform storetransform = bedroom.transform;
            //    storetransform.DOMove(storetransform.position + new Vector3(0, riseDistance, 0), riseTime).SetEase(Ease.OutQuint);
            //}
            if (bedroomposition.y == 100)
            {
                isGoingUp = false;
                Transform storetransform = bedroom.transform;
                storetransform.DOMove(storetransform.position - new Vector3(0, riseDistance, 0), riseTime).SetEase(Ease.OutQuint);
            }
            else if (bedroomposition.y > 0 && bedroomposition.y < 100 && isGoingUp == true)
            {
                isGoingUp = false;
                DOTween.Kill(bedroom.transform);
                bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 0, bedroom.transform.position.z), riseTime).SetEase(Ease.OutQuint);
            }
            else if (bedroomposition.y > 0 && bedroomposition.y < 100 && isGoingUp == false)
            {
                isGoingUp = true;
                DOTween.Kill(bedroom.transform);
                bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 100, bedroom.transform.position.z), riseTime).SetEase(Ease.OutQuint);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Vector3 bedroomposition = bedroom.transform.position;
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            if (other.CompareTag("Player"))
            {
                if (playerTransformAnchor.Value.position.x < 3.5f)
                {
                    if (bedroomposition.y == 0)
                    {
                        isGoingUp = true;
                        Transform storetransform = bedroom.transform;
                        storetransform.DOMove(storetransform.position + new Vector3(0, riseDistance, 0), riseTime).SetEase(Ease.OutQuint);
                    }
                    else if (bedroomposition.y >= 0 && isGoingUp == true)
                    {
                        isGoingUp = false;
                        DOTween.Kill(bedroom.transform);
                        bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 0, bedroom.transform.position.z), riseTime).SetEase(Ease.OutQuint);
                    }
                    else if (bedroomposition.y >= 0 && isGoingUp == false)
                    {
                        isGoingUp = true;
                        DOTween.Kill(bedroom.transform);
                        bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 100, bedroom.transform.position.z), riseTime).SetEase(Ease.OutQuint);
                    }
                }
            }
        }
    }
    //// When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    //public TransformAnchor playerTransformAnchor = default;
    //// The object that we want to fall, so this is the storefront zone, we want move the bedroom here.
    //public GameObject bedroom;
    //// The speed time when falling.
    //public float fallTime;
    //// The distance the storefront falls. "Test": have to test, if the storefront is at this point, our camera should not see storefront anymore.
    //public float fallDistance;
    //// We need to know the storefront is moving up or down. up is true, down is false.
    //private bool isGoingUp = false;

    ////When the object collider touches something.
    //private void OnTriggerEnter(Collider other)
    //{
    //    // When the object collider touches the "Player".
    //    if (other.CompareTag("Player"))
    //    {
    //        // Get the position of the bedroom.
    //        Vector3 bedroomposition = bedroom.transform.position;
    //        // When the storefront is 0 and the player is lower than the position.
    //        // This will not be used because the bedroom always be at the bottom, if the player is in the storefront - 
    //        // - and not touching the collider yet.
    //        if (bedroomposition.y == 0 && playerTransformAnchor.Value.position.x > -11.40f)
    //        {
    //            //Bedroom will be falling, so set to false.
    //            isGoingUp = false;
    //            // Assign the transform component of the game object to a variable, this allows us to access and manipulate the - 
    //            // - position, rotation, and scale.
    //            Transform storetransform = bedroom.transform;
    //            // SetEase(Ease.OutQuint) is one type of a animation.
    //            // Use.DOMove() to choose a position to move to, and choose the speed time to move to that position.
    //            storetransform.DOMove(storetransform.position - new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
    //        }
    //        //When touches the collider and the y for bedroomposition is -100, move the object up.
    //        else if (bedroomposition.y == -100)
    //        {
    //            isGoingUp = true;
    //            Debug.Log("isGoingUp set to true");
    //            Transform storetransform = bedroom.transform;
    //            storetransform.DOMove(storetransform.position + new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
    //        }
    //        // This is one of the important code when playing the game.
    //        // If the object is moving between 0 - -100, the player touches again, it will have bug.
    //        // We need to stop the animation, then make new animation.
    //        else if (bedroomposition.y < 0 && bedroomposition.y > -100 && isGoingUp == false)
    //        {
    //            isGoingUp = true;
    //            //Stop the animation.
    //            DOTween.Kill(bedroom.transform);
    //            // New move, not + or -, using direct position to move to.
    //            bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 0, bedroom.transform.position.z), fallTime).SetEase(Ease.OutQuint);
    //        }
    //        else if (bedroomposition.y < 0 && bedroomposition.y > -100 && isGoingUp == true)
    //        {
    //            isGoingUp = false;
    //            DOTween.Kill(bedroom.transform);
    //            bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, -100, bedroom.transform.position.z), fallTime).SetEase(Ease.OutQuint);
    //        }
    //    }
    //}
    //// After leave the collider it will trigger.
    //private void OnTriggerExit(Collider other)
    //{
    //    // Check this is not null.
    //    if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
    //    {
    //        if (playerTransformAnchor.Value.position.x > -11.40f)
    //        {
    //            if (other.CompareTag("Player"))
    //            {
    //                Vector3 bedroomposition = bedroom.transform.position;
    //                if (bedroomposition.y == 0 && playerTransformAnchor.Value.position.x > -11.40f)
    //                {
    //                    isGoingUp = false;
    //                    Transform storetransform = bedroom.transform;
    //                    storetransform.DOMove(storetransform.position - new Vector3(0, fallDistance, 0), fallTime).SetEase(Ease.OutQuint);
    //                }
    //                else if (bedroomposition.y >= -100 && playerTransformAnchor.Value.position.x > -11.40f && isGoingUp == true)
    //                {
    //                    isGoingUp = false;
    //                    DOTween.Kill(bedroom.transform);
    //                    bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, -100, bedroom.transform.position.z), fallTime).SetEase(Ease.OutQuint);
    //                }
    //                else if (bedroomposition.y >= -100 && playerTransformAnchor.Value.position.x > -11.40f && isGoingUp == false)
    //                {
    //                    isGoingUp = true;
    //                    DOTween.Kill(bedroom.transform);
    //                    bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 0, bedroom.transform.position.z), fallTime).SetEase(Ease.OutQuint);
    //                }
    //            }
    //        }
    //    }
    //}
}
