using UnityEngine;

public class T_MG_TWIN : MachineGun
{
    public GameObject Joint1;
    public GameObject Joint2;
    public GameObject Joint3;
    public GameObject Joint4;

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
        _JointAzimuth = Joint2.transform;
        _JointAltitude = Joint4.transform;
        _barrel = Joint4.transform;
        _data = new TurretData_MG_Twin();
    }

    protected override Vector3 GetBarrelDirection_() { return _barrel.up; }
}
