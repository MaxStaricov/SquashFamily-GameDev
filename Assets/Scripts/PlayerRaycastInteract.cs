using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRaycastInteract : MonoBehaviour
{
    private CinemachineCamera _playerCamera;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float _raycastdistance = 2f;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject aim;
    private Boolean isActive = true;
    private Boolean isActiveWithLongIntecat = true;

    private Interactable currentInteractable;

    void Start()
    {
        _playerCamera = GetComponentInChildren<CinemachineCamera>();
    }


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _raycastdistance, interactableLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (hit.collider.CompareTag("InteractonUI"))
                {
                    canvas.SetActive(isActiveWithLongIntecat);
                }
                else
                {
                    canvas.SetActive(true);
                }

                currentInteractable = interactable;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    isActive = !isActive;
                    if (hit.collider.CompareTag("InteractonUI"))
                    {
                        isActiveWithLongIntecat = !isActiveWithLongIntecat;
                        aim.SetActive(isActiveWithLongIntecat);
                    }
                    interactable.OnInteract();
                }
            }
            else
            {
                currentInteractable = null;
            }
        }
        else 
        {
            canvas.SetActive(false);
        }
    }

}