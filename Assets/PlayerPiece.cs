using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class PlayerPiece : MonoBehaviour
{
    [SerializeField] float _time = 1;
    Queue<Direction> _commands;
    // Start is called before the first frame update
    void Start()
    {
        _commands = new Queue<Direction>();
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddMoveCommand(Direction direction)
    {
        _commands.Enqueue(direction);
    }
    IEnumerator Move()
    {
        while(true)
        {
            if(_commands.Count > 0)
            {
                Direction dir = _commands.Dequeue();
                bool isMoving = true;
                switch (dir)
                {
                    case Direction.Up:
                        transform.forward = Vector3.forward;
                        transform.DOMoveZ(transform.position.z + 1, _time).SetEase(Ease.Linear).OnComplete(() => isMoving = false); break;
                    case Direction.Down:
                        transform.forward = Vector3.back;
                        transform.DOMoveZ(transform.position.z - 1, _time).SetEase(Ease.Linear).OnComplete(() => isMoving = false); break;
                    case Direction.Right:
                        transform.forward = Vector3.right;
                        transform.DOMoveX(transform.position.x + 1, _time).SetEase(Ease.Linear).OnComplete(() => isMoving = false); break;
                    case Direction.Left:
                        transform.forward = Vector3.left;
                        transform.DOMoveX(transform.position.x - 1, _time).SetEase(Ease.Linear).OnComplete(() => isMoving = false); break;
                }
                while(isMoving)
                {
                    yield return null;
                }
                if(!Physics.Raycast(transform.position,Vector3.down))
                {
                    _commands.Clear();
                    Rigidbody rb = GetComponent<Rigidbody>();
                    rb.useGravity = true;
                    rb = transform.GetChild(0).GetComponent<Rigidbody>();
                    rb.useGravity = true;
                    SoundManager.Instance.PlaySE("Fall");
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
