using UnityEngine;
using UnityEngine.Pool;

public class ProjectileBase : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 10f;
    [field: SerializeField] public uint damage { get; private set; } = 1;
    [field: SerializeField] public float lifetime { get; private set; } = 5f;
    private float lifetimeTimer_ = 0f;

    private IObjectPool<ProjectileBase> projectilePool_ = null;

    public void Initialize(float speed, uint damage, float lifetime, IObjectPool<ProjectileBase> projectilePool)
    {
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;
        this.projectilePool_ = projectilePool;
        lifetimeTimer_ = 0f;
    }

    void Update()
    {
        lifetimeTimer_ += Time.deltaTime;
        if (lifetimeTimer_ >= lifetime)
        {
            projectilePool_.Release(this);
            return;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            projectilePool_.Release(this);
        }
    }

    public void ResetTimer()
    {
        lifetimeTimer_ = 0f;
    }
}