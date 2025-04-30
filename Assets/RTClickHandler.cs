using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RTClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Camera _rtCamera;           // RenderTexture ���f���Ă���J����
    [SerializeField] CinemachineVirtualCamera _vCam;
    [SerializeField] RawImage _rawImage;         // �N���b�N�Ώۂ� UI
    [SerializeField] InputManager _inputManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. Screen �� RawImage �̃��[�J�����W
        RectTransform rt = _rawImage.rectTransform;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rt,
            eventData.position,
            null, // Screen Space - Camera �Ȃ炻�̃J�����AOverlay�Ȃ� null
            out localPoint
        );

        // 2. ���[�J�����W �� UV (0�`1)
        Vector2 size = rt.rect.size;
        Vector2 uv = new Vector2(
            (localPoint.x / size.x) + 0.5f,
            (localPoint.y / size.y) + 0.5f
        );

        // 3. UV �� �r���[�|�[�g���W�ɕϊ����� Ray ����
        Ray ray = _rtCamera.ViewportPointToRay(uv);
        // 4. Raycast ���s
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
