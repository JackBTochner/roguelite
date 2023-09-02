using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTemplate : MonoBehaviour
{
    //public ObjectPooler objectPooler;

	[Header("Broadcasting on")]
	[SerializeField] private AttackSO _isAttacking;
	[SerializeField] private VoidEventChannelSO _updateAttackUI = default;

    public Animator playerAnim;
    public Animator attackAnim;
    public PlaySingleAudioClip audioPlayer;

	private void Awake()
        {
            if (_updateAttackUI != null)
                _updateAttackUI.RaiseEvent();
        }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
			_isAttacking.SetAttack(false);
			if(_updateAttackUI != null)
				_updateAttackUI.RaiseEvent();
            Attack();
        }
        else
        {
            _isAttacking.SetAttack(true);
			if(_updateAttackUI != null)
				_updateAttackUI.RaiseEvent();
        }

    }

    void Attack()
    {
        playerAnim.SetTrigger("Attack");
        attackAnim.SetTrigger("Attack");
        audioPlayer.PlayAudioClip();
    }

    
}
