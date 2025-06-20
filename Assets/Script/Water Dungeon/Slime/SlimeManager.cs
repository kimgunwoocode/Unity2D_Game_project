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
    private float attack_cool = 3.5f;//공격 속도 (초)
    private bool attack_able = true;//공격 가능 여부 (true : 가능, false : 쿨타임 중)

    //private float ani_time;

    [HideInInspector]public bool boom = true;//자폭 실행 여부

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (action != Slime_act.DIE)//죽은 상태가 아닐 때
        {
            if (enemydata.Enemy_Die)//몬스터 죽음 감지
            {
                Destroy(rigid);
                enemydata.enabled = false;//데이터 스크립트 끄기 (이미 죽었으니까)
                Boom_prepareArea.SetActive(true);//공격 모션 취소
                action = Slime_act.DIE;//현재 상태 죽음으로 설정
                Slime_ani.SetTrigger("Die");//애니메이션 실행

                Water_DropItem();//아이템 드롭하기
            }
            else if (enemydata.Enemy_Hitted)//몬스터가 맞았을 때
            {
                action = Slime_act.HITTED;//현재 상태 맞을으로 설정
                Slime_ani.SetTrigger("Hitted");//애니메이션 재생
                enemydata.Enemy_Hitted = false;//다시 공격받았을 때 감지하기위해 변수 초기화
            }
        }
    }

    void LateUpdate()
    {
        switch(action)
        {
            case Slime_act.IDLE://플레이어 발견 전 상태
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//플레이어와의 거리
                    if (dis <= 8f)//거리가 가까워졌을 때
                    {
                        action = Slime_act.JUMP;//이동하는 상태로 바뀜
                        Slime_ani.SetBool("Move", true);
                    }
                    break;
                }
            case Slime_act.JUMP://플레이어를 쫒아가는 상태
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//거리 구하기
                    if (dis <= 1.3f && attack_able == true)//거리가 가깝고 공격 가능한 상태일 때
                    {
                        action = Slime_act.ATTACK;//공격 중인 상태로 변환
                        Slime_ani.SetTrigger("Attack");//공격 애니메이션 실행

                        StartCoroutine("count_AttackCool");//공격 쿨타임 함수
                    }

                    if (player.transform.position.x < gameObject.transform.position.x)//플레이어 방향으로 y축 반전
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;

                    rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;//플레이어 방향으로 몬스터 이동
                    break;
                }
            case Slime_act.ATTACK:
                {
                    rigid.velocity = Vector2.zero;
                    break;
                }
            case Slime_act.HITTED://공격 받은 상태
                {
                    rigid.velocity = Vector2.zero;//멈춰있기, (넉백 구현 못함)
                    break;
                }
            case Slime_act.DIE://죽었을 때
                {
                    break;
                }
        }
    }

    public void Slime_attackArea()
    {
        Attack_area.SetActive(true);//공격 범위 오브젝트 활성화
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
        Destroy(gameObject);//오브젝트 파괴
    }



    IEnumerator count_AttackCool()//공격 쿨타임 함수
    {
        attack_able = false;//공격 가능 상태 비활성화
        yield return new WaitForSeconds(attack_cool);//공격 쿨타임 변수만큼 기다림
        attack_able = true;//공격 가능 상태 활성화
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
