using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public float spawnIntervalSeconds = 5f;
    private Bounds _spawnAreaBounds;
    private Bounds _targetAreaBounds;

    void Awake()
    {
        _spawnAreaBounds = transform.Find("SpawnArea").GetComponent<Collider>().bounds;
        _targetAreaBounds = transform.Find("TargetArea").GetComponent<Collider>().bounds;
    }

    void OnEnable()
    {
        InvokeRepeating(nameof(SpawnCallback), spawnIntervalSeconds, spawnIntervalSeconds);
    }

    void OnDisable()
    {
        CancelInvoke(nameof(SpawnCallback));
    }

    public void SpawnCallback()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Spawner has no enemy prefab assigned.", this);
            return;
        }

        var startPosition = new Vector3(
            Random.Range(_spawnAreaBounds.min.x, _spawnAreaBounds.max.x),
            Random.Range(_spawnAreaBounds.min.y, _spawnAreaBounds.max.y),
            Random.Range(_spawnAreaBounds.min.z, _spawnAreaBounds.max.z)
        );
        var targetPosition = new Vector3(
            Random.Range(_targetAreaBounds.min.x, _targetAreaBounds.max.x),
            Random.Range(_targetAreaBounds.min.y, _targetAreaBounds.max.y),
            Random.Range(_targetAreaBounds.min.z, _targetAreaBounds.max.z)
        );
        var enemy = Instantiate(enemyPrefab, startPosition, Quaternion.identity);
        enemy.SetTargetPosition(targetPosition);
    }
}
