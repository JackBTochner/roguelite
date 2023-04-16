using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class AttackObject : MonoBehaviour
{
    public float damage;

    public bool onlyHitEachObjOnce;

    //public GameObject[] objsHit;
    //public int objsHitIndex;

    public float critChance;
    public float critMultiplier;
    public bool ignoreCrit;

    public float knockback;

    public Hittable hittable;
    public PlayerCharacter playerChar;
    public ObjectPooler objectPooler;

    public GameObject hitMarker;
    public GameObject critMarker;

    void Awake()
    {
        //objectPooler = ObjectPooler.Instance;
        //playerChar = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
    }

    public void Hit(GameObject otherG)
    {
        hittable = otherG.GetComponent<Hittable>();
        if (hittable != null)
        {
            if (otherG.CompareTag("Enemy"))
            {
                int crit = Random.Range(0, 100);
                if (crit <= critChance && !ignoreCrit)
                {
                    GameObject hitMarkerObj = Instantiate(critMarker, otherG.transform.position, Quaternion.identity);
                    hitMarkerObj.GetComponent<HitMarker>().damage = critMultiplier * damage;
                    hitMarkerObj.GetComponent<HitMarker>().OnObjectSpawn();
                    hittable.health -= critMultiplier * damage;
                }
                else
                {
                    GameObject hitMarkerObj = Instantiate(hitMarker, otherG.transform.position, Quaternion.identity);
                    hitMarkerObj.GetComponent<HitMarker>().damage = damage;
                    hitMarkerObj.GetComponent<HitMarker>().OnObjectSpawn();
                    hittable.health -= damage;
                }
            }

            hittable.Hit(knockback, Vector3.Normalize(hittable.transform.position - playerChar.transform.position));
        }
        hittable = null;
    }

}
