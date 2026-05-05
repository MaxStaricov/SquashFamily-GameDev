using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class RoomLight : MonoBehaviour, Interactable
{
    [SerializeField] private Light lamp;
    private bool isEnabled = false;

    public void OnInteract()
    {
        isEnabled = !isEnabled;
        lamp.gameObject.SetActive(isEnabled);
    }
}
