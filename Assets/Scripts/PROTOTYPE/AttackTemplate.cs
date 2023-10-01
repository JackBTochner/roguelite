using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackTemplate : MonoBehaviour
{
    //public ObjectPooler objectPooler;
    [SerializeField] private InputReader inputReader = default;

    [SerializeField] private float _comboResetDelay = 0.25f;
    [SerializeField] private float _comboMaxDelay = 1f;
    [SerializeField] private float _comboMinDelay = 0.25f;
    [SerializeField] private int _comboMaxStep = 2;
    [SerializeField] private int _comboHitStep;
    [SerializeField] private float _lastSuccessfulAttackTime = 0;
    private Coroutine _comboAttackResetCoroutine;
    private int _animAttackComboCountHash;
    private int _animAttackTriggerHash;

    [SerializeField] private Animator _animator;
    [SerializeField] private PlaySingleAudioClip _audioPlayer;

    [SerializeField] private bool _canAttack = true;

	[Header("Broadcasting on")]
	[SerializeField] private BoolEventChannelSO _updateAttackUI = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _startDeathEvent = default;

	private void Awake()
    {
        _animAttackComboCountHash = Animator.StringToHash("AttackComboCount");
        _animAttackTriggerHash = Animator.StringToHash("Attack");
        _comboHitStep = -1;
        _comboAttackResetCoroutine = null;

        if (_updateAttackUI != null)
            _updateAttackUI.RaiseEvent(false);
        _canAttack = true;
    }

    private void OnEnable()
    {
        inputReader.OnAttack1Performed += TryAttack;
        _startDeathEvent.OnEventRaised += DisableAttack;
    }
    private void OnDisable()
    {
        inputReader.OnAttack1Performed -= TryAttack;
        _startDeathEvent.OnEventRaised -= DisableAttack;
    }

    void DisableAttack()
    {
        _canAttack = false;
    }

    void TryAttack()
    {
        if (_canAttack)
        {
            if(_updateAttackUI != null)
				_updateAttackUI.RaiseEvent(true);
            Attack();
        }
        else
        {
			if(_updateAttackUI != null)
				_updateAttackUI.RaiseEvent(false);
        }
    }

    void Attack()
    {
        if (_comboHitStep == _comboMaxStep)
            return;
        _animator.ResetTrigger(_animAttackTriggerHash);
        Debug.Log(Time.time - _lastSuccessfulAttackTime > _comboMinDelay);
        if (_comboHitStep == -1 || (Time.time - _lastSuccessfulAttackTime > _comboMinDelay) && (Time.time - _lastSuccessfulAttackTime < _comboMaxDelay))
        {
            if (_comboAttackResetCoroutine != null)
                StopCoroutine(_comboAttackResetCoroutine);
            _lastSuccessfulAttackTime = Time.time;
            _comboHitStep++;
            _animator.SetTrigger(_animAttackTriggerHash);
            _animator.SetInteger(_animAttackComboCountHash, _comboHitStep);
            _audioPlayer.PlayAudioClip();
            _comboAttackResetCoroutine = StartCoroutine(_ResettingAttackCombo());
        }
    }

    // Function run by animation event on the last keyframe of the final animationclip of the combo.
    // Not ideal. Done as a bandaid fix to skip the ComboResetDelay that you would have to wait for after the combo finishes.
    // Find a fix?
    public void ForceResetAttackCombo()
    {
        Debug.Log("FORCERESET ATTACK COMBO");
        if (_comboAttackResetCoroutine != null)
                StopCoroutine(_comboAttackResetCoroutine);
        _comboHitStep = -1;
        _animator.SetInteger(_animAttackComboCountHash, _comboHitStep);
    }
    
    private IEnumerator _ResettingAttackCombo()
    {
        yield return new WaitForEndOfFrame();
        // Wait until the current animation is 95% completed until forcing(or allowing) the combo to be started again
        // yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        yield return new WaitForSeconds(_comboResetDelay);
        _comboHitStep = -1;
        _animator.SetInteger(_animAttackComboCountHash, _comboHitStep);
    }
}
