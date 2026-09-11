using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 _moveDirection;

    public void SetMoveDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }
    
    void Update()
    {
        if (_moveDirection != Vector3.zero)
        {
            transform.Translate(_moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
