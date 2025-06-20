using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHuman_Attack : MonoBehaviour
{
    public EnemyData enemyData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//�浹�� ������Ʈ�� �÷��̾� �� ��
        {
            PlayerData_Manager.Player_PresentHP -= enemyData.Enemy_Damage;//�÷��̾��� ü���� �ش� ������ ���ݷ� ��ŭ ��
            collision.gameObject.GetComponent<PlayerSpeed_Manager>().SpeedEffect_cool(1f, 0.7f);
            gameObject.SetActive(false);//���� ���� ������Ʈ ��Ȱ��ȭ (�� �� ���� ����)
        }
    }
}
