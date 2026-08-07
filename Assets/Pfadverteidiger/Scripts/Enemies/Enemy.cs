using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public uint health { get; private set; } = 10;

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
}