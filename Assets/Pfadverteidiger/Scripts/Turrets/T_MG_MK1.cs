using UnityEngine;

public class T_MG_MK1 : MachineGun
{
    public GameObject platform;
    public GameObject shoulder;
    public GameObject body;
    public GameObject barrel;

    void Awake()
    {
        if (platform == null)
            platform = transform.Find("Platform").gameObject;
        if (shoulder == null)
            shoulder = platform.transform.Find("Shoulder").gameObject;
        if (body == null)
            body = shoulder.transform.Find("Body").gameObject;
        if (barrel == null)
            barrel = body.transform.Find("Barrel").gameObject;
        barrel_ = barrel.transform;
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
        var direction = target.transform.position - barrel_.position;
        Debug.DrawRay(barrel_.position, direction, Color.blue);
        Debug.DrawRay(barrel_.position, -barrel_.forward * firingRangeMax, Color.red);
        if (direction.magnitude > firingRangeMax || direction.magnitude < firingRangeMin)
            return false;
        var angle = Vector3.Angle(-barrel_.forward, direction);
        var result = angle < targetingAngleTolerance;
        return result;
    }

    protected override void PerformTargeting_Implementation_()
    {
        // It's going to be weird. Unity uses left-handed coordinate system, while blend uses right-handed coordinate system.
        var direction = target.transform.position - barrel_.position;

        var targetShoulderDirection = Vector3.ProjectOnPlane(direction, shoulder.transform.forward);
        var targetShoulderAngle = Vector3.SignedAngle(shoulder.transform.up, targetShoulderDirection, shoulder.transform.forward);
        var deltaShoulderAngle = Mathf.Clamp(targetShoulderAngle, -targetingSpeed * Time.deltaTime, targetingSpeed * Time.deltaTime);
        shoulder.transform.Rotate(shoulder.transform.forward, deltaShoulderAngle, Space.World);

        var targetBodyDirection = Vector3.ProjectOnPlane(direction, body.transform.right);
        var targetBodyAngle = Vector3.SignedAngle(body.transform.up, targetBodyDirection, body.transform.right);
        var deltaBodyAngle = Mathf.Clamp(targetBodyAngle, -targetingSpeed * Time.deltaTime, targetingSpeed * Time.deltaTime);
        body.transform.Rotate(body.transform.right, deltaBodyAngle, Space.World);
    }
}
