using UnityEngine;

public enum TurretType
{
    MachineGun,
}

public abstract class TurretData
{
    [field: SerializeField] public TurretType turretType { get; private set; } = TurretType.MachineGun;

    protected TurretData(TurretType _turretType)
    {
        turretType = _turretType;
    }
}

public abstract class TurretData_MG : TurretData
{
    [field: SerializeField] public float minAltitudeAngle { get; private set; } = float.NaN;
    [field: SerializeField] public float maxAltitudeAngle { get; private set; } = float.NaN;
    public TurretData_MG(float _minAltitudeAngle, float _maxAltitudeAngle) : base(TurretType.MachineGun)
    {
        minAltitudeAngle = _minAltitudeAngle;
        maxAltitudeAngle = _maxAltitudeAngle;
    }
}

public class TurretData_MG_Twin : TurretData_MG
{
    public TurretData_MG_Twin() : base(_minAltitudeAngle: 80f, _maxAltitudeAngle: 140f)
    {
    }
}

public class TurretData_MG_Gat : TurretData_MG
{
    public TurretData_MG_Gat() : base(_minAltitudeAngle: 30f, _maxAltitudeAngle: 120f)
    {
    }
}