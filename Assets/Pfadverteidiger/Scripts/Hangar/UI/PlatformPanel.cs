using UnityEngine;

public class PlatformPanel : MonoBehaviour
{
    private Transform _platformTransform;
    public void Initialize(Transform platformInstance)
    {
        _platformTransform = platformInstance;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        var platformPosition = _platformTransform.GetComponent<Renderer>().bounds.center;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(platformPosition);
        transform.position = screenPosition;
    }

    private void Update()
    {
        UpdatePosition();
    }
}
