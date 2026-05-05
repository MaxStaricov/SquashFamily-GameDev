using UnityEngine;

public class LeakSpawner : MonoBehaviour
{
    [SerializeField] private GameObject waterDropPrefab; // Префаб капли или эффекта воды
    [SerializeField] private float minSpawnTime = 0.5f;   // Минимальная задержка
    [SerializeField] private float maxSpawnTime = 2f;     // Максимальная задержка

    private float timeUntilNextSpawn;

    void OnEnable()
    {
        ResetTimer();
    }

    void Update()
    {
        if (Time.time > timeUntilNextSpawn)
        {
            SpawnWaterDrop();
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timeUntilNextSpawn = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnWaterDrop()
    {
        if (waterDropPrefab != null)
        {
            // Создаём каплю на позиции пробоины
            Instantiate(waterDropPrefab, transform.position, Quaternion.identity);
        }
    }
}