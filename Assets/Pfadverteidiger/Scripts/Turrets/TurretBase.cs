using System.Collections;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    public enum State
    {
        Idle,
        Targeting,
        Firing,
        Reloading
    }
    public State currentState { get; protected set; } = State.Idle;
    private uint ammoAvailable_ = 0;
    private float ammoReloadTimer_ = 0f;
    private float fireReloadTimer_ = 0f;
    private Animator animator_ = null;
    private Coroutine disableAnimatorRoutine_ = null;
    protected TurretData data_ = null;
    protected TargetTracker targetTracker_ = null;
    
    protected void Initialize(TurretData _data)
    {
        if (_data == null)
        {
            Debug.LogError($"{nameof(TurretBase)} received null turret data on {gameObject.name}.", gameObject);
            enabled = false;
            return;
        }
        data_ = _data;
        animator_ = gameObject.GetComponent<Animator>();
        if (animator_ == null)
        {
            Debug.LogError($"Missing {nameof(Animator)} component on {gameObject.name}.", gameObject);
            enabled = false;
            return;
        }
        targetTracker_ = new TargetTracker(transform, data_.projectileSpeed);
    }

    void OnEnable()
    {
        if (animator_ == null)
            return;

        if (disableAnimatorRoutine_ != null)
        {
            StopCoroutine(disableAnimatorRoutine_);
            disableAnimatorRoutine_ = null;
        }
        animator_.enabled = true;
        animator_.SetBool("active", true);
        disableAnimatorRoutine_ = StartCoroutine(DisableAnimatorAfterCompletion_());
    }

    protected virtual void Update()
    {
        if (targetTracker_ == null || data_ == null)
            return;

        targetTracker_.Update();
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

    /// <summary>
    /// Handles idle behavior and transitions to other states.
    /// 1. If ammo is not available, transition to Reloading state.
    /// 2. If a target is available, transition to Targeting state.
    /// 3. Otherwise, remain in Idle state.
    /// </summary>
    private void ExecuteIdleState_()
    {
        if (!isAmmoAvailable_())
        {
            StartReloading_();
            return;
        }
        if (isTargetAvailable_())
        {
            StartTargeting_();
            return;
        }
    }

    /// <summary>
    /// Handles targeting behavior and transitions to other states.
    /// 1. If no target is available, transition to Idle state.
    /// 2. If the target is locked, transition to Firing state.
    /// </summary>
    private void ExecuteTargetingState_()
    {
        if (!isTargetAvailable_())
        {
            currentState = State.Idle;
            return;
        }
        if (targetTracker_.isForecastAvailable())
        {
            PerformTargeting_();
        }
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
            return;
        }
        if (!isTargetLocked_())
        {
            StopFiring_();
            StartTargeting_();
            return;
        }
        if (!isAmmoAvailable_())
        {
            StopFiring_();
            StartReloading_();
            return;
        }
        PerformTargeting_();
        if (fireReloadTimer_ > 0f)
        {
            fireReloadTimer_ -= Time.deltaTime;
        } else
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
        else if(isAmmoReloaded_())
        {
            StopReloading_();
        }
        else
        {
            PerformReloading_();
        }
    }


    private void PerformFiring_()
    {
        PerformFiring_Implementation_();
        ammoAvailable_--;
        fireReloadTimer_ = data_.firingReloadRate;
    }

    private void PerformReloading_()
    {
        PerformReloading_Implementation_();
        ammoAvailable_++;
        ammoReloadTimer_ = data_.ammoReloadRate;
    }


    private void PerformTargeting_()
    {
        PerformTargeting_Implementation_();
    }

    private void StartFiring_()
    {
        Debug.Log($"Starting to fire at target: {targetTracker_.target.name}.", gameObject);
        currentState = State.Firing;
    }

    private void StartReloading_()
    {
        Debug.Log($"Starting to reload.", gameObject);
        ammoReloadTimer_ = data_.ammoReloadRate;
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
        Debug.Log($"Finished reloading. Ammo capacity: {ammoAvailable_}/{data_.ammoCapacityMax}.", gameObject);
        currentState = State.Firing;
    }

    protected abstract void PerformFiring_Implementation_();

    protected abstract void PerformReloading_Implementation_();

    protected abstract void PerformTargeting_Implementation_();

    private bool isAmmoAvailable_() { return ammoAvailable_ > 0; }

    private bool isAmmoReloaded_() { return ammoAvailable_ >= data_.ammoCapacityMax; }

    protected bool isTargetAvailable_() { return targetTracker_.isTargetAvailable(); }

    protected virtual bool isTargetLocked_() { return isTargetAvailable_(); }

    private IEnumerator DisableAnimatorAfterCompletion_()
    {
        yield return null;

        while (animator_ != null && (animator_.IsInTransition(0) || animator_.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f))
        {
            yield return null;
        }

        if (animator_ != null)
        {
            animator_.enabled = false;
        }

        disableAnimatorRoutine_ = null;
    }
}
