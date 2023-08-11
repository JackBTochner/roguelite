using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using DG.Tweening; //Animation engine for Unity

public class StoreRoomZone : MonoBehaviour
{
    // When the player spawn it will assign it to the player, it can get the current player, transform and rotation.
    public TransformAnchor playerTransformAnchor = default;
    // The object that we want to fall, so this is the storefront zone, we want move the bedroom here.
    public GameObject bedroom;
    // Rise time.
    public float riseTime;
    // Rise distance.
    public float riseDistance;
    // Set false, so this is not going up now.
    private bool isGoingUp = false;
    // If hits the coliider.
    private void OnTriggerEnter(Collider other)
    {
        // the objects hits the player, or we can think: the player move to this object to make this object touch the player.
        if (other.CompareTag("Player"))
        {
            // Get the bedroom position.
            Vector3 bedroomposition = bedroom.transform.position;

            // Debug Testing
            //if (bedroomposition.y == 0 && playerTransformAnchor.Value.position.x > 1.5f)
            //{
            //    isGoingUp = true;
            //    Transform storetransform = bedroom.transform;
            //    storetransform.DOMove(storetransform.position + new Vector3(0, riseDistance, 0), riseTime).SetEase(Ease.OutQuint);
            //}
            // When the bedroom in the sky, make it come back.
            if (bedroomposition.y == 100)
            {
                // It will move down, set to false: this is going down.
                isGoingUp = false;
                // Assign the transform component of the game object to a variable, this allows us to access and manipulate the - 
                // - position, rotation, and scale.
                Transform storetransform = bedroom.transform;
                // Use.DOMove() to choose a position to move to, and choose the speed time to move to that position. so current position - this vector3 in a time, with using the animation OutQuint.
                storetransform.DOMove(storetransform.position - new Vector3(0, riseDistance, 0), riseTime).SetEase(Ease.OutQuint);
            }


            // This two else if is for: if the bedroom is moving but not finish, we have to add another animation.

            // When the object is during going up, and we touch the collider again, this means we want it come back to the ground.
            else if (bedroomposition.y > 0 && bedroomposition.y < 100 && isGoingUp == true)
            {
                // Set to false, means coming down.
                isGoingUp = false;
                DOTween.Kill(bedroom.transform);
                // This is different than the first one, we using a hardcode location to set it back to the ground "0".
                bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 0, bedroom.transform.position.z), riseTime).SetEase(Ease.OutQuint);
            }
            // When the object is during going down, and we touch the collider again, this means we want it go back to the top.
            else if (bedroomposition.y > 0 && bedroomposition.y < 100 && isGoingUp == false)
            {
                // Set to true, means rising up.
                isGoingUp = true;
                DOTween.Kill(bedroom.transform);
                //we using a hardcode location to set it back to the ground "100".
                bedroom.transform.DOMove(new Vector3(bedroom.transform.position.x, 100, bedroom.transform.position.z), riseTime).SetEase(Ease.OutQuint);
            }
        }
    }
    // Once the player leave this object collider.
    private void OnTriggerExit(Collider other)
    {
        Vector3 bedroomposition = bedroom.transform.position;
        // To makesure we can get the player values.
        if (playerTransformAnchor != null && playerTransformAnchor.Value != null)
        {
            if (other.CompareTag("Player"))
            {
                // The player will levae this collider < 3.5f, so we checking this player leave the collider and it is going towards into the storefront.
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


    // This code below is for testing in the hub with other 2 objects(2 rooms as well but different). Select and Ctrl+k then Ctrl+u to uncomment it.

    
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
