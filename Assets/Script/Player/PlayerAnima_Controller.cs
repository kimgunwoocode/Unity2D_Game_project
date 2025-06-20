using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnima_Controller : MonoBehaviour
{
    public MobileJoystick joystick; // 모바일 조이스틱 스크립트
    //public Joystick_Ver2 joystick_ver2;

    public keyboard keyboard_;

    public int Player_Animation=1;// 이동 방향에 따라 걷는 모션이 바뀌도록 이동 방향 저장,  (멈춤 : 0, 위쪽 : 1, 아래쪽 : 2, 오른쪽 : 3, 왼쪽 : 4

    public Animator animator_body;
    public Animator animator_leg;

    public Transform attack_obj;
    public Transform weapon_tf;
    
    public bool walk = false;
    

    private void Start()
    {
        
    }
    private void Update()
    {
        if (keyboard_.joystickDirection == Vector2.zero)
        {
            walk = false;
            
        }
        else
        {
            walk = true;
            float angle = Mathf.Atan2(keyboard_.joystickDirection.y, keyboard_.joystickDirection.x) * Mathf.Rad2Deg;

            if (keyboard_.joystickDirection.y >= Mathf.Sin(45 * Mathf.Deg2Rad))
            {
                Player_Animation = 1;

                attack_obj.transform.localPosition = new Vector3(0, -0.3f, 0.2f);
            }
            else if (keyboard_.joystickDirection.y <= Mathf.Sin(-45 * Mathf.Deg2Rad))
            {
                Player_Animation = 2;

                attack_obj.transform.localPosition = new Vector3(0, -0.3f, -0.2f);
            }
            else
            {
                if (keyboard_.joystickDirection.x >= Mathf.Cos(45 * Mathf.Deg2Rad))
                {
                    Player_Animation = 3;
                    weapon_tf.localScale = new Vector3(-3.5f, 3.5f, 0.2f);
                }
                else if (keyboard_.joystickDirection.x <= Mathf.Cos(-45 * Mathf.Deg2Rad))
                {
                    Player_Animation = 4;
                    weapon_tf.localScale = new Vector3(3.5f, 3.5f, 1);
                }


                attack_obj.transform.localPosition = new Vector3(0, -0.3f, -0.2f);
            }

            if (Button1Manager.arrow_charge == false || keyboard.arrow_charge == false)
            {
                if (weapon_tf.localScale.x >= 0)
                {
                    attack_obj.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
                }
                else
                {
                    attack_obj.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }


        animator_body.SetInteger("animation", Player_Animation);
        animator_leg.SetBool("walk", walk);


    }
}
