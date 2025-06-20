using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBomb_Manager : MonoBehaviour
{
    public Animator wind_bomb_ani;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;
    public EnemyData enemydata;
    public AudioSource audio;

    public GameObject AttackArea;
    WindBomb_Boom boom;

    GameObject player;
    public GameObject sponer;

    public bool isInArea = false;

    bool stop;//자폭 중에는 움직이지 않게 하기 위한 장치

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigid = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sponer = GameObject.Find("SponeEnemy");

        AttackArea.SetActive(false);

        stop = false;
    }

    void LateUpdate()
    {
        if (enemydata.Enemy_Hitted == true)//플레이어게 피격되면 자폭
        {
            wind_bomb_ani.SetTrigger("isHitted");

            Invoke("Boom", 0.5f);
        }
    }

    void Update()
    {
        if (stop == false)
        {
            wind_bomb_ani.SetBool("isMove", true);

            if(player.transform.position.x > gameObject.transform.position.x)
            {
                sprite.flipX = true;
            }
            else if(player.transform.position.x < gameObject.transform.position.x)
            {
                sprite.flipX = false;
            }

            rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;//플레이어 방향으로 몬스터 이동

        }
        else
        {
            wind_bomb_ani.SetBool("isMove", false);
            rigid.velocity = Vector2.zero;

        }

        if (enemydata.Enemy_Die == true)//죽을 시 Die명령어
        {
            stop = true;
            wind_bomb_ani.SetTrigger("isDie");
        }
    }

    void Boom()//자폭
    {
        stop = true;
        AttackArea.SetActive(true);
        wind_bomb_ani.SetTrigger("isTimer");
    }

    public void BoomDamage()//데미지 줌, 애니매이션 이벤트로 실행
    {
        if(isInArea == true)
        {
            PlayerData_Manager.Player_PresentHP -= enemydata.Enemy_Damage;
        }
    }

    void Die()//애니매이션 이벤트로 실행
    {
        Wind_DropItem();
        Destroy(gameObject);
        sponer.GetComponent<SponerManager>().deadEnemy += 1;
    }

    void Wind_DropItem()
    {
        int type = Random.Range(1, 4);


        switch (type)
        {
            case 1:
                {
                    int num = 1;
                    itemData.AddItem(num);
                    break;
                }
            case 2:
                {
                    int num = 5;
                    itemData.AddItem(num);
                    break;
                }
            case 3:
                {
                    int num = 9;
                    itemData.AddItem(num);
                    break;
                }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//플레이어에 닿았을 때 자폭 명령어 전달
        {
            Boom();
        }
    }

    void AreaOff()//자폭중 사망시 범위 끄기
    {
        AttackArea.SetActive(false);
    }

    void BoomSound()
    {
        audio.enabled = true;
    }
}
