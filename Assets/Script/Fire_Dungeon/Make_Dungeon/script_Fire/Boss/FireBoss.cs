using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : MonoBehaviour
{

    // rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemyData.Enemy_Speed;
    //public CapsuleCollider2D collider_;
    //animator.SetTrigger("Die");
    GameObject player;
    public Animator animator;
    public Rigidbody2D rigid;

    public RoomManager roomManager;
    public FireDungeon_Manager DungeonManager;
    public EnemyData enemydata;

    public SpriteRenderer sprite;
    public Interaction_Text interactiontext;
    public GameObject clearDoor;
    public GameObject die_text;

    public GameObject middleBoss;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject earthQuake;
    public GameObject AttackObj;
    public GameObject[] Skill3_prepare = new GameObject[4];

    MapGenerator mapGenerator;

    public enum FireBoss_action {breathe,attack,move,die,skill };
    public enum Step { step1, step2, step3,step4};
    FireBoss_action BossAction = FireBoss_action.breathe;
    Step BossStep = Step.step1;
    public const float attack_cool = 4f;//공격 속도 (초)
    public const float skill_cool = 10f;//스킬 쿨타임 (초)

    public float AttackGround_length = 0.9f;//공격 애니메이션 지속시간
    public float Attacksord_length = 1.571f;//공격 애니메이션 지속시간
    public float Dead_length = 2.333f;//공격 애니메이션 지속시간

    float time = 0;
    float time2 = 0;
    float timeCoru = 0;
    float needTime = 0;
    bool isEndSkill = true;//스킬이 다 끝나고 다음스킬이 이어져야 하므로!
    bool move = true;//ㅇ보스 움직임 제어
    bool firstupTimeTo2 = true;
    bool firstupTimeTo3 = true;
    bool firstupTimeTo4 = true;

    bool BossDie = false;
    SpriteRenderer thisColor;

    private void sda()
    {
        //죽었을 때
        /*
         action = WaterBoss_act.DIE;
                    enemydata.enabled = false;
               

                    animator.SetTrigger("Die");
                    
                   
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


                if (enemydata.Enemy_Die)//몬스터 죽음 감지
                {

                }
                else if (enemydata.Enemy_Hitted)//몬스터가 맞았을 때
                {
                    if (action == WaterBoss_act.IDLE)
                    {
                        animator.SetTrigger("Hitted");
                        action = WaterBoss_act.HITTED;
                    }


                }

                if (enemydata.Enemy_PresentHP <= enemydata.Enemy_MaxHP * spondSlime_HPper)
                {
                    Instantiate(slime, gameObject.transform.position, Quaternion.identity);
                    spondSlime_HPper -= 0.1f;
                }
          */
    }
    int mapSizeX;
    int mapSizeY;

    private void Start()
    {
        //------------------------------------------------------------------------------------Test)
        PlayerData_Manager.PlayerDamage += 30;
        PlayerData_Manager.Player_MaxHP += 200;


        mapGenerator = GameObject.Find("MapGenerator").GetComponent<MapGenerator>();
        mapSizeX = mapGenerator.mapSizeCopy.x;
        mapSizeY = mapGenerator.mapSizeCopy.y;

        player = GameObject.Find("Player");
        thisColor = gameObject.GetComponent<SpriteRenderer>();
        
    }

    private void LateUpdate()
    {
        //몬스터 죽음 감지
        if (enemydata.Enemy_Die&&!BossDie)//몬스터 죽음 감지
        {
            
            BossAction = FireBoss_action.die;
        }

        time2 += Time.deltaTime;
        if (isEndSkill)
        {//스킬 다 끝났을 때 만 시간재기
            time += Time.deltaTime;

        }

        //스텝 나눈기
        {
            if (enemydata.Enemy_PresentHP < enemydata.Enemy_MaxHP * 1 / 4)
            {
                BossStep = Step.step4;
            }
            else if (enemydata.Enemy_PresentHP < enemydata.Enemy_MaxHP * 2 / 4)
            {
                BossStep = Step.step3;
            }
            else if (enemydata.Enemy_PresentHP < enemydata.Enemy_MaxHP * 3 / 4)
            {
                BossStep = Step.step2;
            }
            else
            {
                BossStep = Step.step1;
            }
        }
        //스텝에 따른 능력치 변화
        switch (BossStep)
        {
            case Step.step1:
                
                break;

            case Step.step2:
                if (firstupTimeTo2)
                {
                    Debug.Log("---------------STEP2");
                    thisColor.color = new Color(255 / 255f, 211 / 255f, 211 / 255f);
                    enemydata.Enemy_Speed += 1;
                    animator.speed += 0.3f;
                    enemydata.Enemy_Damage += 10;
                    firstupTimeTo2 = false;
                    //skill_cool -= 0.5f;
                }
                break;

            case Step.step3:
                if (firstupTimeTo3)
                {
                    Debug.Log("---------------STEP3");
                    thisColor.color = new Color(255 / 255f, 163 / 255f, 163 / 255f);
                    enemydata.Enemy_Speed += 0.3f;
                    animator.speed += 0.3f;
                    enemydata.Enemy_Damage += 10;
                    firstupTimeTo3 = false;
                    //skill_cool -= 0.5f;
                }
                break;

            case Step.step4:
                if (firstupTimeTo4)
                {
                    Debug.Log("---------------STEP4");
                    thisColor.color = new Color(255 / 255f, 114 / 255f, 114 / 255f);
                    animator.speed += 0.4f;
                    AttackGround_length = AttackGround_length / 2;
                    Attacksord_length = Attacksord_length / 2;
                    enemydata.Enemy_Damage += 10;
                    firstupTimeTo4 = false;
                    // skill_cool -= 1f;
                }
                break;
        }

        //보스 상태에 따른 
        switch (BossAction)
        {
            case FireBoss_action.breathe:
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);
                    if (dis <= 16f)
                    {
                        BossAction = FireBoss_action.move;
                    }
                    break;
                }
            case FireBoss_action.move://플레이어를 쫒아가는 상태
                {
                    float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//거리 구하기
                    if (dis <= 4f && time2 >= attack_cool)//거리가 가깝고 공격 가능한 상태일 때
                    {
                        Attack();
                        time2 = 0;

                    }

                    if(dis > 4f)
                    {
                        //뒤집기
                        {
                            if (player.transform.position.x < gameObject.transform.position.x)//플레이어 방향으로 y축 반전
                                sprite.flipX = true;
                            else
                                sprite.flipX = false;
                        }
                    }


                    if (move)
                    {
                        rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;//플레이어 방향으로 몬스터 이동
                    }

                    if (time >= skill_cool && isEndSkill)
                    {
                        Debug.Log(time);
                        BossAction = FireBoss_action.skill;
                        isEndSkill = false;
                    }

                        break;
                }

            case FireBoss_action.die://죽었을 때
                {
                    if (!BossDie)
                    {
                        enemydata.enabled = false;
                        animator.SetTrigger("Die");
                        // SkillCool(Dead_length);
                        Destroy(rigid);
                        gameObject.layer = 0;
                        clearDoor.SetActive(true);

                        interactiontext.enabled = true;
                        interactiontext.die = true;
                        die_text.SetActive(true);

                        for (int i = 0; i < 10; i++)
                        {
                            Fire_DropItem();
                        }

                        if (PlayerData_Manager.dungeon_clear[2] == false)
                        {
                            itemData.AddItem(13);
                            PlayerData_Manager.dungeon_clear[2] = true;
                        }

                        Destroy(this);
                        
                        BossDie = true;
                    }
                    break;
                }
            case FireBoss_action.skill:
                {

                    if (!isEndSkill)
                    {
                        move = true; // move 초기화
                        switch (BossStep)
                        {
                            case Step.step1:
                                switch (Random.Range(0, 2))
                                {
                                    case 0:
                                        UseGroundSkill(20);
                                        break;
                                    case 1:
                                        UseSordSkill();
                                        break;
                                }
                                break;

                            case Step.step2:
                                
                                UseGroundSkill(Random.Range(8, 12));
                                break;

                            case Step.step3:
                                
                                switch (Random.Range(0, 3))
                                {
                                    case 0:
                                        UseGroundSkill(40);
                                        break;

                                    case 1:
                                        UseMakeBossSkill();
                                        break;
                                    case 2:
                                        UseMakeSlimeSkill();
                                        break;
                                }
                                break;

                            case Step.step4:
                                
                                switch (Random.Range(1, 4))
                                {
                                    case 1:
                                        UseGroundSkill(50);
                                        break;
                                    case 2:
                                        UseMakeBossSkill();
                                        break;
                                    case 3:
                                        UseMakeSlimeSkill();
                                        break;
                                }
                                break;
                        }

                    }


                    break;
                }

        }
    }
    void Update()
    {

        


    }

    void fin()
    {
        BossAction = FireBoss_action.move;
        time = 0;
        isEndSkill = true;
    }

    void UseSordSkill()//스킬사용횟수
    {//
        if (time >= skill_cool)
        {
            Debug.Log("SORDSKILL\n");
            animator.SetTrigger("Skill2");
            animator.SetTrigger("Return");
            AttackObj.SetActive(true);
            Instantiate(AttackObj);
            //!왼쪽 보고있는지 아닌지 알려줘야함!

            fin();
        }
            
    }
    void Attack()//스킬사용횟수
    {//
        Debug.Log("ATTACK\n");
        animator.SetTrigger("Skill2");
        animator.SetTrigger("Return");
        Instantiate(AttackObj);

        time = 0;

    }
    void UseGroundSkill(int number)//오브젝트 생성 횟수
    {
        if (time >= skill_cool )
        {
            Debug.Log("GROUNDSKILL\n");

            move = false;
            animator.SetTrigger("Skill1");
            animator.SetTrigger("Return");
            Debug.Log(earthQuake);
            Debug.Log(number);

            roomManager.MakePrefebInRange(earthQuake, number, -222 + (mapSizeX / 2), -214 + (mapSizeY / 2), 44, 28, 1);
            //GameObject[] a= roomManager.MakePrefebInRange(Enemy1, number, -159, -159, 18, 18, 1);


            fin();
        }
    }
    void UseMakeBossSkill()
    {
        if (time >= skill_cool)
        {
            Debug.Log("MakeBossSkill\n");

            animator.SetTrigger("Skill1");
            animator.SetTrigger("Return");
            move = false;
            //roomManager.MakePrefebInRange(middleBoss, Random.Range(1,4), -159, -159, 18, 18, 1);

            fin();
        }
            
    }
    void UseMakeSlimeSkill()
    {
        if (time >= skill_cool)
        {
            Debug.Log("MakeSlimeSkill\n");

            animator.SetTrigger("Skill1");
            animator.SetTrigger("Return");
            move = false;
            int EnemyNumber = Random.Range(5, 15);
            int Enemy1Number = Random.Range(0, EnemyNumber + 1);
            int Enemy2Number = Random.Range(0, EnemyNumber - Enemy1Number + 1);
            int Enemy3Number = EnemyNumber - Enemy1Number - Enemy2Number;

            roomManager.MakePrefebInRange(Enemy1, Enemy1Number, -209 + (mapSizeX / 2), -209 + (mapSizeY / 2), 18, 18, 1);
            roomManager.MakePrefebInRange(Enemy2, Enemy2Number, -209 + (mapSizeX / 2), -209 + (mapSizeY / 2), 18, 18, 1);
            roomManager.MakePrefebInRange(Enemy3, Enemy3Number, -209 + (mapSizeX / 2), -209 + (mapSizeY / 2), 18, 18, 1);

            fin();
        }
        
    }




    void Fire_DropItem()
    {/*
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
        }*/
    }
}
