using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBoss_Manager : MonoBehaviour
{
    public Animator wind_boss_ani;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;
    public EnemyData enemydata;

    WindBallManager windBall;
    Attack1Manager attack1;
    public BuffCountDown buffCountDown;

    public GameObject player;
    public GameObject ball;
    public GameObject buffImage;
    public Sprite attackRange;
    public GameObject madAttackArea;
    public GameObject madAttackArea2;
    public AudioSource madAttackAudio;
    public Animator madAttack2_Ani;
    public SpriteRenderer madAttack2_Renderer;
    public Color madAttack2Color;
    public AudioSource madAttack2Audio;
    public GameObject attack1_wind;
    public GameObject attack2Area;
    public Animator attack2_Ani;
    public SpriteRenderer attack2_Renderer;
    public AudioSource attack2Audio;
    public GameObject exit;

    public GameObject madAttack1;
    public GameObject madAttack2;
    public GameObject madAttack3;
    public GameObject madAttack4;
    public GameObject madAttack5;
    public GameObject madAttack6;
    public GameObject madAttack7;
    public GameObject madAttack8;
    public GameObject madAttack9;
    public GameObject madAttack10;
    public GameObject madAttack11;
    public GameObject madAttack12;
    public GameObject madAttack13;
    public GameObject madAttack14;
    public GameObject madAttack15;
    public GameObject madAttack16;
    public GameObject madAttack17;
    public GameObject madAttack18;

    public int madStack;//광폭화 공격 스택
    public bool stop;//두번 공격 방지+공격중 움직임 방지
    public bool isPlayerEnter;//플레이어가 일정 거리 이내일 때 움직임
    public static bool isAteBall;//플레이어가 공을 먹었는가?
    public static bool isInArea;//범위 안에 있는가?
    public bool isOnBuff;
    int attackCount;
    public bool madForm;
    int randomSkill;
    bool attackCool;//공격 후 일정 시간 공격하지 않음
    
    float dis;
    Vector3 attack1Offset = new Vector3(0.13f, -1.55f, 0);

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        madStack = 0;
        stop = false;
        isPlayerEnter = false;
        isAteBall = false;
        isInArea = false;
        attackCount = 0;
        madForm = false;
        isOnBuff = false;
    }

    void Update()
    {
        dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//플레이어와의 거리

        if (dis <= 8)
        {
            isPlayerEnter = true;
        }

        if (isPlayerEnter == true && stop == false)
        {

            if (player.transform.position.x > gameObject.transform.position.x)
            {
                sprite.flipX = false;
            }
            else if (player.transform.position.x < gameObject.transform.position.x)
            {
                sprite.flipX = true;
            }

            rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;//플레이어 방향으로 몬스터 이동
        }
        else
        {
            rigid.velocity = (Vector2.zero);
        }

        /*if (Input.GetKeyDown(KeyCode.V) && onAttack == false)//test용
        {
            onAttack = true;
            
            StartCoroutine(MadBoom());
        }
        else if (Input.GetKeyDown(KeyCode.A) && onAttack == false)
        {
            onAttack = true;

            StartCoroutine(Attack1());
        }
        else if (Input.GetKeyDown(KeyCode.S) && onAttack == false)
        {
            onAttack = true;

            StartCoroutine(Attack2());
        }*/

        if (stop == false && isPlayerEnter == true && madForm == false && attackCool == false)
        {
            randomSkill = Random.Range(1, 3);

            if (randomSkill == 1)
            {
                stop = true;
                attackCool = true;

                StartCoroutine(Attack1(1f));
            }
            else if (randomSkill == 2)
            {
                stop = true;
                attackCool = true;

                StartCoroutine(Attack2(1f));
            }
        }
        else if (stop == false && isPlayerEnter == true && madForm == true && attackCool == false)
        {
            if (attackCount < 5)
            {
                randomSkill = Random.Range(1, 3);

                if (randomSkill == 1)
                {
                    stop = true;
                    attackCool = true;
                    attackCount += 1;
                    StartCoroutine(Attack1(1.3f));
                }
                else if (randomSkill == 2)
                {
                    stop = true;
                    attackCool = true;
                    attackCount += 1;
                    StartCoroutine(Attack2(1.5f));
                }
            }
            else if (attackCount == 5)
            {
                stop = true;
                attackCool = true;
                attackCount = 0;
                StartCoroutine(MadBoom());
            }
        }

        if (madForm == false && enemydata.Enemy_PresentHP < enemydata.Enemy_MaxHP * 1 / 2)
        {
            madForm = true;
            enemydata.Enemy_Speed = enemydata.Enemy_Speed * 3/2;
            enemydata.Enemy_Damage = enemydata.Enemy_Damage * 3/2;

            //공의 랜덤 위치 배정, 광폭화시 공 지급
            int x = Random.Range(-6, 7);
            int y = Random.Range(-3, 4);
            Vector3 ballPosition = new Vector3(x, y + 25, -1);
            Quaternion rotation = new Quaternion(0, 0, 0, 0);
            GameObject ball_1 = Instantiate(ball, ballPosition, rotation);
            wind_boss_ani.SetTrigger("MadForm");
            StartCoroutine(DestroyFirstBall(ball_1));
        }

        if (enemydata.Enemy_Die == true)
        {
            stop = true;
            wind_boss_ani.SetTrigger("Die");
        }

        if (isAteBall == true && buffCountDown.isBuffed == false)//공을 먹으면 이동속도 1.5배 버프 활성화
        {
            buffImage.SetActive(true);
        }
    }

    IEnumerator MadBoom()
    {
        madAttack2Audio.enabled = false;//다음에 효과음 켜기 위해 끔
        wind_boss_ani.SetTrigger("MadAttackReady");

        yield return new WaitForSeconds(0.5f);

        sprite.enabled = false;//모습 숨김

        madAttackArea.SetActive(true);//범위 표시

        //공의 랜덤 위치 배정
        int x = Random.Range(-6, 7);
        int y = Random.Range(-3, 4);
        Vector3 ballPosition = new Vector3(x, y + 25, -1);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);

        GameObject ball1 = Instantiate(ball, ballPosition, rotation);

        yield return new WaitForSeconds(2.0f);

        if (isAteBall == false)
        {
            Debug.Log("madAttackDamage");
            Destroy(ball1); 
            if (madStack < 1)
            {
                PlayerData_Manager.Player_PresentHP -= (PlayerData_Manager.Player_MaxHP / 3) * 2; //구슬을 먹지 않으면 최대 체력의 2/3 손실
                madStack += 1;
                Invoke("MadStackDown", 60);//60초 뒤 madStack 감소

            }
            else
            {
                PlayerData_Manager.Player_PresentHP = 0; //60초 이내에 다시 광폭화 패턴을 맞으면 즉사
            }
        }
        else if (isAteBall == true)
        {
            Debug.Log("good");
        }

        isAteBall = false;

        Vector3 aniPosition = new Vector3(0, 25, -1.1f);

        madAttackAudio.enabled = true;//효과음
        for (int i = 0; i < 100; i++)
        {
            int randomAni = Random.Range(1, 19);//1~18번 중 랜덤으로 뽑음

            switch (randomAni)//랜던 오브젝트 생성
            {
                case 1:
                    GameObject ani1 = Instantiate(madAttack1, aniPosition, rotation);
                    break;
                case 2:
                    GameObject ani2 = Instantiate(madAttack2, aniPosition, rotation);
                    break;
                case 3:
                    GameObject ani3 = Instantiate(madAttack3, aniPosition, rotation);
                    break;
                case 4:
                    GameObject ani4 = Instantiate(madAttack4, aniPosition, rotation);
                    break;
                case 5:
                    GameObject ani5 = Instantiate(madAttack5, aniPosition, rotation);
                    break;
                case 6:
                    GameObject ani6 = Instantiate(madAttack6, aniPosition, rotation);
                    break;
                case 7:
                    GameObject ani7 = Instantiate(madAttack7, aniPosition, rotation);
                    break;
                case 8:
                    GameObject ani8 = Instantiate(madAttack8, aniPosition, rotation);
                    break;
                case 9:
                    GameObject ani9 = Instantiate(madAttack9, aniPosition, rotation);
                    break;
                case 10:
                    GameObject ani10 = Instantiate(madAttack10, aniPosition, rotation);
                    break;
                case 11:
                    GameObject ani11 = Instantiate(madAttack11, aniPosition, rotation);
                    break;
                case 12:
                    GameObject ani12 = Instantiate(madAttack12, aniPosition, rotation);
                    break;
                case 13:
                    GameObject ani13 = Instantiate(madAttack13, aniPosition, rotation);
                    break;
                case 14:
                    GameObject ani14 = Instantiate(madAttack14, aniPosition, rotation);
                    break;
                case 15:
                    GameObject ani15 = Instantiate(madAttack15, aniPosition, rotation);
                    break;
                case 16:
                    GameObject ani16 = Instantiate(madAttack16, aniPosition, rotation);
                    break;
                case 17:
                    GameObject ani17 = Instantiate(madAttack17, aniPosition, rotation);
                    break;
                case 18:
                    GameObject ani18 = Instantiate(madAttack18, aniPosition, rotation);
                    break;
            }

            yield return new WaitForSeconds(0.01f);//일정한 간격을 둠

        }

        madAttackArea.SetActive(false);

        gameObject.transform.position = player.transform.position;//플레이어 위치로 이동

        madAttackArea2.SetActive(true);//범위 활성화

        yield return new WaitForSeconds(0.5f);//1초 뒤 공격

        madAttack2_Renderer.color = Color.white;
        madAttack2_Ani.enabled = true;
        madAttack2Audio.enabled = true;//효과음
        if (isInArea)
        {
            PlayerData_Manager.Player_PresentHP -= PlayerData_Manager.Player_MaxHP / 3;//공격
            isInArea = false;
            Debug.Log("madAttack2Damage");
        }
        yield return new WaitForSeconds(0.9166666666666667f);

        madAttack2_Ani.enabled = false;
        madAttack2_Renderer.color = madAttack2Color;//원래 색으로 변경
        madAttack2_Renderer.sprite = attackRange;
        madAttackArea2.SetActive(false);

        sprite.enabled = true;
        wind_boss_ani.SetTrigger("MadAttackEnd");
        yield return new WaitForSeconds(0.25f);

        yield return new WaitForSeconds(0.7f);
        stop = false;
        yield return new WaitForSeconds(1.5f);
        attackCool = false;

        madAttackAudio.enabled = false;//다음에 효과음 켜기 위해 끔
    }

    void MadStackDown()
    {
        if (madStack > 0)
        {
            madStack -= 1;
        }
    }

    IEnumerator DestroyFirstBall(GameObject firstBall)
    {
        yield return new WaitForSeconds(10.0f);

        Destroy(firstBall);
    }

    IEnumerator Attack1(float speed)//speed로 광폭화 시 속도 빠르게 조정
    {
        wind_boss_ani.SetTrigger("Attack1");

        yield return new WaitForSeconds(0.25f / speed);

        GameObject attack1_1 = Instantiate(attack1_wind, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D attack1_1_rigid = attack1_1.GetComponent<Rigidbody2D>();
        attack1_1_rigid.velocity = ((Vector2)(player.transform.position - attack1_1.transform.position - attack1Offset).normalized) * enemydata.Enemy_Speed * 2;//플레이어 방향으로 이동

        yield return new WaitForSeconds(0.25f / speed);

        GameObject attack1_2 = Instantiate(attack1_wind, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D attack1_2_rigid = attack1_2.GetComponent<Rigidbody2D>();
        attack1_2_rigid.velocity = ((Vector2)(player.transform.position - attack1_2.transform.position - attack1Offset).normalized) * enemydata.Enemy_Speed * 2;//플레이어 방향으로 이동

        yield return new WaitForSeconds(0.25f / speed);

        GameObject attack1_3 = Instantiate(attack1_wind, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D attack1_3_rigid = attack1_3.GetComponent<Rigidbody2D>();
        attack1_3_rigid.velocity = ((Vector2)(player.transform.position - attack1_3.transform.position - attack1Offset).normalized) * enemydata.Enemy_Speed * 2;//플레이어 방향으로 이동

        yield return new WaitForSeconds(0.8f / speed);
        stop = false;
        yield return new WaitForSeconds(1.5f);
        attackCool = false;
    }

    IEnumerator Attack2(float speed)//speed로 광폭화 시 속도 빠르게 조정
    {
        attack2Audio.enabled = false;//다음에 효과음 켜기 위해 끔
        if (madForm == false)
        {
            wind_boss_ani.SetTrigger("Attack2Ready");

            yield return new WaitForSeconds(0.5f);//일정 시간 이후 공격

            attack2Area.SetActive(true);//범위 활성화

            yield return new WaitForSeconds(0.166666666666666666f);//1초 뒤 공격
            wind_boss_ani.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.833333333333333333f);//1초 뒤 공격
        }
        else if (madForm == true)
        {
            attack2Area.SetActive(true);//범위 활성화

            yield return new WaitForSeconds(0.6f);//1초 뒤 공격
            wind_boss_ani.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.4f);//1초 뒤 공격
        }

        attack2_Renderer.color = Color.white;
        attack2_Ani.enabled = true;
        attack2Audio.enabled = true;//효과음

        if (isInArea)
        {
            PlayerData_Manager.Player_PresentHP -= enemydata.Enemy_Damage * 2;//공격
            Debug.Log("attack2Damage");
        }
        yield return new WaitForSeconds(0.9166666666666667f);

        attack2_Ani.enabled = false;
        attack2_Renderer.color = madAttack2Color;//원래 색으로 변경
        attack2_Renderer.sprite = attackRange;
        attack2Area.SetActive(false);

        yield return new WaitForSeconds(0.6f / speed);
        stop = false;
        yield return new WaitForSeconds(1.5f);
        attackCool = false;
    }

    void BackToTown()
    {
        exit.SetActive(true);

        for (int i = 0; i < 10; i++)
        {
            Wind_DropItem();
        }

        if (PlayerData_Manager.dungeon_clear[1] == false)
        {
            itemData.AddItem(13);
            PlayerData_Manager.dungeon_clear[1] = true;
        }
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
}
