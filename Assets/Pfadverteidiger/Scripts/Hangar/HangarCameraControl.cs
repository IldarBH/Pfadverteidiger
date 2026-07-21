using UnityEngine;
using UnityEngine.InputSystem;

public class HangarCameraControl : MonoBehaviour
{
    public InputActionAsset _inputAsset;
    private InputActionMap _hangarActionMap;
    private InputAction _rotateAction;
    public Transform cameraTransform;
    public float rotationSpeed = 30;
    private Vector2 _rotationInput;
    private HangarScene _hangarScene;
    void Awake()
    {
        if (_inputAsset == null)
        {
            Debug.LogError("InputActionAsset is not assigned.");
            return;
        }

        _hangarActionMap = _inputAsset.FindActionMap("Hangar");
        if (_hangarActionMap == null)
        {
            Debug.LogError("Hangar InputActionMap not found.");
            return;
        }

        _rotateAction = _hangarActionMap.FindAction("Rotate");
        if (_rotateAction == null)
        {
            Debug.LogError("Rotate action not found in InputActionMap.");
            return;
        }
        _rotateAction.performed += (context) => OnRotatePerformed(context);
        _rotateAction.canceled += (context) => OnRotateCanceled();

        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera transform is not assigned.");
            cameraTransform = Camera.main.transform;
        }
    }

    public void Initialize(HangarScene hangarScene)
    {
        _hangarScene = hangarScene;
    }

    void OnEnable()
    {
        _hangarActionMap.Enable();
    }

    void OnDisable()
    {
        _hangarActionMap.Disable();
    }

    void Update()
    {
        UpdateCameraRotation();
    }

    void UpdateCameraRotation()
    {
        if (_rotationInput != Vector2.zero)
        {
            var focusPoint = _hangarScene.GetShipInstance().transform;
            float yaw = -_rotationInput.x * rotationSpeed * Time.deltaTime;
            cameraTransform.RotateAround(focusPoint.position, Vector3.up, yaw);
            
            float pitch = -_rotationInput.y * rotationSpeed * Time.deltaTime;
            var nextPitch = cameraTransform.localRotation.eulerAngles.x + pitch;
            if (nextPitch < 90f || nextPitch > 270f)
            {
                cameraTransform.RotateAround(focusPoint.position, cameraTransform.right, pitch);    
            }
        }
    }

    void OnRotatePerformed(InputAction.CallbackContext context)
    {
        _rotationInput = context.ReadValue<Vector2>();
    }

    void OnRotateCanceled()
    {
        _rotationInput = Vector2.zero;
    }
}
