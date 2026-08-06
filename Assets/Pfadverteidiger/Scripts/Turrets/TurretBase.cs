using UnityEngine;

public class TurretBase : MonoBehaviour
{
    public float targetingSpeed = 20f;
    public float targetingAngleTolerance = 5f;
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

    protected virtual void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                PerformSearching();
                if (isTargetAvailable())
                {
                    currentState = State.Targeting;
                    break;
                }
                break;
            case State.Targeting:
                if (!isTargetAvailable())
                {
                    currentState = State.Idle;
                    break;
                }
                PerformTargeting();
                if (isTargetLocked())
                {
                    StartFiring();
                    currentState = State.Firing;
                    break;
                }
                break;
            case State.Firing:
                if (!isTargetAvailable())
                {
                    currentState = State.Idle;
                    break;
                }
                PerformTargeting();
                if (isAmmoAvailable())
                {
                    PerformFiring();
                } 
                else
                {
                    StartReloading();
                    currentState = State.Reloading;
                    break;
                }
                break;
            case State.Reloading:
                PerformReloading();
                if (isAmmoAvailable())
                {
                    currentState = State.Targeting;
                    break;
                }
                break;
            default:
                break;
        }
    }

    protected virtual void PerformTargeting() {}
    protected virtual void StartFiring() {}
    protected virtual void PerformFiring() {}
    protected virtual void StartReloading() {}
    protected virtual void PerformReloading() {}

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

    private bool isTargetAvailable()
    {
        return target is not null;
    }

    private bool isTargetLocked()
    {
        if (target == null)
            return false;
        var direction = target.transform.position - endPoint.position;
        var angle = Vector3.Angle(endPoint.forward, direction);
        return angle < targetingAngleTolerance;
    }

    private bool isAmmoAvailable()
    {
        return true; // Placeholder
    }
}
