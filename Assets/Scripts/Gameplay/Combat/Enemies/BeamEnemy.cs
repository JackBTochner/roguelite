using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

public class BeamEnemy : Hittable
{
    public float beamFollowLagTime = 0.2f;

    public Animator anim;
    public Rigidbody rb;
    public Transform lookAt;

    public GameObject[] itemDrops;

    public LayerMask detectionObstacles;

    public bool detected;
    public float detectionRange;

    bool died;

    public GameObject hitParticle;

    public Transform beamTransform;

    public GameObject debugCube;

    bool followPlayer;

    public TransformAnchor playerTransformAnchor = default;
    public AIDestinationSetter AI;

    public UnityEvent<BeamEnemy> OnEnemyDied = new UnityEvent<BeamEnemy>();


    void OnEnable()
    {
        playerTransformAnchor.OnAnchorProvided += AssignAITarget;
        if (playerTransformAnchor.isSet)
            AssignAITarget();
    }

    void OnDisable()
    { 
        playerTransformAnchor.OnAnchorProvided -= AssignAITarget;
    }

    void AssignAITarget()
    {
        if (AI != null)
        {
            AI.target = playerTransformAnchor.Value;
        }
    }

    void Update()
    {
        if (playerTransformAnchor.isSet)
        {
        //detect
        if (detected)
        {
            float dist = Vector3.Distance(transform.position, playerTransformAnchor.Value.position);
            if (dist > detectionRange + 5)
            {
                detected = false;
                anim.SetBool("DetectedPlayer", false);
            }

            //lag lookat
            if (followPlayer)
            {
                Vector3 direction = playerTransformAnchor.Value.position - transform.position;
                transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * beamFollowLagTime);
            }

            //lookAt.forward = -direction;


            RaycastHit hit;
            if (Physics.Raycast(transform.position, lookAt.forward, out hit, detectionRange, detectionObstacles))
            {
                beamTransform.localScale = new Vector3(1, 1, Vector3.Distance(transform.position, hit.point));

                //debugCube.transform.position = hit.point;
            }
        }
        //wandering
        else
        {
            if(Vector3.Distance(transform.position, playerTransformAnchor.Value.position) < detectionRange)
            {

                detected = true;
                anim.SetBool("DetectedPlayer", true);
            }
        }

        if (rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 3);
        }
        }
    }

    override public void Hit(DamageInfo damageInfo)
    {
        base.Hit(damageInfo);
        if (health > 0)
        {
            rb.AddForce(damageInfo.Direction * damageInfo.Knockback);
            lookAt.forward = damageInfo.Direction;
            Instantiate(hitParticle, transform.position, lookAt.rotation);

            anim.SetTrigger("Hit");
        }
        else if (!died)
        {
            Die();
        }
    }
    
    public void Die()
    {
        //die
        
        anim.SetTrigger("Die");

        //spawn a hit particle in the opposite direction of the player;
        Vector3 hitDirection = playerTransformAnchor.Value.position - transform.position;
        // lookAt.forward = -hitDirection;
        Instantiate(hitParticle, transform.position, lookAt.rotation);

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
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }

    public void StartFollowing()
    {
        followPlayer = true;
    }
    public void StopFollowing()
    {
        followPlayer = false;
    }
}