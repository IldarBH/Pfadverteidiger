using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NewCareerButton : MonoBehaviour
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
        GameManager.StartNewCareer("NewPlayer");
        SceneManager.LoadScene("CareerMenu");
    }
}
