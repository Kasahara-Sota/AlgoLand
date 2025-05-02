using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Floor : MonoBehaviour
{
    [SerializeField] float _fadeTime;
    public void FadeDisAppear()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = 0;
        DOTween.To(() => renderer.material.color, x => renderer.material.color = x, color, _fadeTime).SetEase(Ease.Linear).OnComplete(() => DisAppear());
    }
    void DisAppear()
    {
        if (Physics.Raycast(transform.position - new Vector3(0, 0.1f, 0), Vector3.up, out RaycastHit hit))
        {
            hit.collider.GetComponent<PlayerPiece>().Fall();
        }
        gameObject.SetActive(false);
    }
}
