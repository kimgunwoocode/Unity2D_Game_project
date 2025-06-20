using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall_Manager : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;
    public EnemyData enemydata;
    public AudioSource audio;

    public AudioClip ballSound;
    public AudioClip dieSound;

    [Header("Skill GameObject")]
    public GameObject skill_prepare;//��ų �߻� ȿ�� (���� ������ ����Ѱ� ����)
    public GameObject Bullet;//�߻�ü


    GameObject player;

    public enum WaterBall_act { IDLE, MOVE_forward, MOVE_backward, ATTACK, HITTED, DIE };
    public WaterBall_act action = WaterBall_act.IDLE;
    private float attack_cool = 9.5f;//���� �ӵ� (��)
    private bool attack_able = true;//���� ���� ���� (true : ����, false : ��Ÿ�� ��)

    private float ani_time;


    void Start()
    {
        player = GameObject.Find("Player");
        skill_prepare.SetActive(false);
    }


    public IEnumerator Attack()//���� �Լ�
    {
        StartCoroutine(count_AttackCool());//���� ��Ÿ�� �Լ�
        action = WaterBall_act.ATTACK;//���� ���·� ��ȯ
        animator.SetBool("Attack_prepare", true);//���� �ִϸ��̼� ���
        Vector2 pos = (Vector2)(player.transform.position - gameObject.transform.position).normalized;//�÷��̾� ���� ���ϱ�
        skill_prepare.transform.position = new Vector3(gameObject.transform.position.x + pos.x, gameObject.transform.position.y + pos.y, -0.1f);//�÷��̾� �������� ���ݹ߻� ȿ�� ����
        skill_prepare.SetActive(true);//���� �߻� ȿ�� ����

        float cool = 2.3f;//�߻�ȿ�� �ִϸ��̼� ���� �� ���� ��ٸ���
        int count = 0;//�߻�ü �߻� ����

        while (true)
        {
            cool -= Time.deltaTime;

            if (cool <= 0)
            {
                GameObject bullet = Instantiate(Bullet, skill_prepare.transform.position, Quaternion.identity);//�߻�ü ��ȯ
                bullet.SetActive(true);//�߻�ü �߻� (�߻�ü�� ���ư��� ��ũ��Ʈ ����Ǿ�����)
                audio.enabled = false;
                audio.enabled = true;
                audio.clip = ballSound;

                count++;

                if (count == 5)//�߻�ü�� ���� �߻����� ��
                {
                    skill_prepare.SetActive(false);//�߻� ȿ�� ��Ȱ��ȭ
                    animator.SetBool("Attack_prepare", false);//�ִϸ��̼� �ߴ�
                    action = WaterBall_act.MOVE_forward;//������ �����̴� ����
                    yield break;//�ڷ�ƾ �ߴ�
                }

                cool = 0.2f;//���� �߻�ü �߻簡 ���� ������ �ʾ����� ���� �߻�ü �߻���� �ð��� �α� ����
            }

            yield return null;
        }
    }


    private void Update()
    {
        if (action != WaterBall_act.DIE)//���� ���°� �ƴ� ��
        {
            if (enemydata.Enemy_Die)
            {
                enemydata.enabled = false;
                StopCoroutine("Attack");
                //audio.enabled = false;
                skill_prepare.SetActive(false);
                action = WaterBall_act.DIE;
                ani_time = 1.3f;
                animator.SetTrigger("Die");
                //audio.enabled = true;
                //audio.clip = dieSound;

                Water_DropItem();
                Water_DropItem();
                Water_DropItem();
            }
            else if (enemydata.Enemy_Hitted)
            {
                action = WaterBall_act.HITTED;
                ani_time = 1f;
                animator.SetTrigger("Hitted");
                enemydata.Enemy_Hitted = false;
            }
        }
    }

    void LateUpdate()
    {
        switch (action)
        {
            case WaterBall_act.IDLE:
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);
                    if (dis <= 10f)
                    {
                        action = WaterBall_act.MOVE_forward;
                    }
                    break;
                }
            case WaterBall_act.MOVE_forward://�÷��̾ ���� �̵�
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);
                    if (attack_able == true)//�������� �����غ� �Ǹ� �����ϱ�
                    {
                        StartCoroutine(Attack());
                    }
                    else if (dis < 2.5)//�ʹ� ����� ���� ��
                    {
                        action = WaterBall_act.MOVE_backward;//�÷��̾����׼� ��������
                    }

                    if (player.transform.position.x < gameObject.transform.position.x)
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;

                    rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;
                    break;
                }
            case WaterBall_act.MOVE_backward://�÷��̾����׼� ��������
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);
                    if (attack_able == true)//�������� �����غ� �Ǹ� �����ϱ�
                    {
                        StartCoroutine(Attack());
                    }
                    else if (dis >= 8)//�ʹ� �־����� ��
                    {
                        action = WaterBall_act.MOVE_forward;//�÷��̾� ���� ����
                    }

                    if (player.transform.position.x > gameObject.transform.position.x)
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;

                    rigid.velocity = (-1) * ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;
                    break;
                }

            case WaterBall_act.ATTACK:
                {
                    rigid.velocity = Vector2.zero;//�������� ���� ������
                    break;
                }

            case WaterBall_act.HITTED:
                {
                    rigid.velocity = Vector2.zero;
                    ani_time -= Time.deltaTime;
                    if (ani_time <= 0)
                    {
                        action = WaterBall_act.IDLE;
                    }
                    break;
                }

            case WaterBall_act.DIE:
                {
                    rigid.velocity = Vector2.zero;
                    ani_time -= Time.deltaTime;
                    if (ani_time <= 0)
                    {
                        Destroy(gameObject);
                    }
                    break;
                }
        }
    }

    IEnumerator count_AttackCool()
    {
        attack_able = false;
        yield return new WaitForSeconds(attack_cool);
        attack_able = true;
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
}
