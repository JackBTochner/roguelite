using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Derive from AttackObject.
public class PlayerProjectile : MonoBehaviour
{
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

    public float damage = 1f;

    public GameObject impactVFX;

    Vector3 lastRootPosition;
    Vector3 velocity;
    List<Collider> ignoredColliders;
    float shootTime = Mathf.NegativeInfinity;

    GameObject instigator;
    Vector3 initialPosition;
    Vector3 initialDirection;
    Vector3 inheritedMuzzleVelocity;

    
    public float critChance;
    public float critMultiplier;
    public bool ignoreCrit;

    public float knockback;

    public GameObject hitMarker;
    public GameObject critMarker;


    public void InitialiseProjectile(PlayerWeaponController controller)
    {
        // Destroy(gameObject, maxLifeTime);
        instigator = controller.gameObject;
        initialPosition = transform.position;
        initialDirection = transform.forward;
        inheritedMuzzleVelocity = controller.muzzleWorldVelocity;

        shootTime = Time.time;
        lastRootPosition = root.position;
        velocity = transform.forward * speed;
        ignoredColliders = new List<Collider>();
        transform.position += inheritedMuzzleVelocity * Time.deltaTime;

        Collider[] instigatorColliders = instigator.GetComponentsInChildren<Collider>();
        ignoredColliders.AddRange(instigatorColliders);
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        if(inheritWeaponVelocity)
            transform.position += inheritedMuzzleVelocity * Time.deltaTime;

        transform.forward = velocity.normalized;

        if(gravityAcceleration > 0)
            velocity += Vector3.down * gravityAcceleration * Time.deltaTime;

        HitDetection();
        lastRootPosition = root.position;
    }

    void HitDetection()
    {
        RaycastHit closestHit = new RaycastHit();
        closestHit.distance = Mathf.Infinity;
        bool foundHit = false;

        Vector3 displacementSinceLastFrame = tip.position - lastRootPosition;
        RaycastHit[] hits = Physics.SphereCastAll(lastRootPosition, radius, displacementSinceLastFrame.normalized, 
        displacementSinceLastFrame.magnitude, hittableLayers, QueryTriggerInteraction.Collide);

        foreach(var hit in hits){
            if(IsHitValid(hit) && hit.distance < closestHit.distance)
            {
                foundHit = true;
                closestHit = hit;
            }
        }

        if(foundHit)
        {
            if(closestHit.distance <= 0f)
            {
                closestHit.point = root.position;
                closestHit.normal = -transform.forward;
            }

            OnHit(closestHit.point, closestHit.normal, closestHit.collider);
        }
    }

    bool IsHitValid(RaycastHit hit)
    {
        if(!hit.collider.enabled)
            return false;

        //if(hit.collider.isTrigger && hit.collider.GetComponent<Damageable> == null)
        //    return false;

        // ignore hits within ignored colliders 
        if(ignoredColliders != null && ignoredColliders.Contains(hit.collider))
            return false;
        return true;
    }

    void OnHit(Vector3 point, Vector3 normal, Collider collider)
    {
        Hittable hittable = collider.GetComponent<Hittable>();
        if (hittable)
        {
            if (collider.CompareTag("Enemy"))
            {
                int crit = Random.Range(0, 100);
                if (crit <= critChance && !ignoreCrit)
                {
                    GameObject hitMarkerObj = Instantiate(critMarker, collider.transform.position, Quaternion.identity);
                    hitMarkerObj.GetComponent<HitMarker>().damage = critMultiplier * damage;
                    hitMarkerObj.GetComponent<HitMarker>().OnObjectSpawn();
                    hittable.health -= critMultiplier * damage;
                }
                else
                {
                    GameObject hitMarkerObj = Instantiate(hitMarker, collider.transform.position, Quaternion.identity);
                    hitMarkerObj.GetComponent<HitMarker>().damage = damage;
                    hitMarkerObj.GetComponent<HitMarker>().OnObjectSpawn();
                    hittable.health -= damage;
                }
            }
            hittable.Hit(0, Vector3.zero);
        }

        if(impactVFX)
        {
            GameObject vfx = GameObject.Instantiate(impactVFX, point, Quaternion.LookRotation(normal));
        }

        // Add impact SFX
        Destroy(this.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

}