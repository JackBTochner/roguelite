using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {

        [Header("Health")]
        [Tooltip("Health config, default value is used when no PlayerManager is present.")]
        [SerializeField] private HealthSO _currentHealthSO = default;
        [SerializeField]private float _deathDelay = 0.5f;

        [Header("Dig")]
        [Tooltip("Stamina config, default value is used when no PlayerManager is present.")]
        [SerializeField] private StaminaSO _currentStaminaSO = default;
        public bool allowStaminaRegen = true;
        public float digStaminaCost = 25;
        public float digStaminaDepleteRate = 5;
        public GameObject playerGFX;
        public GameObject playerDigGFX;
        public AttackObject playerDigAttack;
        public float digFreezeTime;
        private bool isDigging;
        

        [Header("Broadcasting on")]
        [SerializeField] private VoidEventChannelSO _updateHealthUI = default;
        [SerializeField] private VoidEventChannelSO _updateStaminaUI = default;
        [SerializeField] private VoidEventChannelSO _deathEvent = default;
        [SerializeField] private VoidEventChannelSO _camShakeEvent = default;
        [Header("Listening on")]
        [SerializeField] private PlayerManagerAnchor _playerManagerAnchor = default;
        [SerializeField] private RunManagerAnchor _runManagerAnchor = default;
        private PlayerManager _playerManager;

        private void Awake()
        {
            if (_updateHealthUI != null)
                _updateHealthUI.RaiseEvent();
            if (_updateStaminaUI != null)
                _updateStaminaUI.RaiseEvent();
        }

        private void Start()
        {
            if (_playerManagerAnchor.isSet)
                _playerManager = _playerManagerAnchor.Value;
        }

        public void Initialise()
        {
            if (_playerManager)
            {
                _currentHealthSO = _playerManager.CurrentHealthSO;
                _currentStaminaSO = _playerManager.CurrentStaminaSO;
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

        private void Update()
        {
            if (isDigging)
            {
                _currentStaminaSO.InflictDamage(digStaminaDepleteRate * Time.deltaTime);
                if(_updateStaminaUI != null)
                    _updateStaminaUI.RaiseEvent();
            } else if(allowStaminaRegen)
            {
                _currentStaminaSO.RestoreStamina(_currentStaminaSO.RegenRate * Time.deltaTime);
                if(_updateStaminaUI != null)
                    _updateStaminaUI.RaiseEvent();
            }
        }

        public void PlayerToggleDig(bool currentlyDigging)
        {
            if (currentlyDigging)
            {
                StartCoroutine(EmergeFromDig(digFreezeTime));
            }
            else
            {
                if (!HasStaminaForDig())
                {
                    NotifyCantDig();
                    return;
                }
                _currentStaminaSO.InflictDamage(digStaminaCost);
                playerGFX.SetActive(false);
                playerDigGFX.SetActive(true);
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
                isDigging = true;
            }
        }

        public bool HasStaminaForDig()
        {
            return _currentStaminaSO.CurrentStamina - digStaminaCost > 0;
        }

        public void NotifyCantDig()
        {
            Debug.Log("Can't dig! Stamina Too Low");
            //TODO: Raise stamina bar UI flashing event
        }

        IEnumerator EmergeFromDig(float freezeTime)
        {
            // gameObject.GetComponent<CharacterController>().detectCollisions = false;
            isDigging = false;
            _camShakeEvent.RaiseEvent();
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
            _camShakeEvent.RaiseEvent();
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