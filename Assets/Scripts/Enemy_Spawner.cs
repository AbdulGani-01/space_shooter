using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float wait = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, wait);
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-8f, 8f);
        Vector2 spawnPos = new Vector2(randomX, 6f);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}