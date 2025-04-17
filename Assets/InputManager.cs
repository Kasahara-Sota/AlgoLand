using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerPiece _playerPiece;
    List<Direction> _directions;
    // Start is called before the first frame update
    void Start()
    {
        _directions = new List<Direction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDirection(int directionId)
    {
        Direction direction = (Direction)directionId;
        _directions.Add(direction);
    }
    public void SubmitAction()
    {
        _playerPiece.Move(_directions);
        _directions.Clear();
    }
}
