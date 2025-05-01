using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    PlayerPiece _playerPiece;
    PlayerInput _playerInput;
    Vector2 _moveInput;
    public Vector2 MoveInput => _moveInput;
    void Start()
    {
        Instance = this;
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Move"].performed += OnMove;
        _playerInput.actions["Move"].canceled += StopMove;
    }
    void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }
    void StopMove(InputAction.CallbackContext callbackContext)
    {
        _moveInput = Vector2.zero;
    }
    void Update()
    {
    }
    public void SetPlayerPiece(PlayerPiece playerPiece)
    {
        _playerPiece = playerPiece;
    }
    public void SetDirection(int directionId)
    {
        if(_playerPiece != null)
        {
            Direction direction = (Direction)directionId;
            _playerPiece.AddMoveCommand(direction);
        }
    }
}
