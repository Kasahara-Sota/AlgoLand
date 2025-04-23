using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RTClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Camera rtCamera;           // RenderTexture ���f���Ă���J����
    public RawImage rawImage;         // �N���b�N�Ώۂ� UI

    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. Screen �� RawImage �̃��[�J�����W
        RectTransform rt = rawImage.rectTransform;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rt,
            eventData.position,
            null, // Screen Space - Camera �Ȃ炻�̃J�����AOverlay�Ȃ� null
            out localPoint
        ); // :contentReference[oaicite:10]{index=10}

        // 2. ���[�J�����W �� UV (0�`1)
        Vector2 size = rt.rect.size;
        Vector2 uv = new Vector2(
            (localPoint.x / size.x) + 0.5f,
            (localPoint.y / size.y) + 0.5f
        ); // :contentReference[oaicite:11]{index=11}

        // 3. UV �� �r���[�|�[�g���W�ɕϊ����� Ray ����
        Ray ray = rtCamera.ViewportPointToRay(uv); // :contentReference[oaicite:12]{index=12}
        // 4. Raycast ���s
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var obj = hit.collider.gameObject;
            // ��F�ΏۃI�u�W�F�N�g�̏��擾
            Debug.Log($"Hit: {obj.name}");
            // ���\�b�h�Ăяo��
            //obj.SendMessage("OnClicked", SendMessageOptions.DontRequireReceiver);
        } // :contentReference[oaicite:13]{index=13}
    }
}
