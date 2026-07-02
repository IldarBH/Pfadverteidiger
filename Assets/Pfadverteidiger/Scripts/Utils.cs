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
}

