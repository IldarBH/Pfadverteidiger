using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string PlayerTag = "Player";
    private Vector3 _moveDirection;
    private EnemyData _enemyData;
    private BattleScene _battleScene;

    void Update()
    {
        transform.Translate(_moveDirection * _enemyData.Speed * Time.deltaTime, Space.World);
        if (!_battleScene.IsWithinSceneBounds(transform.position))
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(BattleScene battleScene, EnemyData enemyData, Vector3 targetPosition)
    {
        _battleScene = battleScene;
        _enemyData = enemyData;
        _moveDirection = (targetPosition - transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            Debug.Log($"Enemy {gameObject.name} collided with Player.");
            _battleScene.OnEnemyCollidedWithPlayer(_enemyData);
            Destroy(gameObject);
        }
    }
}
