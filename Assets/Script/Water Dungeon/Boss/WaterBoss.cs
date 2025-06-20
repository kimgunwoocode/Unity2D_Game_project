using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBoss : MonoBehaviour
{
    GameObject player;

    public Animator animator;
    public Rigidbody2D rigid;
    public CircleCollider2D collider_;
    public SpriteRenderer sprite;
    public WaterBoss_DungeonManager DungeonManager;
    public EnemyData enemydata;
    public Interaction_Text interactiontext;
    public GameObject clearDoor;

    public GameObject die_text;

    [Header("Skill Object")]
    public GameObject Attack_Hit_obj;//일반공격 
    public GameObject AllTrap_area;//스킬1 범위 표시
    public GameObject TrapAttack_Area;//스킬2 공격 범위 표시
    public GameObject Bullet;
    public GameObject slime;
    public GameObject[] Skill3_prepare = new GameObject[4];

    public enum WaterBoss_act {IDLE, MOVE, ATTACK, SKILL, HITTED, DIE};
    [Header("action")]
    public WaterBoss_act action = WaterBoss_act.IDLE;


    private float attack_cool = 4.1f;//공격 속도 (초)
    private bool attack_able = true;//공격 가능 여부 (true : 가능, false : 쿨타임 중)

    private float spondSlime_HPper = 0.9f;

    private float skill_cool = 5f;//스킬 쿨타임 (초)
    private bool skill_able = true;//스킬 가능 여부 (true : 가능, false : 쿨타임 중)
    public bool Skill2_Attack = false;//스킬2 공격 할 때 활성화



    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine("count_SkillCool");
        DungeonManager.StartCoroutine("repeat_TrapSetting");
        DungeonManager.StartCoroutine("Trap_effect_player_Update");
        DungeonManager.StartCoroutine("Trap_effect_Boss_Update");
    }

    void Update()
    {
        if (action != WaterBoss_act.DIE)//죽은 상태가 아닐 때
        {
            if (enemydata.Enemy_Die)//몬스터 죽음 감지
            {
                action = WaterBoss_act.DIE;
                enemydata.enabled = false;
                StopAllCoroutines();
                DungeonManager.StopCoroutine("repeat_TrapSetting");
                DungeonManager.StopCoroutine("Trap_effect_player_Update");
                DungeonManager.StopCoroutine("Trap_effect_Boss_Update");
                DungeonManager.delete_trapEffect();
                DungeonManager.Delete_Trap();
                animator.SetTrigger("Die");
                AllTrap_area.SetActive(false);
                TrapAttack_Area.SetActive(false);
                Skill3_prepare[0].SetActive(false);
                Skill3_prepare[1].SetActive(false);
                Skill3_prepare[2].SetActive(false);
                Skill3_prepare[3].SetActive(false);
                Destroy(rigid);
                collider_.isTrigger = true;
                gameObject.layer = 0;
                clearDoor.SetActive(true);//------------(임시)-------------

                interactiontext.enabled = true;
                interactiontext.die = true;
                die_text.SetActive(true);

                for (int i = 0; i < 10; i++)
                {
                    Water_DropItem();
                }

                if (PlayerData_Manager.dungeon_clear[0] == false)
                {
                    itemData.AddItem(13);
                    PlayerData_Manager.dungeon_clear[0] = true;
                }

                Destroy(this);
            }
            else if (enemydata.Enemy_Hitted)//몬스터가 맞았을 때
            {
                if (action == WaterBoss_act.IDLE)
                {
                    animator.SetTrigger("Hitted");
                    action = WaterBoss_act.HITTED;
                }

                enemydata.Enemy_Hitted = false;//다시 공격받았을 때 감지하기위해 변수 초기화
            }

            if (enemydata.Enemy_PresentHP <= enemydata.Enemy_MaxHP * spondSlime_HPper)
            {
                Instantiate(slime, gameObject.transform.position, Quaternion.identity);
                spondSlime_HPper -= 0.1f;
            }
        }
    }

    private void LateUpdate()
    {
        switch (action)
        {
            case WaterBoss_act.IDLE:
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);

                    if (skill_able == true)//스킬사용
                    {
                        action = WaterBoss_act.SKILL;
                        int skill_num = Random.Range(1, 6);
                        if (skill_num == 2 || skill_num == 3)
                        {
                            skill_num = 2;
                        }
                        else if (skill_num == 4 || skill_num == 5)
                        {
                            skill_num = 3;
                        }
                        switch(skill_num)
                        {
                            case 1:
                                {
                                    animator.SetTrigger("Skill1");
                                    StartCoroutine(Skill1_fun());
                                    break;
                                }
                            case 2:
                                {
                                    animator.SetTrigger("Skill2");
                                    Skill2_fun();
                                    break;
                                }
                            case 3:
                                {
                                    //animator.SetTrigger("Skill3");
                                    StartCoroutine(Skill3_fun());
                                    break;
                                }
                        }
                    }
                    else if (dis <= 1.6f && attack_able == true)//공격준비가 되면 공격하기
                    {
                        action = WaterBoss_act.ATTACK;
                        animator.SetTrigger("Attack");
                        StartCoroutine(count_AttackCool());
                    }


                    if (player.transform.position.x < gameObject.transform.position.x)
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;

                    rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;
                    break;
                }
            case WaterBoss_act.MOVE:
                {
                    break;
                }
            case WaterBoss_act.ATTACK:
                {
                    rigid.velocity = Vector2.zero;
                    break;
                }
            case WaterBoss_act.SKILL:
                {
                    rigid.velocity = Vector2.zero;
                    break;
                }
            case WaterBoss_act.HITTED:
                {
                    rigid.velocity = Vector2.zero;
                    break;
                }
            case WaterBoss_act.DIE:
                {
                    break;
                }
        }
    }

    public void action_IDLE()
    {
        action = WaterBoss_act.IDLE;
    }

    public void Attack_hit_true()
    {
        Attack_Hit_obj.SetActive(true);
    }
    public void Attacked_hit_false()
    {
        Attack_Hit_obj.SetActive(false);
    }

    IEnumerator Skill1_fun()
    {
        DungeonManager.StopCoroutine("repeat_TrapSetting");
        AllTrap_area.SetActive(true);
        skill_able = false;
        yield return new WaitForSeconds(5f);//스킬 지속 시간

        DungeonManager.StartCoroutine("repeat_TrapSetting");
        StartCoroutine(count_SkillCool());

        yield break;
    }
    public void Skill1_settrap()
    {
        AllTrap_area.SetActive(false);
        DungeonManager.allslow_Trap();
    }

    public void Skill2_fun()
    {
        DungeonManager.StopCoroutine("repeat_TrapSetting");

        for (int i = 0; i < 9; i++)
        {
            //print("소환 번호 : " + i);
            if (DungeonManager.trap_effect[i] == 2)
            {
                //print("소환 성공");
                switch(i)
                {
                    case 0:
                        Instantiate(TrapAttack_Area, new Vector3(-6, 6, -0.5f), Quaternion.identity).SetActive(true); break;
                    case 1:
                        Instantiate(TrapAttack_Area, new Vector3(0, 6, -0.5f), Quaternion.identity).SetActive(true); break;
                    case 2:
                        Instantiate(TrapAttack_Area, new Vector3(6, 6, -0.5f), Quaternion.identity).SetActive(true); break;
                    case 3:
                        Instantiate(TrapAttack_Area, new Vector3(-6, 0, -0.5f), Quaternion.identity).SetActive(true); break;
                    case 4:
                        Instantiate(TrapAttack_Area, new Vector3(0, 0, -0.5f), Quaternion.identity).SetActive(true); break;
                    case 5:
                        Instantiate(TrapAttack_Area, new Vector3(6, 0, -0.5f), Quaternion.identity).SetActive(true); break;
                    case 6:
                        Instantiate(TrapAttack_Area, new Vector3(-6, -6, -0.5f), Quaternion.identity).SetActive(true); break;
                    case 7:
                        Instantiate(TrapAttack_Area, new Vector3(0, -6, -0.5f), Quaternion.identity).SetActive(true); break;
                    case 8:
                        Instantiate(TrapAttack_Area, new Vector3(6, -6, -0.5f), Quaternion.identity).SetActive(true); break;
                }
            }
        }
    }
    public void Skill2_attack()
    {
        Skill2_Attack = true;
        StartCoroutine(count_SkillCool());
    }
    public void Skill2_settingtrap()
    {
        Skill2_Attack = false;
        DungeonManager.StartCoroutine("repeat_TrapSetting");
    }

    IEnumerator Skill3_fun()
    {
        for (int i = 0; i < 4; i++)
        {
            Skill3_prepare[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(1.7f);

        for (int l = 0; l < 3; l++)
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(Bullet, Skill3_prepare[i].transform.position, Quaternion.identity).SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1.1f);
        }

        for (int i = 0; i < 4; i++)
        {
            Skill3_prepare[i].SetActive(false);
        }

        action = WaterBoss_act.IDLE;
        StartCoroutine(count_SkillCool());

        yield break;
    }

    public void enable_this()
    {
        Destroy(this);
    }

    IEnumerator count_AttackCool()
    {
        attack_able = false;
        yield return new WaitForSeconds(attack_cool);
        attack_able = true;
        yield break;
    }

    IEnumerator count_SkillCool()
    {
        skill_able = false;
        yield return new WaitForSeconds(skill_cool);
        skill_able = true;
        yield break;
    }

    void Water_DropItem()
    {
        int type = Random.Range(1, 4);


        switch (type)
        {
            case 1:
                {
                    int num = 3;
                    itemData.AddItem(num);
                    break;
                }
            case 2:
                {
                    int num = 7;
                    itemData.AddItem(num);
                    break;
                }
            case 3:
                {
                    int num = 11;
                    itemData.AddItem(num);
                    break;
                }
        }
    }
}
