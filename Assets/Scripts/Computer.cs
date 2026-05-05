using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject _camera;
    private bool _enabled = false;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Image cursor;
    [SerializeField] private GameObject apps;

    public void OnInteract()
    {
        if( !_enabled)
        {
            _playerController.lockMove();
        }
        else
        {
            apps.gameObject.SetActive(false);
            StartCoroutine(Delay());
        }
        _enabled = !_enabled;
        cursor.gameObject.SetActive(_enabled);
        _camera.SetActive(_enabled);
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        _playerController.lockMove();
    }
}