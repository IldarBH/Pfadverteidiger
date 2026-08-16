using UnityEngine;

public abstract class MachineGun : TurretBase
{
    public Bullet bulletPrefab;
    protected Transform _JointAzimuth = null;
    protected Transform _JointAltitude = null;
    protected Transform _barrel = null;
    protected TurretData_MG data_mg_ => data_ as TurretData_MG;

    protected void Initialize(TurretData_MG data)
    {
        Debug.Log($"MachineGun.Initialize() called for {gameObject.name}");
        base.Initialize(data);
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
        var bullet = Instantiate(bulletPrefab, _barrel.position, Quaternion.identity);
        bullet.transform.LookAt(target.transform.position);
        bullet.Initialize(bulletSpeed, bulletDamage);
    }

    protected override void PerformReloading_Implementation_() { }

    protected override bool isTargetLocked_()
    {
        if (!isTargetAvailable_())
            return false;
        
        var target_direction = targetTracker_.target.position - _barrel.position;
        // Debug.DrawRay(_barrel.position, target_direction, Color.blue);
        var target_forecast_direction = targetTracker_.targetForecast - _barrel.position;
        // Debug.DrawRay(_barrel.position, target_forecast_direction, Color.cyan);
        if (target_direction.magnitude > data_mg_.firingRangeMax)
        {
            // Debug.DrawRay(_barrel.position, target_direction, Color.red);
            return false;
        }
        var barrel_direction = GetBarrelDirection_();
        var angle = Vector3.Angle(barrel_direction, target_direction);
        if (angle > data_mg_.firingAngleTolerance)
        {
            // Debug.DrawRay(_barrel.position, barrel_direction * data_mg_.firingRangeMax, Color.red);
            return false;
        }
        // Debug.DrawRay(_barrel.position, barrel_direction * data_mg_.firingRangeMax, Color.green);
        return true;
    }

    protected virtual Vector3 GetBarrelDirection_() { return _barrel.forward; }
}
