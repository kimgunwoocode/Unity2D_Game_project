using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowJoystic : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{

    private Vector2 Arrow_joystickStartPosition; // ���̽�ƽ ���� ��ġ
    public Vector2 Arrow_joystickDirection; // ���̽�ƽ �̵� ����
    public Vector2 Arrow_joystickDirection_last;//Ȱ ���� �� ������ ���� �� ȭ���� ���ư����� �ϱ� ����, ������ ���� �� ���� ���������� �ٶ� ������ ����

    public float joystickRange = 100f; // ���̽�ƽ �̵� ����
    public float joystickSpeed;

    public RectTransform handle; // ���̽�ƽ �ڵ� �̹���

    public void OnPointerDown(PointerEventData eventData)
    {
        Arrow_joystickStartPosition = eventData.position;//Ȱ ���̽�ƽ
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
