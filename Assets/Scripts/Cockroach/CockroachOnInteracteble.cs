using UnityEngine;

public class CockroachOnInteracteble : MonoBehaviour, Interactable
{
    private CockroachNavigation cockroachNavigation;
    private DeadedCockroach deadedCockroachPrefab;
    private float coastKill = 1f;
    private void Awake() {
        CockroachManager.Increase(1);

        cockroachNavigation = GetComponentInParent<CockroachNavigation>();
        deadedCockroachPrefab = GetComponentInParent<DeadedCockroach>();

        return;
    }
    public void OnInteract() {
        if(!StaminaBar.Instance.CheckUpdate(-coastKill)) return;
        
        StaminaBar.Instance.UpdateValue(-coastKill);
        CockroachManager.Decrease(1);

        cockroachNavigation.BreakCoroutine();

        deadedCockroachPrefab.SpawnDeadedPrefab(cockroachNavigation.agent.transform.position);
        Destroy(cockroachNavigation.gameObject);
    }
}
