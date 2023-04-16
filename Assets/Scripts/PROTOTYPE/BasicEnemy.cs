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

    public bool detected;
    public float detectionRange;

    bool died;

    public UnityEvent OnEnemyDied = new UnityEvent();

    public GameObject hitParticle;

    void OnEnable()
    {
        playerTransformAnchor.OnAnchorProvided += AssignAITarget;
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
                }
            }
            //wandering
            else
            {
                lookAt.forward = playerTransformAnchor.Value.position - transform.position;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, lookAt.forward, out hit, detectionRange, detectionObstacles) && hit.collider.CompareTag("Player"))
                {
                    detected = true;
                }
            }

            if (rb.velocity != Vector3.zero)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 3);
            }
        }
    }

    override public void Hit(float knockback, Vector3 direction)
    {
        if (playerTransformAnchor.isSet)
        {
            if (health > 0)
            {
                //knockback
                rb.AddForce(direction * knockback);

                //spawn a hit particle in the opposite direction of the player;
                Vector2 hitDirection = playerTransformAnchor.Value.position - transform.position;
                lookAt.forward = -hitDirection;
                Instantiate(hitParticle, transform.position, lookAt.rotation);

                anim.SetTrigger("Hit");
            }
            else if (!died) //so that the enemy can't die multiple times
            {
                Die();
            }
        }
    }

    public void Die()
    {
        //die
        OnEnemyDied.Invoke();
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
        //give 0.5 seconds for the death animation to happen before disabling the object
        Invoke("Delete", 0.5f);

        died = true;
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }
}