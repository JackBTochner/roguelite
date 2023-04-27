using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {

        [Header("Health")]
        [SerializeField] private int initialHealth_DEBUG;
        [SerializeField] private int maxHealth_DEBUG;
        [SerializeField] private int currentHealth_DEBUG;

        [Tooltip("Health config, default value is used when no PlayerManager is present.")]
        [SerializeField] private HealthSO _currentHealthSO = default;

        [SerializeField]private float _deathDelay = 0.5f;

        [Header("Dig")]
        public GameObject playerGFX;
        public GameObject playerDigGFX;
        public AttackObject playerDigAttack;
        public float digFreezeTime;

        [Header("Broadcasting on")]
        [SerializeField] private VoidEventChannelSO _updateHealthUI = default;
        [SerializeField] private VoidEventChannelSO _deathEvent = default;
        [Header("Listening on")]
        [SerializeField] private PlayerManagerAnchor _playerManagerAnchor = default;
        [SerializeField] private RunManagerAnchor _runManagerAnchor = default;
        private PlayerManager _playerManager;

        private void Awake()
        {
            if (_updateHealthUI != null)
                _updateHealthUI.RaiseEvent();
            Debug.Log("Player Current Health: " + _currentHealthSO.CurrentHealth);
        }

        public void Start()
        {
            if (_playerManagerAnchor.isSet)
                _playerManager = _playerManagerAnchor.Value;
        }

        public void Initialise()
        {
            if (_playerManager)
            {
                _currentHealthSO = _playerManager.CurrentHealthSO;
                Debug.Log("Player currentHealth set to: " + _currentHealthSO.CurrentHealth);
            } else
            { 
                if (_currentHealthSO == null)
                {
                    _currentHealthSO = ScriptableObject.CreateInstance<HealthSO>();
                    _currentHealthSO.SetMaxHealth(_currentHealthSO.InitialHealth);
                    _currentHealthSO.SetCurrentHealth(_currentHealthSO.InitialHealth);
                }
            }
        }

        public void PlayerToggleDig(bool isDigging)
        {
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
        }

        public void TakeDamage(int amount)
        {
            float nextHealth = _currentHealthSO.CurrentHealth - amount;
            if (nextHealth <= 0)
            {
                _currentHealthSO.SetCurrentHealth(0);
                StartCoroutine(PlayerDie(_deathDelay));
            }
            else
            {
                _currentHealthSO.InflictDamage(amount);
            }
            if (_updateHealthUI != null)
                _updateHealthUI.RaiseEvent();
            // Debug.Log("Player Current Health: " + _currentHealthSO.CurrentHealth);
        }

        IEnumerator PlayerDie(float delay)
        {
            if (_updateHealthUI != null)
                _updateHealthUI.RaiseEvent();
            Debug.Log("Player dead");
            // TURN OFF CHARACTER INPUT
            // SHOW DEATH ANIMATION
            yield return new WaitForSeconds(delay);
            // Maybe create a listener and invoke here.
            if(_runManagerAnchor != null)
                _runManagerAnchor.Value.ReturnToHub();
        }
    }
}