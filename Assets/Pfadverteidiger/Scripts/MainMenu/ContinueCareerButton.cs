using UnityEngine;
using UnityEngine.EventSystems;

public class ContinueCareerButton : MonoBehaviour
{
    void Awake()
    {
        if (!gameObject.TryGetComponent<EventTrigger>(out var trigger))
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }
        UtilityHelpers.RegisterTrigger(trigger, EventTriggerType.PointerClick, (data) => OnButtonClick());
    }

    private void OnButtonClick()
    {
        Debug.Log("Continue Career Button Clicked");
        GameManager.LoadCareer();
    }
}
