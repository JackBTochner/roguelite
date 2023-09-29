using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackTemplate : MonoBehaviour
{
    //public ObjectPooler objectPooler;
    [SerializeField] private InputReader inputReader = default;

    public Animator playerAnim;
    public Animator attackAnim;
    public PlaySingleAudioClip audioPlayer;

    private bool canAttack = true;

	[Header("Broadcasting on")]
	[SerializeField] private BoolEventChannelSO _updateAttackUI = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _startDeathEvent = default;

	private void Awake()
    {
        if (_updateAttackUI != null)
            _updateAttackUI.RaiseEvent(false);
        canAttack = true;
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
        canAttack = false;
    }

    void TryAttack()
    {
        if (canAttack)
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
        if(playerAnim)
            playerAnim.SetTrigger("Attack");
        if(attackAnim)
            attackAnim.SetTrigger("Attack");
        if(audioPlayer)
            audioPlayer.PlayAudioClip();
    }

    
}
