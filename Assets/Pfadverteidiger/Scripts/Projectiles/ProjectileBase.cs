using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 10f;
    [field: SerializeField] public uint damage { get; private set; } = 1;
    [field: SerializeField] public float lifetime { get; private set; } = 5f;

    void Start()
    {
        Invoke(nameof(DestroyProjectile), lifetime);
    }

    public void Initialize(float speed, uint damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            DestroyProjectile();
        }
    }
}