using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    public Animator Slime_ani;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;
    public EnemyData enemydata;
    public AudioSource audio;

    public GameObject Attack_area;
    public GameObject Boom_prepareArea;
    public GameObject Boom_area;

    public AudioClip attackAudio;

    GameObject player;

    public enum Slime_act { IDLE, JUMP, ATTACK, HITTED, DIE }
    public Slime_act action = Slime_act.IDLE;
    private float attack_cool = 3.5f;//���� �ӵ� (��)
    private bool attack_able = true;//���� ���� ���� (true : ����, false : ��Ÿ�� ��)

    //private float ani_time;

    [HideInInspector]public bool boom = true;//���� ���� ����

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (action != Slime_act.DIE)//���� ���°� �ƴ� ��
        {
            if (enemydata.Enemy_Die)//���� ���� ����
            {
                Destroy(rigid);
                enemydata.enabled = false;//������ ��ũ��Ʈ ���� (�̹� �׾����ϱ�)
                Boom_prepareArea.SetActive(true);//���� ��� ���
                action = Slime_act.DIE;//���� ���� �������� ����
                Slime_ani.SetTrigger("Die");//�ִϸ��̼� ����

                Water_DropItem();//������ ����ϱ�
            }
            else if (enemydata.Enemy_Hitted)//���Ͱ� �¾��� ��
            {
                action = Slime_act.HITTED;//���� ���� �������� ����
                Slime_ani.SetTrigger("Hitted");//�ִϸ��̼� ���
                enemydata.Enemy_Hitted = false;//�ٽ� ���ݹ޾��� �� �����ϱ����� ���� �ʱ�ȭ
            }
        }
    }

    void LateUpdate()
    {
        switch(action)
        {
            case Slime_act.IDLE://�÷��̾� �߰� �� ����
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//�÷��̾���� �Ÿ�
                    if (dis <= 8f)//�Ÿ��� ��������� ��
                    {
                        action = Slime_act.JUMP;//�̵��ϴ� ���·� �ٲ�
                        Slime_ani.SetBool("Move", true);
                    }
                    break;
                }
            case Slime_act.JUMP://�÷��̾ �i�ư��� ����
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//�Ÿ� ���ϱ�
                    if (dis <= 1.3f && attack_able == true)//�Ÿ��� ������ ���� ������ ������ ��
                    {
                        action = Slime_act.ATTACK;//���� ���� ���·� ��ȯ
                        Slime_ani.SetTrigger("Attack");//���� �ִϸ��̼� ����

                        StartCoroutine("count_AttackCool");//���� ��Ÿ�� �Լ�
                    }

                    if (player.transform.position.x < gameObject.transform.position.x)//�÷��̾� �������� y�� ����
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;

                    rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;//�÷��̾� �������� ���� �̵�
                    break;
                }
            case Slime_act.ATTACK:
                {
                    rigid.velocity = Vector2.zero;
                    break;
                }
            case Slime_act.HITTED://���� ���� ����
                {
                    rigid.velocity = Vector2.zero;//�����ֱ�, (�˹� ���� ����)
                    break;
                }
            case Slime_act.DIE://�׾��� ��
                {
                    break;
                }
        }
    }

    public void Slime_attackArea()
    {
        Attack_area.SetActive(true);//���� ���� ������Ʈ Ȱ��ȭ
    }

    public void Slime_attacked()
    {
        Attack_area.SetActive(false);
    }

    public void Slime_JUMP()
    {
        action = Slime_act.JUMP;
    }

    public void Slime_Boom()
    {
        boom = false;
        Boom_prepareArea.SetActive(false);
        Boom_area.SetActive(true);
    }

    public void gameObject_Destroy()
    {
        Destroy(gameObject);//������Ʈ �ı�
    }



    IEnumerator count_AttackCool()//���� ��Ÿ�� �Լ�
    {
        attack_able = false;//���� ���� ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(attack_cool);//���� ��Ÿ�� ������ŭ ��ٸ�
        attack_able = true;//���� ���� ���� Ȱ��ȭ
    }


    void Water_DropItem()
    {
        int type = Random.Range(1, 4);
        

        switch (type)
        {
            case 1:
                {
                    int num = 3;
                    if (PlayerData_Manager.Inventory.ContainsKey(num) || PlayerData_Manager.Inventory == null)
                    {
                        PlayerData_Manager.Inventory[num]++;
                    }
                    else
                    {
                        PlayerData_Manager.Inventory.Add(num, 1);
                    }
                    break;
                }
            case 2:
                {
                    int num = 7;
                    if (PlayerData_Manager.Inventory.ContainsKey(num) || PlayerData_Manager.Inventory == null)
                    {
                        PlayerData_Manager.Inventory[num]++;
                    }
                    else
                    {
                        PlayerData_Manager.Inventory.Add(num, 1);
                    }
                    break;
                }
            case 3:
                {
                    int num = 11;
                    if (PlayerData_Manager.Inventory.ContainsKey(num) || PlayerData_Manager.Inventory == null)
                    {
                        PlayerData_Manager.Inventory[num]++;
                    }
                    else
                    {
                        PlayerData_Manager.Inventory.Add(num, 1);
                    }
                    break;
                }
        }
    }

    void AttackSound()
    {
       audio.enabled = true;
       audio.clip = attackAudio;
    }

    void SoundOff()
    {
       audio.enabled = false;
    }
}
