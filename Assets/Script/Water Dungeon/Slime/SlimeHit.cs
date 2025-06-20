using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHit : MonoBehaviour
{
    public EnemyData EnemyData;

    private void OnTriggerEnter2D(Collider2D collision)//충돌감지
    {
        if (collision.gameObject.tag == "Player")//충돌한 오브젝트가 플레이어 일 때
        {
            PlayerData_Manager.Player_PresentHP -= EnemyData.Enemy_Damage;//플레이어의 체력을 해당 몬스터의 공격력 만큼 뺌
            collision.gameObject.GetComponent<PlayerSpeed_Manager>().SpeedEffect_cool(1f, 0.7f);
            gameObject.SetActive(false);//공격 범위 오브젝트 비활성화 (두 번 공격 방지)
        }
    }
}
