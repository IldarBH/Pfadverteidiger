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
        endPoint = barrel.transform;
    }

    protected override void Update()
    {
        base.Update();
    }

    override protected void PerformTargeting()
    {
        if (target is null)
            return;
        // It's going to be weird. Unity uses left-handed coordinate system, while blend uses right-handed coordinate system.
        var direction = target.transform.position - barrel.transform.position;
        Debug.DrawRay(barrel.transform.position, direction, Color.green);

        var targetShoulderDirection = Vector3.ProjectOnPlane(direction, platform.transform.forward);
        var targetShoulderAngle = Vector3.SignedAngle(shoulder.transform.up, targetShoulderDirection, platform.transform.forward);
        var deltaShoulderAngle = Mathf.Clamp(targetShoulderAngle, -targetingSpeed * Time.deltaTime, targetingSpeed * Time.deltaTime);
        shoulder.transform.Rotate(platform.transform.forward, deltaShoulderAngle, Space.World);

        var targetBodyDirection = Vector3.ProjectOnPlane(direction, shoulder.transform.right);
        var targetBodyAngle = Vector3.SignedAngle(body.transform.up, targetBodyDirection, shoulder.transform.right);
        var deltaBodyAngle = Mathf.Clamp(targetBodyAngle, -targetingSpeed * Time.deltaTime, targetingSpeed * Time.deltaTime);
        body.transform.Rotate(shoulder.transform.right, deltaBodyAngle, Space.World);
    }
}
