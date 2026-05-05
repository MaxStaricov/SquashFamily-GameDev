using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool isWork = true;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject cockroach;
    private float spawnInterval = 10f;



    private IEnumerator Spawn() {
        while(true) {
            yield return new WaitForSeconds(spawnInterval);

            if(CockroachManager.currentCount < CockroachManager.maxCount) {
                GameObject newCockroach = Instantiate(cockroach, spawner.transform.position, Quaternion.identity);
                // Debug.Log(CockroachManager.currentCount);
            }
        }
    }

    void Start() {
        if(!isWork) return;

        StartCoroutine(Spawn());
    }
}
