using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackTemplate : MonoBehaviour
{
    //public ObjectPooler objectPooler;
    [SerializeField] private InputReader inputReader = default;

    [SerializeField] private float _comboDelay = 1.2f;
    [SerializeField] private int _comboMaxStep = 2;
    [SerializeField] private int _comboHitStep;
    private Coroutine _comboAttackResetCoroutine;

    private int _animAttackComboStepParamHash;

    [SerializeField] private Animator _animator;
    [SerializeField] private PlaySingleAudioClip _audioPlayer;

    [SerializeField] private bool _canAttack = true;

	[Header("Broadcasting on")]
	[SerializeField] private BoolEventChannelSO _updateAttackUI = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _startDeathEvent = default;

	private void Awake()
    {
        _animAttackComboStepParamHash = Animator.StringToHash("AttackComboCount");
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
        float t = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (_comboHitStep == -1 || (t >= 0.1f && t <= 0.8f))
        {
            if (_comboAttackResetCoroutine != null)
                StopCoroutine(_comboAttackResetCoroutine);
            _comboHitStep++;
            _animator.SetInteger(
                _animAttackComboStepParamHash, _comboHitStep);
            _audioPlayer.PlayAudioClip();
            _comboAttackResetCoroutine = StartCoroutine(_ResettingAttackCombo());
        }
    }
    private IEnumerator _ResettingAttackCombo()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        _comboHitStep = -1;
        _animator.SetInteger(
            _animAttackComboStepParamHash, _comboHitStep);
    }
}
