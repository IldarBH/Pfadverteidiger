using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTopControl : MonoBehaviour
{
    public InputActionAsset actionAsset;
    private InputActionMap _actionMap;
    private InputAction _moveAction;
    private InputAction _rotateAction;
    private Camera _camera;
    private Vector3 _focusPoint = Vector3.zero;
    private Vector2 _moveValue = Vector2.zero;
    private Vector2 _rotateValue = Vector2.zero;
    private float _moveStartTime = float.NaN;
    private float _rotateStartTime = float.NaN;
    public float moveSpeed = 10f;
    public float rotateSpeed = 30f;
    public float actionUpdateDelay = 0.2f;
    private Vector2 _pitchRange = new Vector2(30f, 90f);

    void Awake()
    {
        _camera = gameObject.GetComponent<Camera>();
        _actionMap = actionAsset.FindActionMap("TopCamera");
        _moveAction = actionAsset.FindAction("Move");
        _moveAction.started += (data) => OnMoveStart((InputAction.CallbackContext) data);
        _moveAction.performed += (data) => OnMovePerformed((InputAction.CallbackContext) data);
        _moveAction.canceled += (data) => OnMoveCanceled((InputAction.CallbackContext) data);

        _rotateAction = actionAsset.FindAction("Rotate");
        _rotateAction.started += (data) => OnRotateStart((InputAction.CallbackContext) data);
        _rotateAction.performed += (data) => OnRotatePerformed((InputAction.CallbackContext) data);
        _rotateAction.canceled += (data) => OnRotateCanceled((InputAction.CallbackContext) data);

    }

    void OnEnable()
    {
        _actionMap.Enable();

        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        float enterDistance = 0f;
        if (groundPlane.Raycast(ray, out enterDistance)) {
            _focusPoint = ray.GetPoint(enterDistance);
            Debug.Log("Hit mathematical plane at: " + _focusPoint);
        } else {
            Debug.LogError("Failed to RayCast");
        }
    }

    void OnDisable()
    {
    }

    private void OnMoveStart(InputAction.CallbackContext context)
    {
        _moveStartTime = Time.time;
    }
    
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveValue = _moveAction.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveStartTime = float.NaN;
        _moveValue = Vector2.zero;
    }

    private void OnRotateStart(InputAction.CallbackContext context)
    {
        _rotateStartTime = Time.time;
    }
    
    private void OnRotatePerformed(InputAction.CallbackContext context)
    {
        _rotateValue = _rotateAction.ReadValue<Vector2>();
    }

    private void OnRotateCanceled(InputAction.CallbackContext context)
    {
        _rotateStartTime = float.NaN;
        _rotateValue = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        if (float.IsNaN(_moveStartTime)) return;
        if (Time.time - _moveStartTime < actionUpdateDelay) return;
        
        var frontDir = Vector3.ProjectOnPlane(transform.up, Vector3.up).normalized;
        var rightDir = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
        var delta_dir = frontDir * _moveValue.y + rightDir * _moveValue.x;
        var delta = moveSpeed * delta_dir * Time.deltaTime;
        transform.position += delta;
        _focusPoint += delta;
    }

    private void UpdateRotation()
    {
        if (float.IsNaN(_rotateStartTime)) return;
        if (Time.time - _rotateStartTime < actionUpdateDelay) return;
        
        float pitchAngle = rotateSpeed * _rotateValue.y * Time.deltaTime;
        transform.RotateAround(_focusPoint, transform.right, pitchAngle);
        float yawAngle = rotateSpeed * _rotateValue.x * Time.deltaTime;
        transform.RotateAround(_focusPoint, Vector3.up, yawAngle);
    }
}
