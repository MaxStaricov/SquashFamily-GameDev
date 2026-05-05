using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CockroachNavigation : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent{get; private set;}
    private Coroutine coroutine{get; set;} = null;
    private int maxAttempts = 10;
           
    public bool scared{get; private set;} = false;
    public float calmedSpeed{get; private set;} = 0.5f;
    public float scaredSpeed{get; private set;} = 2f;
    public float currentSpeed{get; private set;}
    public float searchRadius{get; private set;} = 10f;




    private void Awake() {
        agent = (agent == null) ? GetComponent<NavMeshAgent>() : agent;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        coroutine = StartCoroutine(Crawl(searchRadius, calmedSpeed));

        return;
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float radius, int maxAttempts = 10) {
        for (int i = 0; i < maxAttempts; i++) {
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection *= radius;
            Vector3 samplePoint = center + randomDirection;

            if(NavMesh.SamplePosition(samplePoint, out NavMeshHit hit, radius, NavMesh.AllAreas)) return hit.position;
        }

        return center;
    }

    public void BreakCoroutine() {
        if(coroutine != null) {
            StopCoroutine(coroutine);
        }

        coroutine = null;

        return;
    }

    private IEnumerator Crawl(float radius, float speed) {
        agent.speed = speed;
        while(true) {
            Vector3 point = GetRandomPointOnNavMesh(this.transform.position, radius, maxAttempts);
            agent.SetDestination(point);

            while(agent.pathPending || agent.remainingDistance > 0.5f) {
                yield return null;
            }

            yield return null;
        }
    }

    private IEnumerator Scare() {
        scared = true;

        agent.speed = scaredSpeed;
        // Debug.Log("Scared!");

        yield break;
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player") || scared) return;

        StartCoroutine(Scare());

        return;
    }

    private IEnumerator CalmDown() {
        scared = false;

        yield return new WaitForSeconds(2f);
        if(scared) yield break;

        agent.speed = calmedSpeed;
        // Debug.Log("Calm");

        yield break;
    }

    private void OnTriggerExit(Collider other) {
        if(!other.CompareTag("Player") || !scared) return;

        StartCoroutine(CalmDown());
    }




    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, searchRadius);

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(GetComponent<Collider>().transform.position, 2.3f);
    // }
}
