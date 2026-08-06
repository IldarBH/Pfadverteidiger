using UnityEngine;

public class MachineGun : TurretBase
{
    public float bulletSpeed = 20f;
    public Bullet bulletPrefab;
    public float ammoCapacity = 30f;
    bool isFiring = false;
    bool isReloading = false;

    protected override void Update()
    {
        base.Update();
    }

    protected override void PerformFiring()
    {
        
    }

    protected override void PerformReloading()
    {
        
    }

    private void FireBullet()
    {
        
    }
}
