using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 75;
    [SerializeField] private float _speed = 5;

    private CinemachineCamera _playerCamera;
    private CharacterController _characterController;

    private Vector2 _rotation;
    private Vector3 _vecolity;
    private Vector2 _direction;

    private bool canMove = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerCamera = GetComponentInChildren<CinemachineCamera>();
        _characterController = GetComponentInChildren<CharacterController>();  
    }



    void Update()
    {
        if (canMove)
        {
            playerMove();
            playerRotate();
        }
    }

    private void playerRotate()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        mouseDelta *= _rotateSpeed * Time.deltaTime;
        _rotation.y += mouseDelta.x;
        _rotation.x = Mathf.Clamp(_rotation.x - mouseDelta.y, -90, 90);
        _playerCamera.transform.localEulerAngles = _rotation;
    }
    private void playerMove()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _direction *= _speed;
        Vector3 move = Quaternion.Euler(0, _playerCamera.transform.eulerAngles.y, 0) * new Vector3(_direction.x, 0, _direction.y);
        _vecolity = new Vector3(move.x, _vecolity.y, move.z);
        _characterController.Move(_vecolity * Time.deltaTime);
    }

    public void lockMove()
    {
        canMove = !canMove;
    }
}
