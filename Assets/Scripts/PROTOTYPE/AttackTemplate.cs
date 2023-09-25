using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTemplate : MonoBehaviour
{
    //public ObjectPooler objectPooler;

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
        _startDeathEvent.OnEventRaised += DisableAttack;
    }
    private void OnDisable()
    {
        _startDeathEvent.OnEventRaised -= DisableAttack;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
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

    void DisableAttack()
    {
        canAttack = false;
    }

    void Attack()
    {
        playerAnim.SetTrigger("Attack");
        attackAnim.SetTrigger("Attack");
        audioPlayer.PlayAudioClip();
    }

    
}
