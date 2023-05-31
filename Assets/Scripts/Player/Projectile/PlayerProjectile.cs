using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Derive from AttackObject.
public class PlayerProjectile : MonoBehaviour
{
    ProjectileEffectSO projectileEffect;

    [Header("Damage")]
    public DamageType damageType;
    public CriticalData criticalData = new CriticalData(50, 1.5f, false);
    public float damage = 1f;

    public float knockback = 1000;

    public GameObject hitMarker;
    public GameObject critMarker;
    public GameObject impactVFX;
    
    public GameObject pickup;
    GameObject instigator;

    [Header("Collision")]
    public float radius = 0.5f;

    // root of the projectile for collision detection
    public Transform root;

    // tip of the projectile for collision detection
    public Transform tip;

    public float maxLifeTime = 5f;

    public LayerMask hittableLayers = -1;

    public float speed = 10f;
    public float gravityAcceleration = 1f;
    public bool inheritWeaponVelocity = false;
    Vector3 lastRootPosition;
    Vector3 velocity;

    List<Collider> ignoredColliders;

    float shootTime = Mathf.NegativeInfinity;

    Vector3 initialPosition;
    Vector3 initialDirection;
    Vector3 inheritedMuzzleVelocity;


    private float timeSpawned = Mathf.NegativeInfinity;

    public void InitialiseProjectile(PlayerWeaponController controller, ProjectileEffectSO effect)
    {
        instigator = controller.gameObject;
        initialPosition = transform.position;
        initialDirection = transform.forward;
        inheritedMuzzleVelocity = controller.muzzleWorldVelocity;

        shootTime = Time.time;
        lastRootPosition = root.position;
        velocity = transform.forward * speed;
        ignoredColliders = new List<Collider>();
        transform.position += inheritedMuzzleVelocity * Time.deltaTime;

        Collider[] instigatorColliders =
            instigator.GetComponentsInChildren<Collider>();
        ignoredColliders.AddRange (instigatorColliders);
        // Debug.Log(effect);
        projectileEffect = effect;
        projectileEffect.Initialise(this.gameObject);
        timeSpawned = Time.time;
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        if (inheritWeaponVelocity)
            transform.position += inheritedMuzzleVelocity * Time.deltaTime;

        transform.forward = velocity.normalized;

        if (gravityAcceleration > 0)
            velocity += Vector3.down * gravityAcceleration * Time.deltaTime;

        HitDetection();
        lastRootPosition = root.position;

        if (Time.time > timeSpawned + maxLifeTime)
        {
            SpawnPickupAndRemove();
        }
    }

    void HitDetection()
    {
        RaycastHit closestHit = new RaycastHit();
        closestHit.distance = Mathf.Infinity;
        bool foundHit = false;

        Vector3 displacementSinceLastFrame = tip.position - lastRootPosition;
        RaycastHit[] hits =
            Physics
                .SphereCastAll(lastRootPosition,
                radius,
                displacementSinceLastFrame.normalized,
                displacementSinceLastFrame.magnitude,
                hittableLayers,
                QueryTriggerInteraction.Collide);

        foreach (var hit in hits)
        {
            if (IsHitValid(hit) && hit.distance < closestHit.distance)
            {
                foundHit = true;
                closestHit = hit;
            }
        }

        if (foundHit)
        {
            if (closestHit.distance <= 0f)
            {
                closestHit.point = root.position;
                closestHit.normal = -transform.forward;
            }

            OnHit(closestHit.point, closestHit.normal, closestHit.collider);
        }
    }

    bool IsHitValid(RaycastHit hit)
    {
        if (!hit.collider.enabled) return false;

        //if(hit.collider.isTrigger && hit.collider.GetComponent<Damageable> == null)
        //    return false;
        // ignore hits within ignored colliders

        if (ignoredColliders != null && ignoredColliders.Contains(hit.collider))
            return false;
        return true;
    }

    void OnHit(Vector3 point, Vector3 normal, Collider collider)
    {
        
        PlayerProjectileReceiver receiver = collider.GetComponent<PlayerProjectileReceiver>();
        if (receiver)
        {
            receiver.AddProjectile(projectileEffect);
            Destroy(this.gameObject);
        }
        else
        {
            SpawnPickupAndRemove();
        }
        
        Hittable hitReceiver = collider.GetComponent<Hittable>();
        if (hitReceiver)
        {
            Vector3 knockbackDir = Vector3.Normalize(hitReceiver.transform.position - this.transform.position);
            hitReceiver.Hit(damageType, damage, criticalData, knockback, knockbackDir);
        }

        // Add impact SFX
        if (impactVFX)
        {
            GameObject vfx = GameObject.Instantiate(impactVFX, point, Quaternion.LookRotation(normal));
        }

    }

    private void SpawnPickupAndRemove()
    {
        GameObject.Instantiate(pickup, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
