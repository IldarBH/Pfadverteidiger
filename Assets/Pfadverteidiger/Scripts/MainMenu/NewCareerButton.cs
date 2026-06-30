using UnityEngine;
using UnityEngine.EventSystems;

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
        Debug.Log("New Career Button Clicked");
        var player = PlayerData.CreateInstance("NewPlayer", 1000);
        var savePath = SaveSystem.SavePlayerData(player);
        Debug.Log($"New player data saved to: {savePath}");
    }
}
