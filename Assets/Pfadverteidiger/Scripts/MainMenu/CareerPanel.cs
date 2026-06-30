using UnityEngine;
using UnityEngine.EventSystems;

public class CareerPanel : MonoBehaviour
{
    void Awake()
    {
        if (!gameObject.TryGetComponent<EventTrigger>(out var trigger))
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }
        UtilityHelpers.RegisterTrigger(trigger, EventTriggerType.PointerExit, (data) => OnCareerPanelExit());
    }

    private void OnCareerPanelExit()
    {
        gameObject.SetActive(false);
    }
}
