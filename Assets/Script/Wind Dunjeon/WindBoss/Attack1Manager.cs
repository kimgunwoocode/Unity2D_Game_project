using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Manager : MonoBehaviour
{
    WindBoss_Manager windBoss;
    public Rigidbody2D rigid;
    public Animator Attack1Ani;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerData_Manager.Player_PresentHP -= 8;//windBoss의 데미지만큼 체력 감소(알수없는 오류로 숫자로 대체)
            Debug.Log("attack1Damage");
        }
        else if(collision.name == "Wall")
        {
            Attack1Ani.SetTrigger("isDestroy");
        }
    }

    void Des()
    {
        Destroy(gameObject);
    }
}
