using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick_Ver2 : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform background;  // ���̽�ƽ ���
    public RectTransform handle;      // ���̽�ƽ �ڵ�
    public float handleRange = 120f;    // �ڵ� ������ ����

    public Vector2 inputDirection;    // �Է� ����

    public Vector2 joystickDirection;
    public Vector2 joystickDirection_ForMove;

    private void Start()
    {
        handle.anchoredPosition = Vector2.zero;
        inputDirection = Vector2.zero;

        joystickDirection = Vector2.zero;
        joystickDirection_ForMove = Vector2.zero;
    }

    public Vector2 GetInputDirection()
    {
        return inputDirection;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = ClampJoystickPosition(eventData.position);
        handle.anchoredPosition = position - (background.anchoredPosition / 2);

        inputDirection = position / handleRange;
        inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

        joystickDirection = (position - (background.anchoredPosition / 2)).normalized;
        joystickDirection_ForMove = joystickDirection * 100f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;
        inputDirection = Vector2.zero;

        joystickDirection = Vector2.zero;
        joystickDirection_ForMove = Vector2.zero;
    }

    private Vector2 ClampJoystickPosition(Vector2 position)
    {
        Vector2 bgPosition = background.position;
        Vector2 direction = position - bgPosition;
        direction = Vector2.ClampMagnitude(direction, handleRange);
        return bgPosition + direction;
    }
}