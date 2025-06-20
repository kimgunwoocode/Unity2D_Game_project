using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobileJoystick : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public GameObject joystic_obj;

    public RectTransform handle; // 조이스틱 핸들 이미지
    public float joystickRange = 100f; // 조이스틱 이동 범위
    public RectTransform handle_rect;
    public Image handle_Image;

    private Vector2 joystickStartPosition; // 조이스틱 시작 위치
    public Vector2 joystickDirection; // 조이스틱 이동 방향
    public Vector2 joystickDirection_last;//활 조준 시 가만히 있을 때 화살이 날아가도록 하기 위함, 가만히 있을 때 가장 마지막으로 바라본 방향을 저장
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
        // 델리게이트 체인 추가
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
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

