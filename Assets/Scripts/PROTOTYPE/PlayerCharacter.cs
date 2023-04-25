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

        public float maxHealth = 100.0f;
        public float currentHealth;

        void Start()
        {
            currentHealth = maxHealth;
        }

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

        public void takeDamage(float amount)
        {
            float nextHealth = currentHealth - amount;
            if (nextHealth <= 0)
            {
                currentHealth = 0;
                PlayerDie();
            }
            else
            {
                currentHealth = nextHealth;
            }

            Debug.Log(currentHealth);
        }

        public void PlayerDie()
        {
            Debug.Log("Player dead");
            // Maybe create a listener and invoke here.
            // RunManager.ReturnToHub
        }
    }
}