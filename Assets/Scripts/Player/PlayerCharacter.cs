using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

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
        public bool isDigging;
        public Animator playerAnim;
        public Animator staminaBarAnimator;
        public Volume postProcessVolume;
        private ColorAdjustments colorAdjustments;
        private Color originalColorFilter;
        [ColorUsage (true, true)]
        public Color DamagedColorFilter;
        private Coroutine damageFXCoroutine;
        public float damageFXTime = 0.5f;

        [Header("Broadcasting on")]
        //invulnerable
        public bool invulnerable = false;
        //
        [SerializeField] private VoidEventChannelSO _updateHealthUI = default;
        [SerializeField] private VoidEventChannelSO _updateStaminaUI = default;
        [SerializeField] private VoidEventChannelSO _startDeathEvent = default;
        [SerializeField] private VoidEventChannelSO _camShakeEvent = default;
		[SerializeField] private BoolEventChannelSO _updateDigUI = default;
  		[Header("Listening on")]
        [SerializeField] private PlayerManagerAnchor _playerManagerAnchor = default;
        [SerializeField] private RunManagerAnchor _runManagerAnchor = default;
        [SerializeField] private TimeManagerAnchor _timeManagerAnchor = default;
        [SerializeField] private TransformAnchor _postProcessTransformAnchor = default;
        private PlayerManager _playerManager;
        public AudioSource audioSource;
        public AudioSource battlemusic;
        public float musicduration = 5.0f;

        private SphereCollider playerCollider;

        private void Awake()
        {
            if (_updateHealthUI != null)
                _updateHealthUI.RaiseEvent();
            if (_updateStaminaUI != null)
                _updateStaminaUI.RaiseEvent();
            // Will ALWAYS start player as being able to dig when loading into map.
			if (_updateDigUI != null)
                _updateDigUI.RaiseEvent(true);
        }

        private void Start()
        {

            if (_playerManagerAnchor.isSet)
                _playerManager = _playerManagerAnchor.Value;
            if (!battlemusic)
            {
                battlemusic = GameObject.FindWithTag("MusicController").GetComponent<AudioSource>();
            }
            if (_postProcessTransformAnchor.isSet)
            {
                postProcessVolume = _postProcessTransformAnchor.Value.GetComponent<Volume>();
                postProcessVolume.profile.TryGet(out colorAdjustments);
                Debug.LogWarning(colorAdjustments);
                originalColorFilter = (Color)colorAdjustments.colorFilter;
            }
        }

        public void Initialise()
        {
            if (_playerManager)
            {
                _currentHealthSO = _playerManager.CurrentHealthSO;
                _currentStaminaSO = _playerManager.CurrentStaminaSO;
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
				if(_updateDigUI != null)
					_updateDigUI.RaiseEvent(true);
            }
            else
            {
                if (!HasStaminaForDig())
                {
                    NotifyCantDig();
                    return;
                }
                _currentStaminaSO.InflictDamage(digStaminaCost);
                StartCoroutine(DigEntry());
				if(_updateDigUI != null)
					_updateDigUI.RaiseEvent(false);
            }
        }
        
        private IEnumerator DigEntry()
        {
            playerAnim.SetTrigger("Dig_Entry");
            yield return new WaitForSeconds(0.35f);
            playerGFX.SetActive(false);
            playerDigGFX.SetActive(true);
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            isDigging = true;
        }

        public bool HasStaminaForDig()
        {
            return _currentStaminaSO.CurrentStamina - digStaminaCost > 0;
        }

        public void NotifyCantDig()
        {
            staminaBarAnimator.SetTrigger("CantDig");
            // Debug.Log("Can't dig! Stamina Too Low");
            //TODO: Raise stamina bar UI flashing event
        }

        IEnumerator EmergeFromDig(float freezeTime)
        {
            // gameObject.GetComponent<CharacterController>().detectCollisions = false;
            isDigging = false;
            _camShakeEvent.RaiseEvent();
            playerGFX.SetActive(true);
            playerAnim.SetTrigger("Dig_Exit");
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
            //invulnerable
            if (invulnerable)
            {
                return;
            }
            
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

            // Hitstop
            if (_timeManagerAnchor != null)
                _timeManagerAnchor.Value.SetTimeScale(0.05f, 7, 0.1f);
            // Color filter
            if (postProcessVolume != null)
            {
                if (damageFXCoroutine != null)
                    StopCoroutine(damageFXCoroutine);
                damageFXCoroutine = StartCoroutine(DamageScreenFX(damageFXTime));
            }
            // Camshake
            _camShakeEvent.RaiseEvent();
        }

        IEnumerator PlayerDie(float delay)
        {
            if (postProcessVolume != null)
            {
                colorAdjustments.colorFilter.Override(Color.white);
            }
            playerCollider = GetComponent<SphereCollider>();
            playerCollider.enabled = false;
            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }
            CharacterController charController = GetComponent<CharacterController>();
            if (charController != null)
            {
                charController.enabled = false;
            }
            TurnoffMusic();
            PlayMusic();
            if (_updateHealthUI != null)
                _updateHealthUI.RaiseEvent();
            Debug.Log("Player dead");

            // TURN OFF CHARACTER INPUT
            gameObject.GetComponent<PlayerMovement>().allowMovement = false;
            // SHOW DEATH ANIMATION
            playerAnim.SetTrigger("Death");

            if(_startDeathEvent)
            {
                _startDeathEvent.RaiseEvent();
            }

            // Save score
            yield return new WaitForSeconds(3f);
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.SaveScore();


            yield return new WaitForSeconds(delay);
            // Maybe create a listener and invoke here.
            if(_runManagerAnchor != null)
                _runManagerAnchor.Value.ReturnToHub();
        }

        IEnumerator DamageScreenFX(float duration)
        {
            colorAdjustments.colorFilter.Override(DamagedColorFilter);
            float timer = 0.0f;
            while (timer < 1)
            {
                timer += Time.deltaTime / duration;
                if (timer > 1) timer = 1;
                colorAdjustments.colorFilter.Override(Color.Lerp(DamagedColorFilter, originalColorFilter, timer));
                yield return null;
            }
        }

        public void TurnoffMusic()
        {
            if (battlemusic)
            {
                battlemusic.Stop();
            }
                
        }
        public void PlayMusic()
        {
            // Check the Audio is implemented.
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is not assigned!");
                return;
            }

            audioSource.Play();
            // Run "StopMusic" after musicduration
            Invoke("StopMusic", musicduration);
        }

        private void StopMusic()
        {
            // Stop the music
            audioSource.Stop();
        }
    }
}