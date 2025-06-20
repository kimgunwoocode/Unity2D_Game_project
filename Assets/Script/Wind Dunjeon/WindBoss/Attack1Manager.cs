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
            PlayerData_Manager.Player_PresentHP -= 8;//windBoss�� ��������ŭ ü�� ����(�˼����� ������ ���ڷ� ��ü)
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
