using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour, Interactable
{

    [SerializeField] private GameObject list;
    private bool isEnabled = false;
    [SerializeField] private PlayerController _playerController;
    public void OnInteract()
    {
        isEnabled = !isEnabled;
        list.SetActive(isEnabled);
        _playerController.lockMove();
    }
}
