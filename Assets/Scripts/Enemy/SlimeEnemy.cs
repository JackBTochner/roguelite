using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : Enemy
{
    public Transform spawnPoint; // Slime spawn point
    public GameObject slimeEruptProjectile;
    public int slimeEruptCount = 3;
    public float slimeEruptForce = 10;
    public float eruptionTimer = 0f;
    public float eruptionInterval = 3;

    public float spread = 0.1f;

    public override IEnumerator AttackingState()
    {
        aiDestination.enabled = false;
        eruptionTimer = eruptionInterval;
        while (currentState == AIState.Attacking)
        {
            if (DistanceToPlayer() <= minAttackRange)
                currentState = AIState.Retreating;
            if (DistanceToPlayer() > maxAttackRange)
                currentState = AIState.MovingToTarget;
            StartCoroutine(AttemptShoot());
            OrientTowards(playerTarget.position, turnSpeedMultiplier);
            yield return 0;
        }
        anim.ResetTrigger("PrepareAttack");
        anim.ResetTrigger("Attack");
        NextState();
    }

    private IEnumerator AttemptShoot()
    {         
        anim.ResetTrigger("PrepareAttack");
        anim.ResetTrigger("Attack");

        isAttacking = true;
        anim.SetTrigger("PrepareAttack");
        
        // eruptionTimer minus 1/60 in each frame.
        eruptionTimer -= Time.deltaTime;
        // If bulletTime is bigger than 0, do nothing. This means the loading time is passed.
        if (eruptionTimer > 0) 
            yield break;
        // If else do.
        // Set the reload for shot time back to.
        eruptionTimer = eruptionInterval;
        anim.SetTrigger("Attack");

        Erupt();
        isAttacking = false;
    }

    void Erupt()
    {
        for (int i = 0; i < slimeEruptCount; i++)
        {
            GameObject slime = GameObject.Instantiate(slimeEruptProjectile, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = slime.GetComponent<Rigidbody>();
            if (rb)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-spread, spread), 1, Random.Range(-spread, spread)).normalized;
                rb.AddForce(randomDirection * slimeEruptForce, ForceMode.Impulse);
            }
        }
    }
    
}
