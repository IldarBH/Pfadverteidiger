using UnityEngine;

public enum TurretType
{
    MachineGun,
}

public abstract class TurretData
{
    [field: SerializeField] public TurretType turretType { get; private set; } = TurretType.MachineGun;
    [field: SerializeField] public float firingAngleTolerance { get; private set; } = 5f;
    [field: SerializeField] public float firingRangeMin { get; private set; } = float.NaN;
    [field: SerializeField] public float firingRangeMax { get; private set; } = float.NaN;

    protected TurretData(
        TurretType _turretType, 
        float _firingRangeMin, 
        float _firingRangeMax)
    {
        turretType = _turretType;
        firingRangeMin = _firingRangeMin;
        firingRangeMax = _firingRangeMax;
    }
}

public abstract class TurretData_MG : TurretData
{
    [field: SerializeField] public float minAltitudeAngle { get; private set; } = float.NaN;
    [field: SerializeField] public float maxAltitudeAngle { get; private set; } = float.NaN;
    [field: SerializeField] public float targetingSpeed { get; private set; } = float.NaN;
    
    public TurretData_MG(
        float _minAltitudeAngle, 
        float _maxAltitudeAngle, 
        float _targetingSpeed,
        float _firingRangeMax) 
    : base(TurretType.MachineGun, _firingRangeMin: 0f, _firingRangeMax: _firingRangeMax)
    {
        minAltitudeAngle = _minAltitudeAngle;
        maxAltitudeAngle = _maxAltitudeAngle;
        targetingSpeed = _targetingSpeed;
    }
}

public class TurretData_MG_Twin : TurretData_MG
{
    public TurretData_MG_Twin() 
    : base(
        _minAltitudeAngle: 80f, 
        _maxAltitudeAngle: 140f, 
        _targetingSpeed: 30f, 
        _firingRangeMax: 10f)
    {
    }
}

public class TurretData_MG_Gat : TurretData_MG
{
    public TurretData_MG_Gat() 
    : base(
        _minAltitudeAngle: 30f, 
        _maxAltitudeAngle: 120f, 
        _targetingSpeed: 20f, 
        _firingRangeMax: 8f)
    {
    }
}