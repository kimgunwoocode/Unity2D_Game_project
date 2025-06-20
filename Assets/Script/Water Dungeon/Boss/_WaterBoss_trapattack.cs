using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _WaterBoss_trapattack : MonoBehaviour
{
    public Animator animator_;
    public WaterBoss waterboss;
    public CircleCollider2D collider_;
    public EnemyData enemydata_;

    private bool attacked = false;

    void Update()
    {
        if (attacked == false && waterboss.Skill2_Attack == true)
        {
            attacked = true;
            animator_.SetTrigger("Boom");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData_Manager.Player_PresentHP -= Mathf.CeilToInt(enemydata_.Enemy_Damage * 1.5f);
            collision.gameObject.GetComponent<PlayerSpeed_Manager>().SpeedEffect_cool(2f, 0.7f);
            collider_.enabled = false;
        }
    }

    public void destroy_gameObject()
    {
        Destroy(gameObject);
    }
}
