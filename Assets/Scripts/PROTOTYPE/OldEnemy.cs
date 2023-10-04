using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

public class OldEnemy : Hittable
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

    public UnityEvent<OldEnemy> OnEnemyDied = new UnityEvent<OldEnemy>();
    public UnityEvent<OldEnemy> OnTakeDigDamage = new UnityEvent<OldEnemy>();

    public GameObject hitParticle;

    public UnityEvent OnEnemyDiedMeleeExplodeWisp = new UnityEvent();

    public static int enemyAlive = 0;

    void OnEnable()
    {
        enemyAlive++;

        playerTransformAnchor.OnAnchorProvided += AssignAITarget;
        if (playerTransformAnchor.isSet)
            AssignAITarget();
    }

    void OnDisable()
    {

        enemyAlive--;

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

    override public void Hit(DamageInfo damageInfo)
    {
        base.Hit(damageInfo);
        if (damageInfo.DamageType == DamageType.Dig)
            OnTakeDigDamage.Invoke(this);
        if (health > 0)
        {
            rb.AddForce(Vector3.ProjectOnPlane(damageInfo.Direction, Vector3.up) * damageInfo.Knockback, ForceMode.Impulse);
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
        // Method is called to trigger or broadcast the event.
        OnEnemyDiedMeleeExplodeWisp.Invoke();

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
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.IncreaseScore(1);
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }
}