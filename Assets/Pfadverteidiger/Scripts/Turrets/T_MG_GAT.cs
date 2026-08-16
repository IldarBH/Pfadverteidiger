using UnityEngine;

public class T_MG_GAT : MachineGun
{
    public GameObject Joint1;
    public GameObject Joint2;
    public GameObject Joint3;
    public GameObject Joint4;
    public GameObject Joint5;
    
    protected void Awake()
    {
        base.Initialize(new TurretData_MG_Gat());
        if (Joint1 == null)
            Joint1 = transform.Find("Joint.001").gameObject;
        if (Joint2 == null)
            Joint2 = Joint1.transform.Find("Joint.002").gameObject;
        if (Joint3 == null)
            Joint3 = Joint2.transform.Find("Joint.003").gameObject;
        if (Joint4 == null)
            Joint4 = Joint3.transform.Find("Joint.004").gameObject;
        if (Joint5 == null)
            Joint5 = Joint4.transform.Find("Joint.005").gameObject;
        _JointAzimuth = Joint2.transform;
        _JointAltitude = Joint4.transform;
        _barrel = Joint5.transform;
    }
}
