using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDig : MonoBehaviour
    {
        public LayerMask digIgnoreLayerMask = default;

        public Transform digDetectionLocation;

        public float digRayLength = 2f;

        public InputReader inputReader = default;

        public PlayerBufferPoint[] playerBuffers;

        public bool isDigging;

        public GameObject digDownParticle = default;
        public GameObject digUpParticle = default;

        [SerializeField]
        private TransformAnchor _playerTransformAnchor = default;

        private void Awake()
        {
            inputReader.OnDigPerformed += AttemptToggleDig;

        }

        private void Update()
        {
            if (_playerTransformAnchor.isSet)
            {
                transform.position = _playerTransformAnchor.Value.position;
                transform.rotation = _playerTransformAnchor.Value.rotation;
            }
        }

        private void AttemptToggleDig()
        {
            RaycastHit hit;
            if (
                Physics
                    .Raycast(digDetectionLocation.position,
                    -Vector3.up,
                    out hit,
                    digRayLength,
                    ~digIgnoreLayerMask,
                    QueryTriggerInteraction.Collide)
            )
            {
                ToggleDig(hit);
            }
            else
            {
                Debug.Log("Nothing to Dig!");
            }
        }

        private void ToggleDig(RaycastHit hit)
        {
            if (hit.transform.gameObject.CompareTag("Dirt"))
            {
                Debug.Log("Can Dig!");
                if (isDigging)
                {
                    foreach (var buffer in playerBuffers)
                    {
                        buffer.groundCollider = hit.collider;
                        Instantiate(digUpParticle, transform.position, transform.rotation);
                        buffer.gameObject.SetActive(false);
                    }

                    _playerTransformAnchor.Value.GetComponent<PlayerCharacter>().PlayerToggleDig(isDigging);
                    isDigging = false;
                }
                else
                {
                    foreach (var buffer in playerBuffers)
                    {
                        buffer.groundCollider = hit.collider;
                        Instantiate(digDownParticle, transform.position, Quaternion.LookRotation(Vector3.up));
                        buffer.gameObject.SetActive(true);
                    }
                    _playerTransformAnchor.Value.GetComponent<PlayerCharacter>().PlayerToggleDig(isDigging);
                    isDigging = true;
                }
            }
            else
            {
                Debug.Log("Can't Dig this!");
            }
        }
    }
}