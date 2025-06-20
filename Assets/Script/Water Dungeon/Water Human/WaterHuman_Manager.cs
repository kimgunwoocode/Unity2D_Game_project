using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SlimeManager;
using static WaterBoss;

public class WaterHuman_Manager : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;
    public EnemyData enemydata;
    public AudioSource audio;

    public AudioClip hide;
    public AudioClip attack;

    public GameObject HP_bar;

    [Header("Attack obj")]
    public GameObject Attack_Area;
    public GameObject Hide_SlowArea;//��ų ��� �� �ش� �� �ֺ� �÷��̾�� ���������ϱ� ���� ����

    GameObject player;

    public enum WaterHuman_act { IDLE, MOVE, ATTACK, HIDE, HITTED, DIE };
    [Header("action")]
    public WaterHuman_act action = WaterHuman_act.IDLE;


    float attack_cool = 4.1f;
    bool attack_able = true;

    float Hide_cool = 15.6f;
    bool Hide_able = true;
    bool Hiding = false;


    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (action != WaterHuman_act.DIE)//���� ���°� �ƴ� ��
        {
            if (enemydata.Enemy_Die)//���� ���� ����
            {
                rigid.velocity = Vector2.zero;
                enemydata.enabled = false;//������ ��ũ��Ʈ ���� (�̹� �׾����ϱ�)
                Hide_SlowArea.SetActive(false);
                Attack_Area.SetActive(false);
                action = WaterHuman_act.DIE;
                animator.SetTrigger("Die");
                Destroy(gameObject.GetComponent<Rigidbody2D>());

                Water_DropItem();
                Water_DropItem();
            }
            else if (enemydata.Enemy_Hitted)//���Ͱ� �¾��� ��
            {
                Hiding = false;//��ų �� �ߵ� ����
                Hide_SlowArea.SetActive(false);
                Attack_Area.SetActive(false);
                enemydata.Enemy_Hitted = false;//�ٽ� ���ݹ޾��� �� �����ϱ����� ���� �ʱ�ȭ
                animator.SetTrigger("Hitted");
                animator.SetBool("Hide", false);
            }
        }
    }

    private void LateUpdate()
    {
        switch (action)
        {
            case WaterHuman_act.IDLE:
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);
                    if (dis < 10f)
                    {
                        action = WaterHuman_act.MOVE;
                        animator.SetTrigger("Move");
                    }
                    break;
                }
            case WaterHuman_act.MOVE:
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);

                    if (Hide_able == true)//��ų���
                    {
                        Hiding = true;
                        action = WaterHuman_act.HIDE;
                        animator.SetTrigger("Hide");
                        StartCoroutine("count_HideSkillCool");
                    }
                    else if (dis <= 1.1f && attack_able == true)//�����غ� �Ǹ� �����ϱ�
                    {
                        action = WaterHuman_act.ATTACK;
                        animator.SetTrigger("Attack");
                        StartCoroutine("count_AttackCool");
                    }


                    if (player.transform.position.x < gameObject.transform.position.x)
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;

                    rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;
                    break;
                }
            case WaterHuman_act.ATTACK:
                {
                    break;
                }
            case WaterHuman_act.HIDE:
                {
                    if (Hiding == false)
                    {
                        action = WaterHuman_act.IDLE;
                    }

                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);

                    if (player.transform.position.x < gameObject.transform.position.x)
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;

                    rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed * 0.3f;
                    break;
                }
            case WaterHuman_act.HITTED:
                {
                    rigid.velocity = Vector2.zero;
                    break;
                }
            case WaterHuman_act.DIE:
                {
                    break;
                }
        }
    }

    IEnumerator count_AttackCool()//���� ��Ÿ�� �Լ�
    {
        attack_able = false;//���� ���� ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(attack_cool);//���� ��Ÿ�� ������ŭ ��ٸ�
        attack_able = true;//���� ���� ���� Ȱ��ȭ
    }
    IEnumerator count_HideSkillCool()//��ų ��Ÿ�� �Լ�
    {
        Hide_able = false;//��ų��� ���� ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(Hide_cool);//��ų ��Ÿ�� ������ŭ ��ٸ�
        Hide_able = true;//��ų��� ���� ���� Ȱ��ȭ
    }


    public void hiding_true()
    {
        Hiding = true;
        Hide_SlowArea.SetActive(true);
        HP_bar.SetActive(false);
    }
    public void hiding_false()
    {
        Hiding = false;
        Hide_SlowArea.SetActive(false);
        HP_bar.SetActive(true);
    }

    public void action_IDEL()
    {
        action = WaterHuman_act.HIDE;
    }
    public void attackArea_true()
    {
        rigid.velocity = Vector2.zero;
        Attack_Area.SetActive(true);
    }
    public void attackArea_false()
    {
        Attack_Area.SetActive(false);
    }
    public void destroy_gameObject()
    {
        Destroy(gameObject);
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

    void HideSound()
    {
        //audio.enabled = false;
        //audio.enabled = true;
        //audio.clip = hide;
    }

    void AttackSound()
    {
        audio.enabled = false;
        audio.enabled = true;
        audio.clip = attack;
    }

    void SoundOff()
    {
       audio.enabled = false;
    }
}
