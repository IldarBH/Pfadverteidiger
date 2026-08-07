using UnityEngine;

public abstract class MachineGun : TurretBase
{
    public Bullet bulletPrefab;
    [field: SerializeField] public float bulletSpeed { get; private set; } = 10f;
    [field: SerializeField] public uint bulletDamage { get; private set; } = 1;

    protected override void Update()
    {
        base.Update();
    }

    protected override void PerformShoot_()
    {
        var bullet = Instantiate(bulletPrefab, endPoint.position, Quaternion.identity);
        bullet.transform.LookAt(target.transform.position);
        bullet.Initialize(bulletSpeed, bulletDamage);
    }
}
