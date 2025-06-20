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

    bool stop;//���� �߿��� �������� �ʰ� �ϱ� ���� ��ġ

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
        if (enemydata.Enemy_Hitted == true)//�÷��̾�� �ǰݵǸ� ����
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

            rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;//�÷��̾� �������� ���� �̵�

        }
        else
        {
            wind_bomb_ani.SetBool("isMove", false);
            rigid.velocity = Vector2.zero;

        }

        if (enemydata.Enemy_Die == true)//���� �� Die��ɾ�
        {
            stop = true;
            wind_bomb_ani.SetTrigger("isDie");
        }
    }

    void Boom()//����
    {
        stop = true;
        AttackArea.SetActive(true);
        wind_bomb_ani.SetTrigger("isTimer");
    }

    public void BoomDamage()//������ ��, �ִϸ��̼� �̺�Ʈ�� ����
    {
        if(isInArea == true)
        {
            PlayerData_Manager.Player_PresentHP -= enemydata.Enemy_Damage;
        }
    }

    void Die()//�ִϸ��̼� �̺�Ʈ�� ����
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
        if (collision.gameObject.tag == "Player")//�÷��̾ ����� �� ���� ��ɾ� ����
        {
            Boom();
        }
    }

    void AreaOff()//������ ����� ���� ����
    {
        AttackArea.SetActive(false);
    }

    void BoomSound()
    {
        audio.enabled = true;
    }
}
