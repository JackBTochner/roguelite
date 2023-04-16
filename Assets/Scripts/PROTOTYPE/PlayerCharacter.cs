using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {

        public GameObject playerGFX;
        public GameObject playerDigGFX;
        public AttackObject playerDigAttack;
        public float digFreezeTime;

        public void PlayerToggleDig(bool isDigging)
        {
            Debug.Log("YEAHAYAH");
            if (isDigging)
            {
                StartCoroutine(EmergeFromDig(digFreezeTime));
            }
            else
            {
                playerGFX.SetActive(false);
                playerDigGFX.SetActive(true);
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            }
        }

        IEnumerator EmergeFromDig(float freezeTime)
        {
            // gameObject.GetComponent<CharacterController>().detectCollisions = false;
            playerGFX.SetActive(true);
            playerDigGFX.SetActive(false);
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
            playerDigAttack.gameObject.SetActive(true);
            gameObject.GetComponent<PlayerMovement>().allowMovement = false;
            yield return new WaitForSeconds(freezeTime);
            // gameObject.GetComponent<CharacterController>().detectCollisions = true;
            gameObject.GetComponent<PlayerMovement>().allowMovement = true;
            playerDigAttack.gameObject.SetActive(false);
            Debug.Log("FinishedFreeze");
        }
    }
}