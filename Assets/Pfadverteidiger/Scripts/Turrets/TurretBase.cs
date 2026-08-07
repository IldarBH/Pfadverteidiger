using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    public float targetingSpeed = 20f;
    public float targetingAngleTolerance = 5f;
    [field: SerializeField] public float fireCooldown = 0.5f;
    [field: SerializeField] public float ammoCooldown = 5f;
    [field: SerializeField] public uint ammoCapacity { get; private set; } = 30;
    [field: SerializeField] public uint ammoCapacityMax { get; private set; } = 30;
    public Transform endPoint;
    public enum State
    {
        Idle,
        Targeting,
        Firing,
        Reloading
    }
    public enum TargetingMode
    {
        Nearest
    }

    public State currentState { get; protected set; } = State.Idle;
    public GameObject target {get; private set;} = null;
    public TargetingMode targetingMode { get; set; } = TargetingMode.Nearest;

    void Awake()
    {
        ammoCapacity = ammoCapacityMax;
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                PerformSearching();
                if (isTargetAvailable_())
                    currentState = State.Targeting;
                break;
            case State.Targeting:
                if (!isTargetAvailable_())
                {
                    currentState = State.Idle;
                    break;
                }
                PerformTargeting();
                if (isTargetLocked_())
                    StartFiring_();
                break;
            case State.Firing:
                break;
            case State.Reloading:
                break;
            default:
                break;
        }
    }

    protected abstract void PerformTargeting();

    private void PerformSearching()
    {
        switch (targetingMode)
        {
            case TargetingMode.Nearest:
                target = FindNearestTarget();
                break;
            default:
                target = null;
                break;
        }
    }

    private GameObject FindNearestTarget()
    {
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    protected bool isAmmoAvailable_() { return ammoCapacity > 0; }

    protected bool isTargetAvailable_() { return target is not null; }

    protected virtual bool isTargetLocked_() { return isTargetAvailable_(); }

    private void StartFiring_()
    {
        Debug.Log($"Start firing. Ammo capacity: {ammoCapacity}. Cooldown: {fireCooldown}");
        InvokeRepeating(nameof(Shoot_), 0f, fireCooldown);
        currentState = State.Firing;
    }

    private void StartReloading_()
    {
        Debug.Log($"Start reloading. Cooldown: {ammoCooldown}");
        Invoke(nameof(Reload_), ammoCooldown);
        currentState = State.Reloading;
    }

    private void StopFiring_()
    {
        Debug.Log($"Stop firing.");
        CancelInvoke(nameof(Shoot_));
        currentState = State.Idle;
    }
    
    private void Shoot_()
    {
        if (!isTargetAvailable_())
        {
            Debug.Log($"No target available");
            StopFiring_();
            return;
        }
        if (!isTargetLocked_())
        {
            Debug.Log($"Target not locked");
            StopFiring_();
            return;
        }
        if (!isAmmoAvailable_())
        {
            Debug.Log($"Out of ammo");
            StopFiring_();
            StartReloading_();
            return;
        }
        ammoCapacity--;
        Debug.Log($"Shooting. Remaining ammo: {ammoCapacity}");
        return;
    }

    private void Reload_()
    {
        ammoCapacity = ammoCapacityMax;
        Debug.Log($"Reloaded. Ammo capacity: {ammoCapacity}");
    }
}
