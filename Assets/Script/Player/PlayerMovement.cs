using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MobileJoystick joystick; // 모바일 조이스틱 스크립트
    //public Joystick_Ver2 joystick_ver2;
    public Rigidbody2D rb;
    private float length;
    private float limitLength;
    public float joystickLimitPercent = 5;//조이스틱이 움직여도 캐릭터는 움직이지 않는 범위(퍼센트100)
    private void Awake()
    {
        
    }

    /*
    private void FixedUpdate()
    {
        rb.velocity = joystick_ver2.joystickDirection_ForMove * PlayerData_Manager.moveSpeed * Time.deltaTime;
    }
    */

    
    private void FixedUpdate()
    {
        Vector2 direction = joystick.joystickDirection;
        length = joystick.joystickSpeed;
        limitLength = joystick.joystickRange;

        if (limitLength * joystickLimitPercent / 100 < length)
        {
            rb.velocity = direction * PlayerData_Manager.moveSpeed;
        }
        else
        {
            rb.velocity = 0 * direction;
        }
        
        
    }
    
}
