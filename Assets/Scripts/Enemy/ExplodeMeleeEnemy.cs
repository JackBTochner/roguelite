using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Unity.VisualScripting;
using DG.Tweening;

public class ExplodeMeleeEnemy : Enemy
{
    public bool isDying = false;

    public float ExplosionCountdown = 2f;
    public float ExplosionTime = 0.5f;

    public float ExplosionDamage = 10;

    public float ExplosionDistance = 3;
    public GameObject ExplosionIndicator;
    public GameObject ExplosionVFX;
    public SkinnedMeshRenderer gfxRenderer;

    public Material WarningMaterial;
    public Color WarningColorHigh = Color.white;
    public Color WarningColorLow = Color.red;

    public override IEnumerator IdleState()
    {
        Debug.Log("Idle: Enter");
        aiDestination.enabled = false;
        aiPath.canMove = false;
        aiPath.enableRotation = false;
        while (currentState == AIState.Idle)
        {
            // The addition of !isDying makes sure that when the enemy is dying, the state will not transition and will remain IdleState
            if (!isDying && DistanceToPlayer() <= detectionRadius)
                currentState = AIState.MovingToTarget;
            yield return 0;
        }
        Debug.Log("Idle: Exit");
        aiDestination.enabled = false;
        aiPath.canMove = true;
        aiPath.enableRotation = true;
        NextState();
    }

    public override void Die()
    {
        isDying = true;
        currentState = AIState.Idle;
        //spawn a hit particle in the opposite direction of the player;
        Vector3 hitDirection = playerTransformAnchor.Value.position - transform.position;
        Instantiate(hitParticle, transform.position, Quaternion.LookRotation(-hitDirection));
        StartCoroutine(ExplosionCoroutine());
    }

    public IEnumerator ExplosionCoroutine()
    {
        //ExplosionIndicator.SetActive(true);
        gfxRenderer.material = WarningMaterial;
        WarningMaterial.SetColor("Emission", WarningColorLow);
        WarningMaterial.DOColor(WarningColorHigh, "Emission", 0.5f).SetEase(Ease.InExpo);
        transform.DOShakeScale(ExplosionCountdown-0.1f, 0.1f, 5, 5, false).SetEase(Ease.InExpo);

        yield return new WaitForSeconds(ExplosionCountdown);    
        PlayerCharacter playerCharacter = playerTransformAnchor.Value.GetComponent<PlayerCharacter>();
        if (DistanceToPlayer() < ExplosionDistance) // Take full damage.
        {
            playerCharacter.TakeDamage((int)ExplosionDamage);
        }
        rootAnim.SetTrigger("Die");
        //ExplosionIndicator.SetActive(false);
        ExplosionVFX.SetActive(true);
        yield return new WaitForSeconds(ExplosionTime);
        // item drops
        if (Random.Range(0, 100) < dropPercentage)
        {
            if (itemDrops.Length > 0)
            {
                for (int i = 0; i < itemDrops.Length; i++)
                {
                    Instantiate(itemDrops[i], transform.position, Quaternion.identity);
                }
            }
        }
        OnEnemyDied.Invoke(this);
        Delete();
        died = true;
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.IncreaseScore(1);
    }
}
