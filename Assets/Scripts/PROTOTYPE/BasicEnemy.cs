using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

public class BasicEnemy : Hittable
{
    public Animator anim;
    public Rigidbody rb;
    public Transform lookAt;

    public GameObject[] itemDrops;

    public TransformAnchor playerTransformAnchor = default;
    public AIDestinationSetter AI;

    public LayerMask detectionObstacles;

    public bool detected = true;
    public float detectionRange;

    bool died;

    public UnityEvent<BasicEnemy> OnEnemyDied = new UnityEvent<BasicEnemy>();


    public GameObject hitParticle;

    
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
        if (rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 3);
        }
    }

    override public void Hit(DamageType damageType, float damage, CriticalData criticalData, float knockback, Vector3 direction)
    {
        base.Hit(damageType, damage, criticalData, knockback, direction);
        if (health > 0)
        {
            rb.AddForce(direction * knockback);
            lookAt.forward = direction;
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
        anim.SetTrigger("Die");

        //spawn a hit particle in the opposite direction of the player;
        Vector3 hitDirection = playerTransformAnchor.Value.position - transform.position;
        lookAt.forward = -hitDirection;
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
}