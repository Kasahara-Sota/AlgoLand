using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RTClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Camera _rtCamera;           // RenderTexture を映しているカメラ
    [SerializeField] CinemachineVirtualCamera _vCam;
    [SerializeField] RawImage _rawImage;         // クリック対象の UI
    [SerializeField] InputManager _inputManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. Screen → RawImage のローカル座標
        RectTransform rt = _rawImage.rectTransform;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rt,
            eventData.position,
            null, // Screen Space - Camera ならそのカメラ、Overlayなら null
            out localPoint
        );

        // 2. ローカル座標 → UV (0～1)
        Vector2 size = rt.rect.size;
        Vector2 uv = new Vector2(
            (localPoint.x / size.x) + 0.5f,
            (localPoint.y / size.y) + 0.5f
        );

        // 3. UV → ビューポート座標に変換して Ray 生成
        Ray ray = _rtCamera.ViewportPointToRay(uv);
        // 4. Raycast 実行
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var obj = hit.collider.gameObject;
            Debug.Log($"Hit: {obj.name}");
            if(obj.CompareTag("Player"))
            {
                _inputManager.SetPlayerPiece(obj.GetComponent<PlayerPiece>());
                _vCam.Follow = obj.transform;
                _vCam.MoveToTopOfPrioritySubqueue();
            }
        }
    }
}
