using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public uint health { get; private set; } = 10;
    private float _moveSpeed = 2f;
    private Vector3 _targetPosition = Vector3.zero;

    public void TakeDamage(uint damage)
    {
        if (damage >= health)
        {
            Destroy(gameObject);
        }
        else
        {
            health -= damage;
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}