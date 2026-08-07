using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 10f;

    public void Initialize(float bulletSpeed)
    {
        speed = bulletSpeed;

    }

    void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
