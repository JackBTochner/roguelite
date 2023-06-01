using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public float turnSpeedMultiplier;
    public GameObject projectilePrefab;

    public float fireRate = 200;
    public int bulletsPerShot = 1;
    public float spreadAngle;
    //public int magazineSize = 8;
    //public int currentMagazineAmount = 8;
    
    public ProjectileEffectSlots effectTemplate = default;
    // public List<PlayerProjectileEffectSO> effectTemplates = new List<PlayerProjectileEffectSO>();
    [SerializeField]
    private List<ProjectileEffectSO> effectInstances = new List<ProjectileEffectSO>();

    public Transform muzzle;
    public ParticleSystem muzzleFlash;

    private Vector3 lastMuzzlePosition;
    public Vector3 muzzleWorldVelocity;

    private float lastTimeShot;

    [SerializeField] private InputReader inputReader = default;

    [Header("Broadcasting On")]
    [SerializeField] private ProjectileCountSO _projectileCount;
    [SerializeField] private VoidEventChannelSO _updateProjectileUI = default;

    private void OnEnable()
    {
        inputReader.OnAttack2Performed += TryAttack;
        effectTemplate.OnSlotsUpdated += AssignEffectInstances;
    }

    private void OnDisable()
    {
        inputReader.OnAttack2Performed -= TryAttack;
        effectTemplate.OnSlotsUpdated -= AssignEffectInstances;
    }

    private void Start()
    {
        AssignEffectInstances(effectTemplate.Effects);
        if (_updateProjectileUI != null)
                _updateProjectileUI.RaiseEvent();
    }

// TODO: Make sure to find a way to garbage collect cleared scriptable object instances.
    private void AssignEffectInstances(List<ProjectileEffectSO> effectTemplates)
    {
        effectInstances.Clear();
        foreach (var effect in effectTemplates)
        {
            string effectTemplateName = effect.GetType().ToString();
            ProjectileEffectSO effectInstance = (ProjectileEffectSO)ScriptableObject.CreateInstance(effectTemplateName);
            effectInstance.Copy(effect);
            effectInstances.Add(effectInstance);
        }
    }

    private void Update()
    {
        if (Time.deltaTime > 0)
        {
            muzzleWorldVelocity = (muzzle.position - lastMuzzlePosition) / Time.deltaTime;
            lastMuzzlePosition = muzzle.position;
        }
    }

    public void AddNewProjectile()
    {
        _projectileCount.AddNewProjectiles(1);
        if (_updateProjectileUI != null)
                _updateProjectileUI.RaiseEvent();
    }

    public void TryAttack()
    {
        FireWeapon();
    }

    public bool FireWeapon()
    {
        if (_projectileCount.CurrentProjectileCount <= 0)
        {
            return false;
        }
        if (lastTimeShot + 60 / fireRate < Time.time)
        {
            _projectileCount.RemoveCurrentProjectiles(1);
            if (_updateProjectileUI != null)
                    _updateProjectileUI.RaiseEvent();
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Vector3 shotDirection = GetShotDirectionWithinSpread(muzzle);
                GameObject newProjectile = Instantiate(projectilePrefab, muzzle.position, Quaternion.LookRotation(shotDirection));
                Debug.Log(effectInstances[_projectileCount.CurrentProjectileCount]);
                newProjectile.GetComponent<PlayerProjectile>().InitialiseProjectile(this, effectInstances[_projectileCount.CurrentProjectileCount]);
            }

            if (muzzleFlash != null)
            {
                muzzleFlash.Clear();
                muzzleFlash.Play();
            }
            lastTimeShot = Time.time;
            return true;
        }
        return false;
    }

    public void AddToCurrentMagazine(int count)
    {
        _projectileCount.AddCurrentProjectiles(count);
        if (_updateProjectileUI != null)
                _updateProjectileUI.RaiseEvent();
    }

    public Vector3 GetShotDirectionWithinSpread(Transform origin)
    {
        float spreadAngleRatio = spreadAngle / 180f;
        Vector3 spreadWorldDirection = Vector3.Slerp(origin.forward, Random.insideUnitSphere, spreadAngleRatio);
        return spreadWorldDirection;
    }
}