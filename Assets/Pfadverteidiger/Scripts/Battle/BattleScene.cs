using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : MonoBehaviour
{
    public float SpawnInterval = 2f;
    private float _spawnTimer = 0f;
    private Collider _spawnArea;
    private Collider _targetArea;
    public Vector3 MinSceneCorner = new Vector3(-300f, -50f, -50f);
    public Vector3 MaxSceneCorner = new Vector3(300f, 50f, 310f);
    private Bounds _sceneBounds;

    void Awake()
    {
        _spawnArea = GameObject.Find("SpawnArea").GetComponent<Collider>();
        _targetArea = GameObject.Find("TargetArea").GetComponent<Collider>();

        var sceneBoundsCenter = (MinSceneCorner + MaxSceneCorner) / 2f;
        var sceneBoundsSize = MaxSceneCorner - MinSceneCorner;
        _sceneBounds = new Bounds(sceneBoundsCenter, sceneBoundsSize);
        BattleManager.PrepareBattle();
    }

    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= SpawnInterval)
        {
            SpawnEnemy();
            _spawnTimer = 0f;
        }
    }

    public void OnEnemyCollidedWithPlayer(EnemyData enemyData)
    {
        GameManager.Ship.Hit((uint)enemyData.Damage);
        Debug.Log($"Armor: {GameManager.Ship.Armor.Value}, Health: {GameManager.Ship.Health.Value}");
        if (GameManager.Ship.Health.Value <= 0)
        {
            Debug.Log("Player has been defeated!");
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void SpawnEnemy()
    {
        EnemyData nextEnemy = BattleManager.GetNextEnemy();
        if (nextEnemy == null)
        {
            Debug.Log("No more enemies to spawn.");
            return;
        }

        var enemyPrefab = Resources.Load<GameObject>(nextEnemy.PrefabPath);
        var randomPosition = UtilityHelpers.GetRandomPosition(_spawnArea.bounds);
        var randomRotation = UtilityHelpers.GetRandomRotation();
        var enemyInstance = Instantiate(enemyPrefab, randomPosition, randomRotation);

        var enemyController = enemyInstance.GetComponent<EnemyController>();
        if (enemyController == null)
        {
            Debug.LogWarning("Enemy prefab does not have an EnemyController component. Adding one.");
            enemyController = enemyInstance.AddComponent<EnemyController>();
        }
        var targetPosition = UtilityHelpers.GetRandomPosition(_targetArea.bounds);
        enemyController.Initialize(this, nextEnemy, targetPosition);
        Debug.DrawLine(randomPosition, targetPosition, Color.red, 100f);
    }

    public bool IsWithinSceneBounds(Vector3 position)
    {
        return _sceneBounds.Contains(position);
    }
}
