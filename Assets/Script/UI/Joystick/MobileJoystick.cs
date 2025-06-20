using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobileJoystick : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public GameObject joystic_obj;

    public RectTransform handle; // ���̽�ƽ �ڵ� �̹���
    public float joystickRange = 100f; // ���̽�ƽ �̵� ����
    public RectTransform handle_rect;
    public Image handle_Image;

    private Vector2 joystickStartPosition; // ���̽�ƽ ���� ��ġ
    public Vector2 joystickDirection; // ���̽�ƽ �̵� ����
    public Vector2 joystickDirection_last;//Ȱ ���� �� ������ ���� �� ȭ���� ���ư����� �ϱ� ����, ������ ���� �� ���� ���������� �ٶ� ������ ����
    public float joystickSpeed;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickStartPosition = eventData.position;
        handle_rect.sizeDelta = new Vector2(170, 170);
        handle_Image.color = new Color(0.8627451f, 0.8627451f, 0.8627451f, 0.6470588f);
        joystic_obj.transform.position = joystickStartPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        

        Vector2 touchPosition = eventData.position;
        joystickDirection = (touchPosition - joystickStartPosition).normalized;
        joystickDirection_last = joystickDirection;
        joystickSpeed = Vector2.Distance(touchPosition, joystickStartPosition);
        if (joystickSpeed <= joystickRange)
        {
            handle.anchoredPosition = touchPosition - joystickStartPosition;
        }
        else
        {
            handle.anchoredPosition = joystickDirection * joystickRange;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;
        joystickDirection = Vector2.zero;
        handle_rect.sizeDelta = new Vector2(150, 150);
        handle_Image.color = new Color(1f, 1f, 1f, 0.6470588f);
    }

    private void Awake()
    {
        //joystickStartPosition = /*gameObject.GetComponent<RectTransform>().anchoredPosition;*/new Vector2(220, 220);
        joystickDirection = Vector2.zero;
        
    }

    public GameObject player;

    void OnEnable()
    {
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        handle.anchoredPosition = Vector2.zero;
        joystickDirection = Vector2.zero;
        joystic_obj.transform.position = new Vector3(300, 250, 0);
    }

    void OnDisable()
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

