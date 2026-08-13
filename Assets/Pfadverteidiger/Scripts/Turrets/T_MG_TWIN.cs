using UnityEngine;

public class T_MG_TWIN : MachineGun
{
    public GameObject Joint1;
    public GameObject Joint2;
    public GameObject Joint3;
    public GameObject Joint4;
    public float minAltitude = 80f;
    public float maxAltitude = 140f;

    protected override void Awake()
    {
        base.Awake();
        if (Joint1 == null)
            Joint1 = transform.Find("Joint.001").gameObject;
        if (Joint2 == null)
            Joint2 = Joint1.transform.Find("Joint.002").gameObject;
        if (Joint3 == null)
            Joint3 = Joint2.transform.Find("Joint.003").gameObject;
        if (Joint4 == null)
            Joint4 = Joint3.transform.Find("Joint.004").gameObject;
        barrel_ = Joint4.transform;
    }

    protected override void PerformFiring_Implementation_()
    {
        var bullet = Instantiate(bulletPrefab, barrel_.position, Quaternion.identity);
        bullet.transform.LookAt(target.transform.position);
        bullet.Initialize(bulletSpeed, bulletDamage);
    }

    protected override bool isTargetLocked_()
    {
        if (!isTargetAvailable_())
            return false;
        // For that model direction of barrel_ is along its local up (green) axis
        var target_direction = target.transform.position - barrel_.position;
        var barrel_direction = GetBarrelDirection_();
        var angle = Vector3.Angle(barrel_direction, target_direction);
        Debug.DrawRay(barrel_.position, target_direction, Color.blue);
        if (target_direction.magnitude > firingRangeMax || target_direction.magnitude < firingRangeMin || angle > targetingAngleTolerance)
        {
            Debug.DrawRay(barrel_.position, barrel_direction * firingRangeMax, Color.red);
            return false;
        }
        Debug.DrawRay(barrel_.position, barrel_direction * firingRangeMax, Color.green);
        return true;
    }

    protected override void PerformTargeting_Implementation_()
    {
        // It's going to be weird. Unity uses left-handed coordinate system, while blend uses right-handed coordinate system.
        var direction = target.transform.position - barrel_.position;

        var targetAzimuthDirection = Vector3.ProjectOnPlane(direction, Joint2.transform.forward);
        var targetAzimuthAngle = Vector3.SignedAngle(Joint2.transform.up, targetAzimuthDirection, Joint2.transform.forward);
        var deltaAzimuthAngle = Mathf.Clamp(targetAzimuthAngle, -targetingSpeed * Time.deltaTime, targetingSpeed * Time.deltaTime);
        Joint2.transform.Rotate(Joint2.transform.forward, deltaAzimuthAngle, Space.World);

        var targetAltitudeDirection = Vector3.ProjectOnPlane(direction, Joint4.transform.right);
        var targetAltitudeAngle = Vector3.SignedAngle(Joint4.transform.up, targetAltitudeDirection, Joint4.transform.right);
        var deltaAltitudeAngle = Mathf.Clamp(targetAltitudeAngle, -targetingSpeed * Time.deltaTime, targetingSpeed * Time.deltaTime);
        
        var currentAltitudeAngle = Vector3.SignedAngle(Joint3.transform.forward, Joint4.transform.forward, Joint3.transform.right);
        var maxAltitudeCondition = deltaAltitudeAngle > 0f && currentAltitudeAngle > maxAltitude;
        var minAltitudeCondition = deltaAltitudeAngle < 0f && currentAltitudeAngle < minAltitude;
        if (maxAltitudeCondition || minAltitudeCondition)
        {
            return;
        }
        Joint4.transform.Rotate(Joint4.transform.right, deltaAltitudeAngle, Space.World);
    }

    private Vector3 GetBarrelDirection_()
    {
        return barrel_.up;
    }
}
