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
    public GameObject skill_prepare;//스킬 발사 효과 (대충 마법진 비슷한거 ㅎㅎ)
    public GameObject Bullet;//발사체


    GameObject player;

    public enum WaterBall_act { IDLE, MOVE_forward, MOVE_backward, ATTACK, HITTED, DIE };
    public WaterBall_act action = WaterBall_act.IDLE;
    private float attack_cool = 9.5f;//공격 속도 (초)
    private bool attack_able = true;//공격 가능 여부 (true : 가능, false : 쿨타임 중)

    private float ani_time;


    void Start()
    {
        player = GameObject.Find("Player");
        skill_prepare.SetActive(false);
    }


    public IEnumerator Attack()//공격 함수
    {
        StartCoroutine(count_AttackCool());//공격 쿨타임 함수
        action = WaterBall_act.ATTACK;//공격 상태로 전환
        animator.SetBool("Attack_prepare", true);//공격 애니메이션 재생
        Vector2 pos = (Vector2)(player.transform.position - gameObject.transform.position).normalized;//플레이어 방향 구하기
        skill_prepare.transform.position = new Vector3(gameObject.transform.position.x + pos.x, gameObject.transform.position.y + pos.y, -0.1f);//플레이어 방향으로 공격발사 효과 생성
        skill_prepare.SetActive(true);//공격 발사 효과 실행

        float cool = 2.3f;//발사효과 애니메이션 끝날 때 까지 기다리기
        int count = 0;//발사체 발사 개수

        while (true)
        {
            cool -= Time.deltaTime;

            if (cool <= 0)
            {
                GameObject bullet = Instantiate(Bullet, skill_prepare.transform.position, Quaternion.identity);//발사체 소환
                bullet.SetActive(true);//발사체 발사 (발사체에 날아가는 스크립트 내장되어있음)
                audio.enabled = false;
                audio.enabled = true;
                audio.clip = ballSound;

                count++;

                if (count == 5)//발사체를 전부 발사했을 대
                {
                    skill_prepare.SetActive(false);//발사 효과 비활성화
                    animator.SetBool("Attack_prepare", false);//애니메이션 중단
                    action = WaterBall_act.MOVE_forward;//앞으로 움직이는 상태
                    yield break;//코루틴 중단
                }

                cool = 0.2f;//아직 발사체 발사가 전부 끝나지 않았으면 다음 발사체 발사까지 시간을 두기 위함
            }

            yield return null;
        }
    }


    private void Update()
    {
        if (action != WaterBall_act.DIE)//죽은 상태가 아닐 대
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
            case WaterBall_act.MOVE_forward://플레이어를 향해 이동
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);
                    if (attack_able == true)//언제든지 공격준비가 되면 공격하기
                    {
                        StartCoroutine(Attack());
                    }
                    else if (dis < 2.5)//너무 가까워 졌을 때
                    {
                        action = WaterBall_act.MOVE_backward;//플레이어한테서 도망가기
                    }

                    if (player.transform.position.x < gameObject.transform.position.x)
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;

                    rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;
                    break;
                }
            case WaterBall_act.MOVE_backward://플레이어한테서 도망가기
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);
                    if (attack_able == true)//언제든지 공격준비가 되면 공격하기
                    {
                        StartCoroutine(Attack());
                    }
                    else if (dis >= 8)//너무 멀어졌을 때
                    {
                        action = WaterBall_act.MOVE_forward;//플레이어 따라 가기
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
                    rigid.velocity = Vector2.zero;//공격중일 때는 가만히
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
