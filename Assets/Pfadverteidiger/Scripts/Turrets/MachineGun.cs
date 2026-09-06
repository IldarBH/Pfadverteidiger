using UnityEngine;
using UnityEngine.Pool;

public abstract class MachineGun : TurretBase
{
    public Bullet bulletPrefab;
    private IObjectPool<ProjectileBase> projectilePool_ = null;
    protected Transform _JointAzimuth = null;
    protected Transform _JointAltitude = null;
    protected Transform _barrel = null;
    protected TurretData_MG data_mg_ => data_ as TurretData_MG;

    protected void Initialize(TurretData_MG data)
    {
        Debug.Log($"MachineGun.Initialize() called for {gameObject.name}");
        base.Initialize(data);
        projectilePool_ = new ObjectPool<ProjectileBase>(
            createFunc: () => CreateProjectile_(),
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 100
        );
    }

    ProjectileBase CreateProjectile_()
    {
        var targetForecast = targetTracker_.targetForecast;
        var lifetime = (targetForecast - _barrel.position).magnitude / data_.projectileSpeed;
        Bullet bullet = Instantiate(bulletPrefab, _barrel.position, Quaternion.identity);
        bullet.transform.LookAt(targetForecast);
        bullet.Initialize(data_.projectileSpeed, 1, lifetime, projectilePool_);
        return bullet;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void PerformTargeting_Implementation_()
    {
        // It's going to be weird. Unity uses left-handed coordinate system, while blend uses right-handed coordinate system.
        var direction = targetTracker_.targetForecast - _barrel.position;

        var targetAzimuthDirection = Vector3.ProjectOnPlane(direction, _JointAzimuth.forward);
        var targetAzimuthAngle = Vector3.SignedAngle(_JointAzimuth.up, targetAzimuthDirection, _JointAzimuth.forward);
        var deltaAzimuthAngle = Mathf.Clamp(targetAzimuthAngle, -data_mg_.targetingSpeed * Time.deltaTime, data_mg_.targetingSpeed * Time.deltaTime);
        _JointAzimuth.Rotate(_JointAzimuth.forward, deltaAzimuthAngle, Space.World);

        var targetAltitudeDirection = Vector3.ProjectOnPlane(direction, _JointAltitude.right);
        var targetAltitudeAngle = Vector3.SignedAngle(_JointAltitude.up, targetAltitudeDirection, _JointAltitude.right);
        var deltaAltitudeAngle = Mathf.Clamp(targetAltitudeAngle, -data_mg_.targetingSpeed * Time.deltaTime, data_mg_.targetingSpeed * Time.deltaTime);
        
        var currentAltitudeAngle = Vector3.SignedAngle(_JointAltitude.parent.forward, _JointAltitude.forward, _JointAltitude.parent.right);
        var maxAltitudeCondition = deltaAltitudeAngle > 0f && currentAltitudeAngle > data_mg_.maxAltitudeAngle;
        var minAltitudeCondition = deltaAltitudeAngle < 0f && currentAltitudeAngle < data_mg_.minAltitudeAngle;
        if (!maxAltitudeCondition && !minAltitudeCondition)
        {
            _JointAltitude.Rotate(_JointAltitude.right, deltaAltitudeAngle, Space.World);
        }
    }

    protected override void PerformFiring_Implementation_()
    {
        bool ishit = Random.value < data_mg_.firingAccuracy;
        var bullet = projectilePool_.Get();
        bullet.transform.position = _barrel.position;
        var targetForecast = targetTracker_.targetForecast;
        bullet.transform.LookAt(targetForecast);
        bullet.ResetTimer();
        bullet.gameObject.SetActive(true);
    }

    protected override void PerformReloading_Implementation_() { }

    protected override bool isTargetLocked_()
    {
        if (!isTargetAvailable_())
            return false;
        
        var target_direction = targetTracker_.target.position - _barrel.position;
        var target_forecast_direction = targetTracker_.targetForecast - _barrel.position;
        if (target_direction.magnitude > data_mg_.firingRangeMax)
        {
            return false;
        }
        var barrel_direction = GetBarrelDirection_();
        var angle = Vector3.Angle(barrel_direction, target_direction);
        if (angle > data_mg_.firingAngleTolerance)
        {
            return false;
        }
        return true;
    }

    protected virtual Vector3 GetBarrelDirection_() { return _barrel.forward; }
}
