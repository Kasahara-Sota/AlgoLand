using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class PlayerPiece : MonoBehaviour
{
    [SerializeField] float _time = 1;
    Sequence _seq;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Move(List<Direction> commands)
    {
        Debug.Log($"command count:{commands.Count}");
        _seq = DOTween.Sequence();
        Vector3 pos = transform.position;
        for (int i = 0; i < commands.Count; i ++)
        {
            Direction dir = commands[i];
            Debug.Log(dir.ToString());
            bool isComplete = false;
            switch (dir)
            {
                case Direction.Up:
                    pos.z += 1;
                    _seq.Append(transform.DOMoveZ(pos.z, _time)); break;
                case Direction.Down:
                    pos.z -= 1;
                    _seq.Append(transform.DOMoveZ(pos.z, _time)); break;
                case Direction.Right:
                    pos.x += 1;
                    _seq.Append(transform.DOMoveX(pos.x, _time)); break;
                case Direction.Left:
                    pos.x -= 1;
                    _seq.Append(transform.DOMoveX(pos.x, _time)); break;
            }
            _seq.Play();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
