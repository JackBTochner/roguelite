using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;
using UnityEditor.EditorTools;

public class Enemy : Hittable
{
    public Animator anim;
    public Rigidbody rb;

    public GameObject[] itemDrops;
    [Tooltip("Item drop rate out of 100")]
    public float itemDropRate = 10.0f;

    public GameObject impactVFX;

    bool died;

    [Header("Listening on")]
    [SerializeField] private TransformAnchor _playerTransformAnchor = default;

    [Header("Broadcasting on")]
    public UnityEvent<Enemy> OnEnemyDied = new UnityEvent<Enemy>();
    public UnityEvent<Enemy> OnTakeDigDamage = new UnityEvent<Enemy>();

    override public void Hit(DamageInfo damageInfo)
    {
        base.Hit(damageInfo);
        if (damageInfo.DamageType == DamageType.Dig)
            OnTakeDigDamage.Invoke(this);
        if (health > 0)
        {
            rb.AddForce(Vector3.ProjectOnPlane(damageInfo.Direction, Vector3.up) * damageInfo.Knockback, ForceMode.Impulse);
            Instantiate(impactVFX, transform.position, Quaternion.LookRotation(damageInfo.Direction));

            anim.SetTrigger("Hit");
        }
        else if (!died)
        {
            Die();
        }
    }

    public void Die()
    {
        anim.SetTrigger("Die");

        //spawn a hit particle in the opposite direction of the player;
        Vector3 hitDirection = _playerTransformAnchor.Value.position - transform.position;
        Instantiate(impactVFX, transform.position, Quaternion.LookRotation(-hitDirection));

        //item drops
        if (itemDrops.Length > 0)
        {
            for (int i = 0; i < itemDrops.Length; i++)
            {
                Instantiate(itemDrops[i], transform.position, Quaternion.identity);
            }
        }
        OnEnemyDied.Invoke(this);
        //give 0.5 seconds for the death animation to happen before disabling the object
        Invoke("Delete", 0.5f);
        died = true;
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.IncreaseScore(1);
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }
}
