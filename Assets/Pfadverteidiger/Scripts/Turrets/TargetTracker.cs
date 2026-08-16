using UnityEngine;

public enum TargetingMode
{
    Nearest
}

public class TargetTracker
{
    public TargetingMode targetingMode { get; set; } = TargetingMode.Nearest;
    public Transform target { get; private set; } = null;
    public Vector3 targetPrevPose { get; private set; } = Vector3.zero;
    public Vector3 targetForecast { get; private set; } = Vector3.zero;
    public Vector3 targetVelocity { get; private set; } = Vector3.zero;
    
    private Transform turret_ = null;
    private float projectileSpeed_ = float.NaN;
    private bool isForecastAvailable_ = false;

    public TargetTracker(Transform turret, float projectileSpeed)
    {
        if (turret is null)
        {
            throw new System.ArgumentNullException("turret", "Turret transform cannot be null.");
        }
        if (float.IsNaN(projectileSpeed) || projectileSpeed <= 0f)
        {
            throw new System.ArgumentOutOfRangeException("projectileSpeed", "Projectile speed must be a positive number.");
        }
        turret_ = turret;
        projectileSpeed_ = projectileSpeed;
    }

    public void Update()
    {
        PerformSearching_();
        PerformForecasting_();
    }

    public bool PerformForecasting_()
    {
        if (!isTargetAvailable())
        {
            targetForecast = Vector3.zero;
            isForecastAvailable_ = false;
            return false;
        }
        var targetDistance = target.position - turret_.position;
        Debug.DrawRay(turret_.position, targetDistance, Color.yellow);
        targetVelocity = (target.position - targetPrevPose) / (Time.deltaTime + 1e-6f);
        Debug.DrawRay(targetPrevPose, targetVelocity, Color.magenta);
        targetPrevPose = target.position;

        var t = CalculateFlightTime_(targetDistance, targetVelocity, projectileSpeed_);
        if (float.IsNaN(t))
        {
            targetForecast = Vector3.zero;
            isForecastAvailable_ = false;
            return false;
        }
        targetForecast = target.position + targetVelocity*t;
        isForecastAvailable_ = true;
        Debug.DrawRay(turret_.position, targetForecast - turret_.position, Color.cyan);
        return true;
    }

    private bool PerformSearching_()
    {
        switch (targetingMode)
        {
            case TargetingMode.Nearest:
                return FindNearestTarget_();
            default:
                target = null;
                return false;
        }
    }

    private bool FindNearestTarget_()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(turret_.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                SetTarget_(enemy.transform);
            }
        }
        return isTargetAvailable();
    }

    private float CalculateFlightTime_(Vector3 distance, Vector3 targetSpeed, float projectileSpeed)
    {
        var Vp = projectileSpeed;
        var Vt = targetSpeed.magnitude;
        var D = distance.magnitude;
        // (Vp*t)^2 = (Vt*t)^2 + D^2 - 2*D*Vt*t*cos(theta), dot(D, Vt) = D*Vt*cos(theta)
        // (Vp*t)^2 = (Vt*t)^2 + D^2 - 2*dot(D, Vt)*t
        // (Vp^2 - Vt^2)*t^2 + 2*dot(D, Vt)*t - D^2 = 0
        //      Ka      *t^2 +      Kb     *t + Kc  = 0
        float Ka = Mathf.Pow(Vp, 2f) - Mathf.Pow(Vt, 2f);
        float Kb = 2f * Vector3.Dot(distance, targetSpeed);
        float Kc = -Mathf.Pow(D, 2f);
        var discriminant = Mathf.Pow(Kb, 2f) - 4f * Ka * Kc;
        if (discriminant < 0f)
        {
            return float.NaN; // No real solution, target cannot be hit
        }
        float sqrtDiscriminant = Mathf.Sqrt(discriminant);
        float t1 = (-Kb + sqrtDiscriminant) / (2f * Ka);
        float t2 = (-Kb - sqrtDiscriminant) / (2f * Ka);
        float t = Mathf.Max(t1, t2);
        if (t < 0f)
        {
            return float.NaN;
        }
        return t;
    }

    public bool isTargetAvailable() { return target is not null; }
    
    public bool isForecastAvailable() { return isForecastAvailable_; }

    private void SetTarget_(Transform newTarget) 
    { 
        if (newTarget == target)
            return;
        target = newTarget; 
        targetPrevPose = newTarget.position;
        targetForecast = newTarget.position;
        isForecastAvailable_ = false;
    }
}
