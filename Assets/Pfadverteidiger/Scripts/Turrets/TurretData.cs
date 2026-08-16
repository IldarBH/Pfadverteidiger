using UnityEngine;

public enum TurretType
{
    MachineGun,
}

public abstract class TurretData
{
    [field: SerializeField] public uint ammoCapacityMax { get; private set; } = uint.MaxValue;
    [field: SerializeField] public float ammoReloadRate { get; private set; } = float.NaN;
    [field: SerializeField] public float firingAngleTolerance { get; private set; } = 5f;
    [field: SerializeField] public float firingRangeMin { get; private set; } = float.NaN;
    [field: SerializeField] public float firingRangeMax { get; private set; } = float.NaN;
    [field: SerializeField] public float firingReloadRate { get; private set; } = float.NaN;
    [field: SerializeField] public TurretType turretType { get; private set; } = TurretType.MachineGun;
    [field: SerializeField] public float projectileSpeed { get; private set; } = float.NaN;
    
    protected TurretData(
        uint _ammoCapacityMax,
        float _ammoReloadRate,
        float _firingRangeMin,
        float _firingRangeMax,
        float _firingReloadRate,
        float _projectileSpeed,
        TurretType _turretType)
    {
        turretType = _turretType;
        firingRangeMin = _firingRangeMin;
        firingRangeMax = _firingRangeMax;
        firingReloadRate = _firingReloadRate;
        ammoCapacityMax = _ammoCapacityMax;
        ammoReloadRate = _ammoReloadRate;
        projectileSpeed = _projectileSpeed;
    }
}

public abstract class TurretData_MG : TurretData
{
    [field: SerializeField] public float minAltitudeAngle { get; private set; } = float.NaN;
    [field: SerializeField] public float maxAltitudeAngle { get; private set; } = float.NaN;
    [field: SerializeField] public float targetingSpeed { get; private set; } = float.NaN;
    
    public TurretData_MG(
        uint _ammoCapacityMax,
        float _ammoReloadRate,
        float _firingRangeMax,
        float _firingReloadRate,
        float _maxAltitudeAngle,
        float _minAltitudeAngle,
        float _targetingSpeed)
    : base(
        _ammoCapacityMax: _ammoCapacityMax,
        _ammoReloadRate: _ammoReloadRate,
        _firingRangeMin: 0f,
        _firingRangeMax: _firingRangeMax,
        _firingReloadRate: _firingReloadRate,
        _projectileSpeed: 10f,
        _turretType: TurretType.MachineGun)
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
        _ammoCapacityMax: 30,
        _ammoReloadRate: 0.2f,
        _firingRangeMax: 10f,
        _firingReloadRate: 0.6f,
        _maxAltitudeAngle: 140f,
        _minAltitudeAngle: 80f,
        _targetingSpeed: 30f)
    {
    }
}

public class TurretData_MG_Gat : TurretData_MG
{
    public TurretData_MG_Gat() 
    : base(
        _ammoCapacityMax: 90,
        _ammoReloadRate: 0.1f,
        _firingRangeMax: 8f,
        _firingReloadRate: 0.2f,
        _maxAltitudeAngle: 120f,
        _minAltitudeAngle: 30f,
        _targetingSpeed: 20f)
    {
    }
}