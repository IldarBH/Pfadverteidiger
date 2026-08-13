using UnityEngine;

public abstract class MachineGun : TurretBase
{
    public Bullet bulletPrefab;
    protected Transform _JointAzimuth = null;
    protected Transform _JointAltitude = null;
    protected Transform _barrel = null;
    protected TurretData_MG _data_mg => _data as TurretData_MG;

    protected override void Update()
    {
        base.Update();
    }

    protected override void PerformTargeting_Implementation_()
    {
        // It's going to be weird. Unity uses left-handed coordinate system, while blend uses right-handed coordinate system.
        var direction = target.transform.position - _barrel.position;

        var targetAzimuthDirection = Vector3.ProjectOnPlane(direction, _JointAzimuth.forward);
        var targetAzimuthAngle = Vector3.SignedAngle(_JointAzimuth.up, targetAzimuthDirection, _JointAzimuth.forward);
        var deltaAzimuthAngle = Mathf.Clamp(targetAzimuthAngle, -targetingSpeed * Time.deltaTime, targetingSpeed * Time.deltaTime);
        _JointAzimuth.Rotate(_JointAzimuth.forward, deltaAzimuthAngle, Space.World);

        var targetAltitudeDirection = Vector3.ProjectOnPlane(direction, _JointAltitude.right);
        var targetAltitudeAngle = Vector3.SignedAngle(_JointAltitude.up, targetAltitudeDirection, _JointAltitude.right);
        var deltaAltitudeAngle = Mathf.Clamp(targetAltitudeAngle, -targetingSpeed * Time.deltaTime, targetingSpeed * Time.deltaTime);
        
        var currentAltitudeAngle = Vector3.SignedAngle(_JointAltitude.parent.forward, _JointAltitude.forward, _JointAltitude.parent.right);
        var maxAltitudeCondition = deltaAltitudeAngle > 0f && currentAltitudeAngle > _data_mg.maxAltitudeAngle;
        var minAltitudeCondition = deltaAltitudeAngle < 0f && currentAltitudeAngle < _data_mg.minAltitudeAngle;
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
        // For that model direction of barrel_ is along its local up (green) axis
        var target_direction = target.transform.position - _barrel.position;
        var barrel_direction = GetBarrelDirection_();
        var angle = Vector3.Angle(barrel_direction, target_direction);
        Debug.DrawRay(_barrel.position, target_direction, Color.blue);
        if (target_direction.magnitude > firingRangeMax || target_direction.magnitude < firingRangeMin || angle > targetingAngleTolerance)
        {
            Debug.DrawRay(_barrel.position, barrel_direction * firingRangeMax, Color.red);
            return false;
        }
        Debug.DrawRay(_barrel.position, barrel_direction * firingRangeMax, Color.green);
        return true;
    }

    protected virtual Vector3 GetBarrelDirection_() { return _barrel.forward; }
}
