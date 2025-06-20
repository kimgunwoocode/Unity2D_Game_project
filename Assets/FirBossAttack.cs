using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirBossAttack : MonoBehaviour
{
     EnemyData data;
    GameObject Boss;
    Rigidbody2D rigid;
    bool flip;
    float time=0;

    private void Awake()
    {
        Boss= GameObject.Find("FireBoss");
        data = Boss.GetComponent<EnemyData>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        flip = Boss.GetComponent<SpriteRenderer>().flipX;
        //true 哭率
        //flase 坷弗率

    }

    private void OnEnable()
    {
        Boss = GameObject.Find("FireBoss");
        data = Boss.GetComponent<EnemyData>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        flip = Boss.GetComponent<SpriteRenderer>().flipX;
        //true 哭率
        //flase 坷弗率
    }

    private void Update()
    {
        if (time >= 1.02)
        {
            if (flip)
            {
                gameObject.transform.position = Boss.transform.position + new Vector3(-0.4917f, -0.286f, 0);
            }
            else
            {
                gameObject.transform.position = Boss.transform.position + new Vector3(0.4917f, -0.286f, 0);
            }
        }
        
        
        time += Time.deltaTime;
        if(time >= 1.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&& (time >= 1.02))
        {
            PlayerData_Manager.Player_PresentHP -= data.Enemy_Damage;
            //collision.gameObject.GetComponent<PlayerSpeed_Manager>().SpeedEffect_cool(0.2f, 1f);
            Destroy(gameObject);
        }
        
    }
}
