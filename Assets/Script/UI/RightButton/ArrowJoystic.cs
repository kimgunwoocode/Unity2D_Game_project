using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowJoystic : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{

    private Vector2 Arrow_joystickStartPosition; // 조이스틱 시작 위치
    public Vector2 Arrow_joystickDirection; // 조이스틱 이동 방향
    public Vector2 Arrow_joystickDirection_last;//활 조준 시 가만히 있을 때 화살이 날아가도록 하기 위함, 가만히 있을 때 가장 마지막으로 바라본 방향을 저장

    public float joystickRange = 100f; // 조이스틱 이동 범위
    public float joystickSpeed;

    public RectTransform handle; // 조이스틱 핸들 이미지

    public void OnPointerDown(PointerEventData eventData)
    {
        Arrow_joystickStartPosition = eventData.position;//활 조이스틱
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition = eventData.position;
        Arrow_joystickDirection = (touchPosition - Arrow_joystickStartPosition).normalized;
        Arrow_joystickDirection_last = Arrow_joystickDirection;
        joystickSpeed = Vector2.Distance(touchPosition, Arrow_joystickStartPosition);
        if (joystickSpeed <= joystickRange)
        {
            handle.anchoredPosition = touchPosition - Arrow_joystickStartPosition;
        }
        else
        {
            handle.anchoredPosition = Arrow_joystickDirection * joystickRange;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;
        Arrow_joystickDirection = Vector2.zero;

        handle.anchoredPosition = Vector2.zero;
    }

    private void Awake()
    {
        Arrow_joystickDirection = Vector2.zero;

    }
}
