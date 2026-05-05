using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Bed : MonoBehaviour,Interactable
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CinemachineCamera _camera;
    private bool _isActive = false;

    public void OnInteract()
    {
        if (!_isActive)
        {
            _playerController.lockMove();
        }
        else
        {
            StartCoroutine(Delay());
        }
        _isActive = !_isActive;
        if (_isActive)
        {
            TimeManager.setMultiply(1000f);
        }
        else
        {
            TimeManager.setMultiply(100f);
        }
        _camera.gameObject.SetActive(_isActive);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        _playerController.lockMove();
    }
}
