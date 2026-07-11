using UnityEngine;

public class HangarScene : MonoBehaviour
{
    public enum HangarState
    {
        None,
        BuildPlatform,
    }
    public BaseShip shipInstance;
    public HangarState hangarState { get; private set; } = HangarState.None;
    
    private void Awake()
    {
        if (shipInstance == null)
        {
            Debug.LogWarning("Ship instance is not assigned in HangarScene.");
            shipInstance = GameObject.FindAnyObjectByType<BaseShip>();
        }
    }
    
    public void SetHangarState(HangarState state)
    {
        hangarState = state;
    }
}
