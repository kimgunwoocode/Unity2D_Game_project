using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("Enemy 정보 설정")]
    public int Enemy_MaxHP;
    public float Enemy_Speed;
    public int Enemy_Damage;

    [Header("현재 enemy 정보 (인스펙터에서 설정 X)")]
    public int Enemy_PresentHP;

    private bool hitted_cool_is = false;

    public bool Enemy_Die = false;
    public bool Enemy_Hitted = false;

    private void Start()
    {
        Enemy_PresentHP = Enemy_MaxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack" && !hitted_cool_is)
        {
            Enemy_PresentHP -= PlayerData_Manager.PlayerDamage;
            hitted_cool_is = true;
            StartCoroutine(hitted_cool());

            if (Enemy_PresentHP <= 0 )
            {
                Enemy_Die = true;
            }
            else
            {
                Enemy_Hitted = true;
            }
        }
    }

    public IEnumerator hitted_cool()
    {
        yield return new WaitForSeconds(0.5f);
        hitted_cool_is = false;
        yield break;
    }
}
