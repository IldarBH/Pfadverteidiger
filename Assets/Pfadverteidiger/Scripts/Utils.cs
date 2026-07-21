using UnityEngine;
using UnityEngine.EventSystems;

public static class UtilityHelpers
{
    public static void RegisterTrigger(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new() { eventID = type };
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
    }

    public static void RegisterEvent<T>(T component, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> callback) where T : Component
    {
        if (!component.TryGetComponent<EventTrigger>(out var trigger))
        {
            trigger = component.gameObject.AddComponent<EventTrigger>();
        }
        RegisterTrigger(trigger, type, callback);
    }

    public static Quaternion GetRandomRotation(
        (float minRoll, float maxRoll) rollRange, 
        (float minPitch, float maxPitch) pitchRange, 
        (float minYaw, float maxYaw) yawRange)
    {

        float roll = UnityEngine.Random.Range(rollRange.minRoll, rollRange.maxRoll);
        float pitch = UnityEngine.Random.Range(pitchRange.minPitch, pitchRange.maxPitch);
        float yaw = UnityEngine.Random.Range(yawRange.minYaw, yawRange.maxYaw);
        return Quaternion.Euler(pitch, yaw, roll);
    }

    public static Quaternion GetRandomRotation()
    {
        return GetRandomRotation((0f, 360f), (0f, 360f), (0f, 360f));
    }

    public static Vector3 GetRandomPosition(
        (float minX, float maxX) xRange,
        (float minY, float maxY) yRange,
        (float minZ, float maxZ) zRange)
    {
        float x = UnityEngine.Random.Range(xRange.minX, xRange.maxX);
        float y = UnityEngine.Random.Range(yRange.minY, yRange.maxY);
        float z = UnityEngine.Random.Range(zRange.minZ, zRange.maxZ);
        return new Vector3(x, y, z);
    }
    
    public static Vector3 GetRandomPosition(Bounds bounds)
    {
        return GetRandomPosition(
            (bounds.min.x, bounds.max.x), (bounds.min.y, bounds.max.y), (bounds.min.z, bounds.max.z)
        );
    }
}
