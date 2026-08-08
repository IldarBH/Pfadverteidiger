using UnityEngine;

public abstract class MachineGun : TurretBase
{
    public Bullet bulletPrefab;
    [field: SerializeField] public float bulletSpeed { get; private set; } = 10f;
    [field: SerializeField] public uint bulletDamage { get; private set; } = 1;
    protected Transform barrel_ = null;

    protected override void Update()
    {
        base.Update();
    }

    protected override void PerformReloading_Implementation_() { }
}
