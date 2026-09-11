using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Transform playerTransform;
    private BoundingSphere poseBounds = new BoundingSphere(Vector3.zero, 12f);
    public float maxDeviation = 1f;
    public InputActionAsset inputActions;
    public float moveSpeed = 5f;
    private InputActionMap _inputActionMap;
    private InputAction _moveAction;
    private Vector3 _moveInputDirection = Vector3.zero;

    private void Awake()
    {
        _inputActionMap = inputActions.FindActionMap("BattleControl");
        if (_inputActionMap == null) {
            Debug.LogError("BattleControl action map not found in the input actions.");
            return;
        }
        _moveAction = _inputActionMap.FindAction("Move");
        if (_moveAction == null) {
            Debug.LogError("Move action not found in the input action map.");
        } else {
            _moveAction.started += MoveStarted;
            _moveAction.performed += MovePerformed;
            _moveAction.canceled += MoveCanceled;
        }
    }

    private void OnEnable()
    {
        _inputActionMap?.Enable();
    }
    
    private void OnDisable()
    {
        _inputActionMap?.Disable();
    }

    private void MoveStarted(InputAction.CallbackContext context)
    {
        // TODO: Implement logic for when the move action starts, if needed.
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0);
        _moveInputDirection = moveDirection.normalized;
    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {
        Vector3 moveDirection = Vector3.zero;
        _moveInputDirection = moveDirection.normalized;
    }

    private Vector3 GetOutOfBoundsPenalty_()
    {
        if (Vector3.Distance(playerTransform.position, poseBounds.position) > poseBounds.radius)
        {
            var direction = (playerTransform.position - poseBounds.position).normalized;
            var anchor = poseBounds.position + direction * poseBounds.radius;
            var deviation = anchor - playerTransform.position;
            var penaltymagnitude = Mathf.Clamp(deviation.magnitude / maxDeviation, 0f, 1f);
            var penalty = deviation.normalized * penaltymagnitude;
            Debug.DrawRay(playerTransform.position, penalty, Color.red);
            Debug.Log($"Out of bounds deviation: {deviation.magnitude}, penalty: {penalty.magnitude}");
            return penalty;
        }
        return Vector3.zero;
    }

    void Update()
    {
        Vector3 outOfBoundsPenalty = GetOutOfBoundsPenalty_();
        Vector3 finalMoveDirection = _moveInputDirection + outOfBoundsPenalty;
        playerTransform.Translate(finalMoveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
