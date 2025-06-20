using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoom : MonoBehaviour
{
    public Animator animator;
    public CircleCollider2D Collider;
    public EnemyData enemyData;
    public AudioSource audio;

    private float cool = 0.4f;
    private bool damaged = false;

    private void Update()
    {
        cool -= Time.deltaTime;

        if (cool <= 0 )
        {
            Collider.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && damaged == false)
        {
            PlayerData_Manager.Player_PresentHP -= enemyData.Enemy_Damage * 2;
            collision.gameObject.GetComponent<PlayerSpeed_Manager>().SpeedEffect_cool(1.5f, 0.5f);
            damaged = true;
        }
    }

    void BoomSound()
    {
        //audio.enabled = true;
    }
}
