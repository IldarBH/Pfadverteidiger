using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CareerButton : MonoBehaviour
{
    private GameObject _careerPanel;
    void Awake()
    {
        if (!gameObject.TryGetComponent<EventTrigger>(out var trigger))
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }
        UtilityHelpers.RegisterTrigger(trigger, EventTriggerType.PointerEnter, (data) => OnCareerButtonHover(data));

        _careerPanel = transform.Find("CareerPanel").gameObject;
        _careerPanel.SetActive(false);
    }

    private void OnCareerButtonHover(BaseEventData data)
    {
        _careerPanel.SetActive(true);
    }
}
