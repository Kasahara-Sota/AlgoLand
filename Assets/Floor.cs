using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class Floor : MonoBehaviour
{
    [SerializeField] float _fadeDisAppearTime;
    [SerializeField] float _fadeAppearTime;
    Renderer _renderer;
    Collider _collider;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }
    public void FadeAppear()
    {
        Color color = _renderer.material.color;
        color.a = 1;
        _renderer.material.color = color;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, _fadeAppearTime).OnComplete(() => _collider.enabled = true);
    }
    public void FadeDisAppear()
    {
        Color color = _renderer.material.color;
        color.a = 0;
        DOTween.To(() => _renderer.material.color, x => _renderer.material.color = x, color, _fadeDisAppearTime).SetEase(Ease.Linear).OnComplete(() => DisAppear());
    }
    void DisAppear()
    {
        if (Physics.Raycast(transform.position - new Vector3(0, 0.1f, 0), Vector3.up, out RaycastHit hit))
        {
            hit.collider.GetComponent<PlayerPiece>().Fall();
        }
        //gameObject.SetActive(false);
        _collider.enabled = false;
        Standby();
    }
    async UniTask Standby()
    {
        await UniTask.Delay((int)_fadeDisAppearTime * 1000);
        FadeAppear();
    }
}
