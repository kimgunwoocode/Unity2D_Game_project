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

    public int madStack;//����ȭ ���� ����
    public bool stop;//�ι� ���� ����+������ ������ ����
    public bool isPlayerEnter;//�÷��̾ ���� �Ÿ� �̳��� �� ������
    public static bool isAteBall;//�÷��̾ ���� �Ծ��°�?
    public static bool isInArea;//���� �ȿ� �ִ°�?
    public bool isOnBuff;
    int attackCount;
    public bool madForm;
    int randomSkill;
    bool attackCool;//���� �� ���� �ð� �������� ����
    
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
        dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//�÷��̾���� �Ÿ�

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

            rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemydata.Enemy_Speed;//�÷��̾� �������� ���� �̵�
        }
        else
        {
            rigid.velocity = (Vector2.zero);
        }

        /*if (Input.GetKeyDown(KeyCode.V) && onAttack == false)//test��
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

            //���� ���� ��ġ ����, ����ȭ�� �� ����
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

        if (isAteBall == true && buffCountDown.isBuffed == false)//���� ������ �̵��ӵ� 1.5�� ���� Ȱ��ȭ
        {
            buffImage.SetActive(true);
        }
    }

    IEnumerator MadBoom()
    {
        madAttack2Audio.enabled = false;//������ ȿ���� �ѱ� ���� ��
        wind_boss_ani.SetTrigger("MadAttackReady");

        yield return new WaitForSeconds(0.5f);

        sprite.enabled = false;//��� ����

        madAttackArea.SetActive(true);//���� ǥ��

        //���� ���� ��ġ ����
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
                PlayerData_Manager.Player_PresentHP -= (PlayerData_Manager.Player_MaxHP / 3) * 2; //������ ���� ������ �ִ� ü���� 2/3 �ս�
                madStack += 1;
                Invoke("MadStackDown", 60);//60�� �� madStack ����

            }
            else
            {
                PlayerData_Manager.Player_PresentHP = 0; //60�� �̳��� �ٽ� ����ȭ ������ ������ ���
            }
        }
        else if (isAteBall == true)
        {
            Debug.Log("good");
        }

        isAteBall = false;

        Vector3 aniPosition = new Vector3(0, 25, -1.1f);

        madAttackAudio.enabled = true;//ȿ����
        for (int i = 0; i < 100; i++)
        {
            int randomAni = Random.Range(1, 19);//1~18�� �� �������� ����

            switch (randomAni)//���� ������Ʈ ����
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

            yield return new WaitForSeconds(0.01f);//������ ������ ��

        }

        madAttackArea.SetActive(false);

        gameObject.transform.position = player.transform.position;//�÷��̾� ��ġ�� �̵�

        madAttackArea2.SetActive(true);//���� Ȱ��ȭ

        yield return new WaitForSeconds(0.5f);//1�� �� ����

        madAttack2_Renderer.color = Color.white;
        madAttack2_Ani.enabled = true;
        madAttack2Audio.enabled = true;//ȿ����
        if (isInArea)
        {
            PlayerData_Manager.Player_PresentHP -= PlayerData_Manager.Player_MaxHP / 3;//����
            isInArea = false;
            Debug.Log("madAttack2Damage");
        }
        yield return new WaitForSeconds(0.9166666666666667f);

        madAttack2_Ani.enabled = false;
        madAttack2_Renderer.color = madAttack2Color;//���� ������ ����
        madAttack2_Renderer.sprite = attackRange;
        madAttackArea2.SetActive(false);

        sprite.enabled = true;
        wind_boss_ani.SetTrigger("MadAttackEnd");
        yield return new WaitForSeconds(0.25f);

        yield return new WaitForSeconds(0.7f);
        stop = false;
        yield return new WaitForSeconds(1.5f);
        attackCool = false;

        madAttackAudio.enabled = false;//������ ȿ���� �ѱ� ���� ��
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

    IEnumerator Attack1(float speed)//speed�� ����ȭ �� �ӵ� ������ ����
    {
        wind_boss_ani.SetTrigger("Attack1");

        yield return new WaitForSeconds(0.25f / speed);

        GameObject attack1_1 = Instantiate(attack1_wind, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D attack1_1_rigid = attack1_1.GetComponent<Rigidbody2D>();
        attack1_1_rigid.velocity = ((Vector2)(player.transform.position - attack1_1.transform.position - attack1Offset).normalized) * enemydata.Enemy_Speed * 2;//�÷��̾� �������� �̵�

        yield return new WaitForSeconds(0.25f / speed);

        GameObject attack1_2 = Instantiate(attack1_wind, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D attack1_2_rigid = attack1_2.GetComponent<Rigidbody2D>();
        attack1_2_rigid.velocity = ((Vector2)(player.transform.position - attack1_2.transform.position - attack1Offset).normalized) * enemydata.Enemy_Speed * 2;//�÷��̾� �������� �̵�

        yield return new WaitForSeconds(0.25f / speed);

        GameObject attack1_3 = Instantiate(attack1_wind, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D attack1_3_rigid = attack1_3.GetComponent<Rigidbody2D>();
        attack1_3_rigid.velocity = ((Vector2)(player.transform.position - attack1_3.transform.position - attack1Offset).normalized) * enemydata.Enemy_Speed * 2;//�÷��̾� �������� �̵�

        yield return new WaitForSeconds(0.8f / speed);
        stop = false;
        yield return new WaitForSeconds(1.5f);
        attackCool = false;
    }

    IEnumerator Attack2(float speed)//speed�� ����ȭ �� �ӵ� ������ ����
    {
        attack2Audio.enabled = false;//������ ȿ���� �ѱ� ���� ��
        if (madForm == false)
        {
            wind_boss_ani.SetTrigger("Attack2Ready");

            yield return new WaitForSeconds(0.5f);//���� �ð� ���� ����

            attack2Area.SetActive(true);//���� Ȱ��ȭ

            yield return new WaitForSeconds(0.166666666666666666f);//1�� �� ����
            wind_boss_ani.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.833333333333333333f);//1�� �� ����
        }
        else if (madForm == true)
        {
            attack2Area.SetActive(true);//���� Ȱ��ȭ

            yield return new WaitForSeconds(0.6f);//1�� �� ����
            wind_boss_ani.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.4f);//1�� �� ����
        }

        attack2_Renderer.color = Color.white;
        attack2_Ani.enabled = true;
        attack2Audio.enabled = true;//ȿ����

        if (isInArea)
        {
            PlayerData_Manager.Player_PresentHP -= enemydata.Enemy_Damage * 2;//����
            Debug.Log("attack2Damage");
        }
        yield return new WaitForSeconds(0.9166666666666667f);

        attack2_Ani.enabled = false;
        attack2_Renderer.color = madAttack2Color;//���� ������ ����
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
