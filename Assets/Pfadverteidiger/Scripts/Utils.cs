using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
}
