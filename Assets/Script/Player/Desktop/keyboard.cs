using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class keyboard : MonoBehaviour
{
    public Rigidbody2D Player_Rigidbody;

    public Vector2 joystickDirection;

    void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        Vector2 GetVel = new Vector2(xMove, yMove) * PlayerData_Manager.moveSpeed;
        joystickDirection = GetVel;
        Player_Rigidbody.velocity = GetVel;
    }

    public GameObject interaction_icon;
    public void LateUpdate()
    {
        if (Button1_Type == 2)//상호작용 상태일 때
        {
            interaction_icon.SetActive(true);
        }
        else
        {
            interaction_icon.SetActive(false);
        }
    }

    public static int Button1_Type = 1;//버튼 종류,  1 : 공격, 2 : 상호작용, 3 : 상호작용중 or 상호작용 시작
    public bool FullCharge = false;//화살 장전 완료 여부 판단

    public bool SwordAttackable = true;
    public bool BowAttackable = true;
    private bool bowattack_bug = false;

    private bool mousedown_is = false;
    //private Vector2 colseenemy;

    public Transform attack_;

    [Header("Attack")]
    public Animator Sword_ani;

    public GameObject BowSlider_obj;
    public Slider Bow_slider;
    public Image BowSlider_image;
    public GameObject bow_parent;
    public GameObject arrow;
    public GameObject Arrow_target;


    private GameObject arrow_obj;

    public static bool arrow_charge = false;


    private void Start()
    {
        Camera_Object = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Camera_Object = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public GameObject Sword_Pobj;
    public GameObject Bow_Pobj;
    public OptionButton_Manager option_;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Button1_Type == 1)
            {
                switch (PlayerData_Manager.Player_WeaponType)
                {
                    case 1://근접 공격 일 때
                        {
                            if (SwordAttackable == true)
                            {
                                StartCoroutine(SwordAttack_cooltime());
                                Sword_ani.SetTrigger("SwordAttack");
                            }
                            break;
                        }
                    case 2://원거리 공격 일 때
                        {
                            if (BowAttackable == true)
                            {
                                print("arrow attack");
                                mousedown_is = true;

                                BowSlider_image.color = Color.white;
                                BowSlider_obj.SetActive(true);
                                Arrow_target.SetActive(true);
                                StartCoroutine("OnPointer_touch");

                                StartCoroutine("arrow_joy");
                            }
                            else if (BowAttackable == false)
                            {
                                bowattack_bug = true;
                            }
                            break;
                        }
                }
            }
            else if (Button1_Type == 2)
            {
                Button1_Type = 3;
            }
        }
        if (Input.GetMouseButtonUp(0) && mousedown_is == true)
        {
            stopCharging();
        }

        if (Input.GetKeyDown(KeyCode.Q) && arrow_charge == false)
        {
            switch (PlayerData_Manager.Player_WeaponType)//원거리 공격일 때는 근접으로, 근접일 때는 원거리로 무기 전환
            {
                case 1:
                    {
                        PlayerData_Manager.Player_WeaponType = 2;
                        Sword_Pobj.SetActive(false);
                        Bow_Pobj.SetActive(true);
                        break;
                    }
                case 2:
                    {
                        PlayerData_Manager.Player_WeaponType = 1;
                        Sword_Pobj.SetActive(true);
                        Bow_Pobj.SetActive(false);
                        break;
                    }
            }
        }


        if (Input.GetKeyDown(KeyCode.E) && arrow_charge == false)
        {
            option_.Open_InventoryScreen();
        }

    }



    public IEnumerator OnPointer_touch()
    {
        //차징 시작 시
        float time_ = 0;
        arrow_obj = Instantiate(arrow);
        //arrow_obj.transform.parent = bow_parent.transform;
        arrow_obj.transform.localScale = new Vector2(2.8f, 2.8f);
        arrow_obj.SetActive(true);

        //GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        while (true)
        {
            Bow_slider.value = time_ / PlayerData_Manager.Player_ArrowCharge;
            time_ += Time.deltaTime;
            if (time_ >= PlayerData_Manager.Player_ArrowCharge)
            {
                break;
            }

            yield return null;
        }
        FullCharge = true;
        BowSlider_image.color = Color.red;
    }

    Vector2 MousePosition;//마우스 위치
    Camera Camera_Object;//카메라 오브젝트
    private float ArrowAngle;
    void InputMousePoint()//마우스 포인트 입력받는 함수
    {
        Vector2 mousePoint = Input.mousePosition;
        MousePosition = Camera_Object.ScreenToWorldPoint(mousePoint);
    }
    public IEnumerator arrow_joy()
    {
        arrow_charge = true;
        while (true)
        {
            InputMousePoint();
            ArrowAngle = Mathf.Atan2(gameObject.transform.position.y - MousePosition.y, gameObject.transform.position.x - MousePosition.x) * Mathf.Rad2Deg;

            if (attack_.localScale.x >= 0)
            {
                attack_.rotation = Quaternion.AngleAxis(ArrowAngle, Vector3.forward);
            }
            else
            {
                attack_.rotation = Quaternion.AngleAxis(ArrowAngle + 180, Vector3.forward);
            }

            yield return null;
        }
    }

    public void stopCharging()//마우스 클릭 해제 시 호출
    {
        print("stop charging");
        mousedown_is = false;
        arrow_charge = false;

        if (bowattack_bug == true)
        {
            bowattack_bug = false;
            if (BowAttackable == true)
            {
                //아무것도 안함
            }
        }
        else if (PlayerData_Manager.Player_WeaponType == 2 && BowAttackable == true)//활 차징 완료 시
        {
            StartCoroutine("BowAttack_cooltime");
            StopCoroutine("OnPointer_touch");
            StopCoroutine("arrow_joy");
            BowSlider_obj.SetActive(false);

            if (FullCharge)
            {

            }
            else if (!FullCharge)
            {

            }

            Arrow_target.SetActive(false);

            arrow_obj.GetComponent<BoxCollider2D>().enabled = true;
            arrow_obj.GetComponent<arrow_fire>().arrow_movetarget = (MousePosition - (Vector2)gameObject.transform.position).normalized * 20f;
            arrow_obj.GetComponent<arrow_fire>().fire = true;

            arrow_obj = null;
        }
    }

    public IEnumerator SwordAttack_cooltime()
    {
        SwordAttackable = false;
        yield return new WaitForSeconds(PlayerData_Manager.PlayerAttack_cool);
        SwordAttackable = true;
        yield break;
    }

    public IEnumerator BowAttack_cooltime()
    {
        BowAttackable = false;
        yield return new WaitForSeconds(PlayerData_Manager.PlayerAttack_cool);
        BowAttackable = true;
        yield break;
    }

}
