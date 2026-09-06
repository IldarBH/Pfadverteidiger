using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public uint health { get; private set; } = 10;
    [field: SerializeField] public float orbitSpeed { get; private set; } = 45f;

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
        transform.RotateAround(Vector3.zero, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}