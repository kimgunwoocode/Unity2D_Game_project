using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHit : MonoBehaviour
{
    public EnemyData EnemyData;

    private void OnTriggerEnter2D(Collider2D collision)//�浹����
    {
        if (collision.gameObject.tag == "Player")//�浹�� ������Ʈ�� �÷��̾� �� ��
        {
            PlayerData_Manager.Player_PresentHP -= EnemyData.Enemy_Damage;//�÷��̾��� ü���� �ش� ������ ���ݷ� ��ŭ ��
            collision.gameObject.GetComponent<PlayerSpeed_Manager>().SpeedEffect_cool(1f, 0.7f);
            gameObject.SetActive(false);//���� ���� ������Ʈ ��Ȱ��ȭ (�� �� ���� ����)
        }
    }
}
