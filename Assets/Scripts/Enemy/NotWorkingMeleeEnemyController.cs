using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NotWorkingMeleeEnemyController : StateMachine
{
    public NotWorkingEnemy enemy;
    public Animator anim;
    public Rigidbody rb;
    public GameObject[] attackObjects;
    [HideInInspector] public Transform playerTarget;
    public float detectionRadius = 10f;
    public float maxAttackRange = 2f;
    public float minAttackRange = 1f;
    public float retreatRadius = 3f;
    public float turnSpeedMultiplier = 3f;
    public AIPath ai;

    public float attackTime = 1.5f;
    public float attackPause = 0.1f;

    public MeleeIdle idleState;
    public MeleeMoveToTarget moveToTarget;
    public MeleeAttacking attacking;
    public MeleeRetreating retreating;
    public MeleeOnHit onHit;

    [Header("Listening on")]
    [SerializeField] private TransformAnchor _playerTransformAnchor = default;

    private void Awake()
    {
        idleState = new MeleeIdle(this);
        moveToTarget = new MeleeMoveToTarget(this);
        attacking = new MeleeAttacking(this);
        retreating = new MeleeRetreating(this);
        onHit = new MeleeOnHit(this);
    }
    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    void OnEnable()
    {
        _playerTransformAnchor.OnAnchorProvided += AssignTarget;
        ai.onSearchPath += Update;
    }

    void OnDisable()
    { 
        _playerTransformAnchor.OnAnchorProvided -= AssignTarget;
        ai.onSearchPath -= Update;
    }

    void AssignTarget()
    {
        playerTarget = _playerTransformAnchor.Value;
    }

    void Update()
    {
        Debug.Log(ai);
        if (playerTarget)
            ai.destination = playerTarget.position;
    }

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

    public Coroutine InvokeCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
}

public class MeleeIdle : BaseState
{
    protected NotWorkingMeleeEnemyController sm;
    public MeleeIdle (NotWorkingMeleeEnemyController stateMachine) : base("MeleeIdle", stateMachine) {
        sm = (NotWorkingMeleeEnemyController)this.stateMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (sm.DistanceToPlayer() <= sm.detectionRadius)
            sm.ChangeState(sm.moveToTarget);
    }
    public override void OnHit()
    {
        base.OnHit();
        sm.ChangeState(sm.onHit);
    }

    public override void Exit()
    {
        base.Exit();
        if(sm.playerTarget)
            sm.ai.destination = sm.playerTarget.position;
    }
}

public class MeleeMoveToTarget : BaseState
{ 
    protected NotWorkingMeleeEnemyController sm;
    public MeleeMoveToTarget (NotWorkingMeleeEnemyController stateMachine) : base("MeleeMovingToTarget", stateMachine) {
        sm = (NotWorkingMeleeEnemyController)this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sm.ai.canSearch = true;
        sm.ai.enableRotation = false;
    }
    public override void OnHit()
    {
        base.OnHit();
        sm.ChangeState(sm.onHit);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (sm.DistanceToPlayer() <= sm.maxAttackRange)
            sm.ChangeState(sm.attacking);
        sm.anim.SetFloat("Speed", sm.rb.velocity.magnitude / sm.ai.maxSpeed);
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (sm.playerTarget)
        {
            sm.OrientTowards(sm.playerTarget.position, sm.turnSpeedMultiplier);
            // sm.transform.LookAt(sm.playerTarget.position);
            sm.ai.destination = sm.playerTarget.position;
        }
    }
}

public class MeleeAttacking : BaseState
{ 
    protected NotWorkingMeleeEnemyController sm;
    public MeleeAttacking (NotWorkingMeleeEnemyController stateMachine) : base("MeleeAttacking", stateMachine) {
        sm = (NotWorkingMeleeEnemyController)this.stateMachine;
    }
    private bool _isAttacking = true;
    private bool _isInterruptible = false;

    public override void Enter()
    {
        base.Enter();
        sm.ai.canSearch = false;
        sm.ai.enableRotation = false;
        sm.InvokeCoroutine(AttemptAttack());
    }

/*
    public override void OnHit()
    {
        base.OnHit();
        if (_isInterruptible)
        {
            sm.attackObject.SetActive(false);
            sm.anim.ResetTrigger("PrepareAttack");
            sm.anim.ResetTrigger("Attack");
            sm.anim.ResetTrigger("OnHit");
            sm.anim.SetTrigger("OnHit");
        }
    }*/

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (!_isAttacking)
        {
            if (sm.DistanceToPlayer() <= sm.minAttackRange)
                sm.ChangeState(sm.retreating);
            if (sm.DistanceToPlayer() > sm.maxAttackRange)
                sm.ChangeState(sm.moveToTarget);
        }
    }

    public override void Exit()
    {
        base.Exit();
        sm.anim.ResetTrigger("PrepareAttack");
        sm.anim.ResetTrigger("Attack");
    }

    IEnumerator AttemptAttack()
    {
        _isAttacking = true;
        // Can add functionality to counter attack here
        _isInterruptible = true;
        sm.anim.SetTrigger("PrepareAttack");
        yield return new WaitForSeconds(sm.attackPause);
        // Can add functionality to counter attack here
        _isInterruptible = false;
        
        sm.anim.SetTrigger("Attack");
        foreach (var obj in sm.attackObjects)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(sm.attackTime);
        foreach (var obj in sm.attackObjects)
        {
            obj.SetActive(false);
        }
        _isAttacking = false;
    }
}

public class MeleeRetreating : BaseState
{ 
    protected NotWorkingMeleeEnemyController sm;
    public MeleeRetreating (NotWorkingMeleeEnemyController stateMachine) : base("MeleeRetreating", stateMachine) {
        sm = (NotWorkingMeleeEnemyController)this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sm.ai.canSearch = true;
        sm.ai.enableRotation = false;
    }

    public override void OnHit()
    {
        base.OnHit();
        sm.ChangeState(sm.onHit);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (sm.DistanceToPlayer() > sm.retreatRadius)
            sm.ChangeState(sm.moveToTarget);
        sm.OrientTowards(sm.playerTarget.position, sm.turnSpeedMultiplier);
        //sm.transform.LookAt(sm.playerTarget.position);
    }
}

public class MeleeOnHit : BaseState
{
    protected NotWorkingMeleeEnemyController sm;
    public MeleeOnHit (NotWorkingMeleeEnemyController stateMachine) : base("MeleeOnHit", stateMachine)  {
        sm = (NotWorkingMeleeEnemyController)this.stateMachine;
    }

    public override void Enter()
    {   
        base.Enter();
        sm.ai.canSearch = false;
        sm.ai.enableRotation = false;
        sm.anim.ResetTrigger("OnHit");
        sm.anim.SetTrigger("OnHit");
        sm.ChangeState(sm.idleState);
    }
    public override void OnHit()
    {
        base.OnHit();
        sm.ChangeState(sm.onHit);
    }
}