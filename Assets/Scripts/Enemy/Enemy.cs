using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;
using Unity.VisualScripting;

// Not nice. garbage accumulates.
public class Enemy : Hittable
{
    public Animator anim;
    public Animator rootAnim;
    public Rigidbody rb;
    public GameObject[] attackObjects;
    public GameObject[] itemDrops;
    public float dropPercentage = 10;
    public GameObject hitParticle;

    public AIPath aiPath;
    public AIDestinationSetter aiDestination;
    public Transform playerTarget;

    public LayerMask detectionObstacles;

    public bool detected = true;
    public float detectionRange;

    public bool isAttacking = false;
    public bool died;

    public UnityEvent<Enemy> OnEnemyDied = new UnityEvent<Enemy>();
    public UnityEvent<Enemy> OnTakeDigDamage = new UnityEvent<Enemy>();

    public UnityEvent OnEnemyDiedMeleeExplodeWisp = new UnityEvent();

    public static int enemyAlive = 0;

    [Header("Stats")]
    public float detectionRadius = 10f;
    public float maxAttackRange = 2f;
    public float minAttackRange = 1f;
    public float retreatRadius = 3f;
    public float turnSpeedMultiplier = 3f;

    public float attackTime = 1.5f;
    public float attackPause = 0.1f;

    public enum AIState {
        Idle,
        MovingToTarget,
        Attacking,
        Retreating,
        GettingHit
    }
    
    public AIState currentState;

    [Header("Listening on")]
    public TransformAnchor playerTransformAnchor = default;

    public float DistanceToPlayer()
    {
        if(playerTarget)
            return Vector3.Distance(transform.position, playerTarget.position);
        return 10;
    }

    public void OrientTowards(Vector3 targetLocation, float turnSpeedMultiplier)
    { 
        Vector3 targetDirection  = (targetLocation - transform.position).normalized;
        if(targetDirection.sqrMagnitude != 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeedMultiplier);
        }
    }
    
    public void AssignAITarget()
    {
        aiDestination.target = playerTransformAnchor.Value;
        playerTarget = playerTransformAnchor.Value;
    }

    public void SetActiveAttackObjects(bool enable)
    { 
        foreach (var obj in attackObjects)
        {
            obj.SetActive(enable);
        }
    }

    public virtual IEnumerator IdleState()
    {
        Debug.Log("Idle: Enter");
        aiDestination.enabled = false;
        while (currentState == AIState.Idle)
        {
            if (DistanceToPlayer() <= detectionRadius)
                currentState = AIState.MovingToTarget;
            yield return 0;
        }
        Debug.Log("Idle: Exit");
        aiDestination.enabled = false;
        NextState();
    }

    public virtual IEnumerator MovingToTargetState()
    {
        Debug.Log("MovingToTarget: Enter");
        aiDestination.enabled = true;
        while (currentState == AIState.MovingToTarget)
        {
            if (DistanceToPlayer() <= maxAttackRange)
                currentState = AIState.Attacking;
            OrientTowards(playerTarget.position, turnSpeedMultiplier);
            yield return 0;
        }
        Debug.Log("MovingToTarget: Exit");
        aiDestination.enabled = false;
        NextState();
    }
    public virtual IEnumerator AttackingState()
    {
        Debug.Log("AttackingState: Enter");
        aiDestination.enabled = false;

        isAttacking = true;
        anim.SetTrigger("PrepareAttack");
        yield return new WaitForSeconds(attackPause);
        anim.SetTrigger("Attack");
        // Enable damage colliders
        SetActiveAttackObjects(true);
        yield return new WaitForSeconds(attackTime);
        // Disable damage colliders
        SetActiveAttackObjects(false);
        isAttacking = false;
        while (currentState == AIState.Attacking)
        {
            if (DistanceToPlayer() <= minAttackRange)
                currentState = AIState.Retreating;
            if (DistanceToPlayer() > maxAttackRange)
                currentState = AIState.MovingToTarget;
            yield return 0;
        }
        Debug.Log("AttackingState: Exit");
        anim.ResetTrigger("PrepareAttack");
        anim.ResetTrigger("Attack");
        NextState();
    }

    public virtual IEnumerator RetreatingState()
    {
        Debug.Log("RetreatingState: Enter");
        aiDestination.enabled = false;
        while (currentState == AIState.Retreating)
        {
            if (DistanceToPlayer() >= retreatRadius)
                currentState = AIState.MovingToTarget;
            OrientTowards(playerTarget.position, turnSpeedMultiplier);
            yield return 0;
        }
        Debug.Log("RetreatingState: Exit");
        NextState();
    }

    public virtual IEnumerator GettingHitState()
    {
        Debug.Log("GettingHitState: Enter");
        aiDestination.enabled = false;
        anim.ResetTrigger("OnHit");
        anim.SetTrigger("OnHit");
        while (currentState == AIState.GettingHit)
        {
            currentState = AIState.Idle;
            yield return 0;
        }
        Debug.Log("GettingHitState: Exit");
        NextState();
    }

    public void NextState()
    {
        string methodName = currentState.ToString() + "State";
        System.Reflection.MethodInfo info = 
        GetType().GetMethod(methodName, 
        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    public void OnEnable()
    {
        enemyAlive++;
        playerTransformAnchor.OnAnchorProvided += AssignAITarget;
        if (playerTransformAnchor.isSet)
            AssignAITarget();
    }
    public void OnDisable()
    {
        enemyAlive--;
        playerTransformAnchor.OnAnchorProvided -= AssignAITarget;
    }

    public void Start()
    {
        NextState();
    }

    public override void Hit(DamageInfo damageInfo)
    {
        base.Hit(damageInfo);
        if (damageInfo.DamageType == DamageType.Dig)
            OnTakeDigDamage.Invoke(this);
        if (health > 0)
        {
            rb.AddForce(Vector3.ProjectOnPlane(damageInfo.Direction, Vector3.up) * damageInfo.Knockback, ForceMode.Impulse);
            Instantiate(hitParticle, transform.position, Quaternion.LookRotation(damageInfo.Direction));
            if(currentState != AIState.Attacking)
                currentState = AIState.GettingHit;
            rootAnim.SetTrigger("Hit");
        }
        else if (!died)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        rootAnim.SetTrigger("Die");

        //spawn a hit particle in the opposite direction of the player;
        Vector3 hitDirection = playerTransformAnchor.Value.position - transform.position;
        Instantiate(hitParticle, transform.position, Quaternion.LookRotation(-hitDirection));

        //item drops
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
        //give 0.5 seconds for the death animation to happen before disabling the object
        Invoke("Delete", 0.5f);
        died = true;
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.IncreaseScore(1);
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }
}
