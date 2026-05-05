using UnityEngine;

public class DeadedCockroach : MonoBehaviour
{
    [SerializeField] private GameObject deadedPrefab;
    private float prefabLifeTime = 10f;





    public void SpawnDeadedPrefab(Vector3 position) {
        GameObject prefab = Instantiate(deadedPrefab, position, Quaternion.identity);

        Destroy(prefab, prefabLifeTime);
    }
}
