using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public Transform spawnPoint; // The spawnPoint that the bullets came out.
    public GameObject enemyBullet; // To get the bullet object.
    public float bulletSpeed = 15; // The move speed of the enemy.
    [SerializeField] private float timer = 1.5f; // [SerializeField] is for private vairables, but it can be edit in the unity inspector.
    [SerializeField] private float maxExtraTimeAttackTime = 0.2f;
    private float bulletTime; // Loading time of the bullet.

    public override IEnumerator AttackingState()
    {
        aiDestination.enabled = false;
        bulletTime = timer + Random.Range(0, maxExtraTimeAttackTime);
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
        
        // bulletTime minus 1/60 in each frame.
        bulletTime -= Time.deltaTime;
        // If bulletTime is bigger than 0, do nothing. This means the loading time is passed.
        if (bulletTime > 0) 
            yield break;
        // If else do.
        // Set the reload for shot time back to.
        bulletTime = timer + Random.Range(0, maxExtraTimeAttackTime);
        anim.SetTrigger("Attack");

        //30% to do
        if (Random.value <= 0.05f)
        {
            //Shoot 3 bullets

            //Shoot normal bullet
            ShootBulletDirection(spawnPoint.transform.rotation);

            //Shoot bullet a bit to the left
            Quaternion leftBullet = Quaternion.Euler(0, -15, 0) * spawnPoint.transform.rotation;
            ShootBulletDirection(leftBullet);

            //Shoot bullet a bit to the right
            Quaternion RightBullet = Quaternion.Euler(0, 15, 0) * spawnPoint.transform.rotation;
            ShootBulletDirection(RightBullet);
        }
        else
        {
            //Shoot normal bullet
            ShootBulletDirection(spawnPoint.transform.rotation);
        }

        isAttacking = false;
    }
    
    void ShootBulletDirection(Quaternion bulletRotation)
    {
        // Create a new Object that on that position and rotation, with the enemyBullet.

        // We use "as GameObject" at last because Instantiate will return a "object" not GameObject, -
        // - we use to explicitly cast to GameObject. Instantiate(GameObject, Vector3, Quaternion).
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, bulletRotation) as GameObject;
        // Get the rigidbody of the object.
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        // Shot the object.
        bulletRig.AddForce(-bulletRig.transform.forward * -bulletSpeed, ForceMode.Impulse);
        // Destroy the object after 5 second.
        Destroy(bulletObj, 5f);
    }
}
