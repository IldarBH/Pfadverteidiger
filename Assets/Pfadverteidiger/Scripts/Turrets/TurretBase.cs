using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    [field: SerializeField] public uint ammoCapacityMax { get; private set; } = 30;
    [field: SerializeField] public float ammoReloadTime { get; private set; } = 0.1f;
    [field: SerializeField] public float fireReloadTime { get; private set; } = 0.5f;

    private uint ammoAvailable_ = 0;
    private uint ammoCapacity_ = 0;
    private float ammoReloadTimer_ = 0f;
    private float fireReloadTimer_ = 0f;

    [field: SerializeField] public float targetingSpeed { get; private set; } = 30f;
    [field: SerializeField] public float targetingAngleTolerance { get; private set; } = 5f;
    [field: SerializeField] public float firingRangeMin { get; private set; } = 0f;
    [field: SerializeField] public float firingRangeMax { get; private set; } = 8f;

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
    public GameObject target { get; private set; } = null;
    public TargetingMode targetingMode { get; set; } = TargetingMode.Nearest;

    protected virtual void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                ExecuteIdleState_();
                break;
            case State.Targeting:
                ExecuteTargetingState_();
                break;
            case State.Firing:
                ExecuteFiringState_();
                break;
            case State.Reloading:
                ExecuteReloadingState_();
                break;
            default:
                break;
        }
    }

    private void ExecuteIdleState_()
    {
        if (!isAmmoAvailable_())
        {
            StartReloading_();
            return;
        }
        PerformSearching_();
        if (isTargetAvailable_())
        {
            StartTargeting_();
            return;
        }
    }

    private void ExecuteTargetingState_()
    {
        if (!isTargetAvailable_())
        {
            currentState = State.Idle;
            return;
        }
        PerformTargeting_();
        if (isTargetLocked_())
        {
            StartFiring_();
        }
    }

    private void ExecuteFiringState_()
    {
        if (!isTargetAvailable_())
        {
            StopFiring_();
        }
        else if (!isTargetLocked_())
        {
            StopFiring_();
            StartTargeting_();
        }
        else if (!isAmmoAvailable_())
        {
            StopFiring_();
            StartReloading_();
        }
        else if (fireReloadTimer_ > 0f)
        {
            fireReloadTimer_ -= Time.deltaTime;
        }
        else
        {
            PerformFiring_();
        }
    }

    private void ExecuteReloadingState_()
    {
        if (ammoReloadTimer_ > 0f)
        {
            ammoReloadTimer_ -= Time.deltaTime;
        }
        else if (!isAmmoReloaded_())
        {
            PerformReloading_();
        }
        else
        {
            StopReloading_();
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

    private void PerformFiring_()
    {
        PerformFiring_Implementation_();
        ammoAvailable_--;
        ammoCapacity_--;
        fireReloadTimer_ = fireReloadTime;
    }

    private void PerformReloading_()
    {
        PerformReloading_Implementation_();
        ammoCapacity_++;
        ammoReloadTimer_ = ammoReloadTime;
    }

    private void PerformSearching_()
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

    private void PerformTargeting_()
    {
        PerformTargeting_Implementation_();
    }

    private void StartFiring_()
    {
        currentState = State.Firing;
    }

    private void StartReloading_()
    {
        ammoReloadTimer_ = ammoReloadTime;
        currentState = State.Reloading;
    }

    private void StartTargeting_()
    {
        currentState = State.Targeting;
    }

    private void StopFiring_()
    {
        currentState = State.Idle;
    }

    private void StopReloading_()
    {
        ammoAvailable_ = ammoCapacity_;
        currentState = State.Firing;
    }

    protected abstract void PerformFiring_Implementation_();

    protected abstract void PerformReloading_Implementation_();

    protected abstract void PerformTargeting_Implementation_();

    private bool isAmmoAvailable_() { return ammoAvailable_ > 0; }

    private bool isAmmoReloaded_() { return ammoCapacity_ >= ammoCapacityMax; }

    protected bool isTargetAvailable_() { return target is not null; }

    protected virtual bool isTargetLocked_() { return isTargetAvailable_(); }
}
