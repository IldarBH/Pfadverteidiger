using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public uint health { get; private set; } = 10;
    private float _moveSpeed = 10f;
    private Vector3 _targetPosition = Vector3.zero;
    private Collider _collider;    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy triggered with the player!");
        } else if (other.CompareTag("TargetArea"))
        {
            Debug.Log("Enemy triggered the target area!");
            _collider.enabled = false;
            Destroy(gameObject, 5f);
        }
    }
}