using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public MovementControl movementControl;
    public InputActionAsset inputActions;
    private InputActionMap _inputActionMap;
    private InputAction _moveAction;

    private void Awake()
    {
        _inputActionMap = inputActions.FindActionMap("BattleControl");
        if (_inputActionMap == null)
        {
            Debug.LogError("BattleControl action map not found in the input actions.");
            return;
        }
        _moveAction = _inputActionMap.FindAction("Move");
        if (_moveAction == null)
        {
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
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        movementControl.SetMoveDirection(moveDirection);
    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {
        Vector3 moveDirection = Vector3.zero;
        movementControl.SetMoveDirection(moveDirection);
    }
}
