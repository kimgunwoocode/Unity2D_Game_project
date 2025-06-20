using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button1Manager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //----------------------------------------------
    //public GameObject attack_test;
    //-----------------------------------테스트-------------------------------


    public static int Button1_Type = 1;//버튼 종류,  1 : 공격, 2 : 상호작용, 3 : 상호작용중 or 상호작용 시작
    public bool FullCharge = false;//화살 장전 완료 여부 판단

    public bool SwordAttackable = true;
    public bool BowAttackable = true;
    private bool bowattack_bug = false;
    //private Vector2 colseenemy;

    public ArrowJoystic arrowjoystick;
    public MobileJoystick joystick;
    public Transform attack_;

    [Header("Attack")]
    public Animator Sword_ani;

    public GameObject BowSlider_obj;
    public Slider Bow_slider;
    public Image BowSlider_image;
    public GameObject bow_parent;
    public GameObject arrow;


    [Header("GameObejct")]
    public GameObject player;
    //public MobileJoystick joystic;
    public GameObject Attack_Image;
    public GameObject Interaction_Image;
    public GameObject Arrow_target;

    private GameObject arrow_obj;

    public static bool arrow_charge = false;

    public GameObject interaction_icon;
    public void LateUpdate()
    {
        switch (Button1_Type)
        {
            case 1://공격 상태 일 때
                {
                    Attack_Image.SetActive(true);
                    Interaction_Image.SetActive(false);
                    interaction_icon.SetActive(false);
                    break;
                }
            case 2://상호작용 상태일 때
                {
                    Attack_Image.SetActive(false);
                    Interaction_Image.SetActive(true);
                    interaction_icon.SetActive(true);
                    break;
                }
        }
    }

    private void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(0.8627451f, 0.8627451f, 0.8627451f, 0.6470588f);

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
        yield break;
    }
    public IEnumerator arrow_joy()
    {
        arrow_charge = true;
        while (true)
        {
            float joystic_angle = Mathf.Atan2(joystick.joystickDirection.y, joystick.joystickDirection.x) * Mathf.Rad2Deg;
            float arrow_angle = Mathf.Atan2(arrowjoystick.Arrow_joystickDirection.y, arrowjoystick.Arrow_joystickDirection.x) * Mathf.Rad2Deg;

            if (attack_.localScale.x >= 0)
            {
                attack_.rotation = Quaternion.AngleAxis(arrow_angle + 180, Vector3.forward);
            }
            else
            {
                attack_.rotation = Quaternion.AngleAxis(arrow_angle, Vector3.forward);
            }
            yield return null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6470588f);

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
            arrow_obj.GetComponent<arrow_fire>().arrow_movetarget = arrowjoystick.Arrow_joystickDirection_last * 20f;
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
