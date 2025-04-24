using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerPiece _playerPiece;
    void Start()
    {
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
