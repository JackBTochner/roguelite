using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Highlighter))]
public class PlayerProjectileReceiver : MonoBehaviour
{
    public int projectileCount = 0;
    public GameObject pickup;
    public GameObject hitParticle = default;
    public List<ProjectileEffectSO> effectSOs = new List<ProjectileEffectSO>();
    public PlayerProjectileMarker projectileMarker;

    void OnEnable()
    { 
        BasicEnemy enemy = GetComponent<BasicEnemy>();
        BeamEnemy bEnemy = GetComponent<BeamEnemy>();
        // on enemy take dig damage, drop projectiles.
        if (enemy)
        {
            enemy.OnEnemyDied.AddListener(DropProjectiles);
            enemy.OnTakeDigDamage.AddListener(DropProjectiles);
        }
        if(bEnemy)
            bEnemy.OnEnemyDied.AddListener(BDropProjectiles);
    }
    void OnDisable()
    { 
        BasicEnemy enemy = GetComponent<BasicEnemy>();
        BeamEnemy bEnemy = GetComponent<BeamEnemy>();
        // on enemy take dig damage, drop projectiles.
        if (enemy)
        {
            enemy.OnEnemyDied.RemoveListener(DropProjectiles);
            enemy.OnTakeDigDamage.RemoveListener(DropProjectiles);
        }
        if(bEnemy)
            bEnemy.OnEnemyDied.RemoveListener(BDropProjectiles);
    }

    public void AddProjectile(ProjectileEffectSO projectileEffect)
    {
        projectileCount++;
        effectSOs.Add(projectileEffect);
        projectileEffect.Initialise(this.gameObject);
        Debug.Log(projectileEffect.GetType());
        Debug.Log(projectileEffect.Icon);
        projectileMarker.AddIconAt(projectileEffect.Icon, effectSOs.Count-1);
    }

    public void DropProjectiles(BasicEnemy enemy)
    {
        if (projectileCount > 0)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                Instantiate(pickup, transform.position, Quaternion.identity);
            }
        }
        Instantiate(hitParticle, transform.position, Quaternion.LookRotation(transform.up));
        projectileCount = 0;
        effectSOs.Clear();
        projectileMarker.ClearIcons();
    }

    public void BDropProjectiles(BeamEnemy enemy)
    {
        if (projectileCount > 0)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                Instantiate(pickup, transform.position, Quaternion.identity);
            }
        }
        Instantiate(hitParticle, transform.position, Quaternion.LookRotation(transform.up));
        projectileCount = 0;
        effectSOs.Clear();
        projectileMarker.ClearIcons();
    }
}
