using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RTClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Camera rtCamera;           // RenderTexture を映しているカメラ
    public RawImage rawImage;         // クリック対象の UI

    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. Screen → RawImage のローカル座標
        RectTransform rt = rawImage.rectTransform;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rt,
            eventData.position,
            null, // Screen Space - Camera ならそのカメラ、Overlayなら null
            out localPoint
        ); // :contentReference[oaicite:10]{index=10}

        // 2. ローカル座標 → UV (0～1)
        Vector2 size = rt.rect.size;
        Vector2 uv = new Vector2(
            (localPoint.x / size.x) + 0.5f,
            (localPoint.y / size.y) + 0.5f
        ); // :contentReference[oaicite:11]{index=11}

        // 3. UV → ビューポート座標に変換して Ray 生成
        Ray ray = rtCamera.ViewportPointToRay(uv); // :contentReference[oaicite:12]{index=12}
        // 4. Raycast 実行
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var obj = hit.collider.gameObject;
            // 例：対象オブジェクトの情報取得
            Debug.Log($"Hit: {obj.name}");
            // メソッド呼び出し
            //obj.SendMessage("OnClicked", SendMessageOptions.DontRequireReceiver);
        } // :contentReference[oaicite:13]{index=13}
    }
}
