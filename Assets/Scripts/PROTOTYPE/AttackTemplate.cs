using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTemplate : MonoBehaviour
{
    //public ObjectPooler objectPooler;

	[Header("Broadcasting on")]
	[SerializeField] private BoolEventChannelSO _updateAttackUI = default;

    public Animator playerAnim;
    public Animator attackAnim;
    public PlaySingleAudioClip audioPlayer;

	private void Awake()
    {
        if (_updateAttackUI != null)
            _updateAttackUI.RaiseEvent(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
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
        playerAnim.SetTrigger("Attack");
        attackAnim.SetTrigger("Attack");
        audioPlayer.PlayAudioClip();
    }

    
}
