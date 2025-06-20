using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomRoom : MonoBehaviour
{
    private GameObject roomManagerObj;
    private GameObject enemyLoadObj;
    private GameObject enterBoss;

    //Trigger ������Ʈ�� ������Ʈ�ν� �߰�
    private GameObject Enemy1;
    private GameObject Enemy2;
    private GameObject Enemy3;
    private RoomManager roomManager;


    EnemyData[] EnemyData_isDead_2;
    //bool[] EnemyData_isDead;//������
    GameObject[] Enemy;
    GameObject[] EnemyArray;
    private int EnemyNumber;//�� ���� ���� ��
    private enemyLoad enemy;//�� ����Ŭ����

    private int x;
    private int y;
    private int w;
    private int h;
    private int roomCount;//�� ������ ���� �ѹ��� ������ �� �ֵ��� �ϱ�����
    int number;
    private RoomTrigger roomTrigger;
    private bool inPlayer = false;

    public int difficulty = 0; //���̵� 1~10

    private void Awake()
    {

        enterBoss = GameObject.Find("EnterBoss");

        roomManagerObj = GameObject.Find("RoomManager");
        enemyLoadObj = GameObject.Find("EnemyLoad");
        enemy = enemyLoadObj.GetComponent<enemyLoad>();
        Enemy1 = enemy.Enemy1;
        Enemy2 = enemy.Enemy2;
        Enemy3 = enemy.Enemy3;
        roomManager = roomManagerObj.GetComponent<RoomManager>();

        //Debug.Log(enemy);
        //Debug.Log(roomManager);

        roomCount = 0;
        roomTrigger = GetComponent<RoomTrigger>();
        number = roomTrigger.roomTrigerNumber;

        x = RoomManager.roomInfor[number].roomX;
        y = RoomManager.roomInfor[number].roomY;
        w = RoomManager.roomInfor[number].roomWidth;
        h = RoomManager.roomInfor[number].roomHeight;
    }

    public void MakeEnemy()
    {
        switch (RoomManager.roomInfor[number].roomType)
        {
            case Room.RoomType.A:
                difficulty = Random.Range(1, 4);
                break;
            case Room.RoomType.B:
                difficulty = Random.Range(2, 6);
                break;
            case Room.RoomType.C:
                difficulty = Random.Range(5, 8);
                break;
            case Room.RoomType.D:
                difficulty = Random.Range(7, 11);
                break;
            default:
                break;

        }



        EnemyNumber = Random.Range(3, 12);
        //EnemyData_isDead = new bool[EnemyNumber];
        EnemyData_isDead_2 = new EnemyData[EnemyNumber];
        EnemyArray = new GameObject[EnemyNumber];
        int Enemy1Number = Random.Range(0, EnemyNumber + 1);
        int Enemy2Number = Random.Range(0, EnemyNumber - Enemy1Number + 1);
        int Enemy3Number = EnemyNumber - Enemy1Number - Enemy2Number;

        Enemy = roomManager.MakePrefebInRange(Enemy1, Enemy1Number, x, y, w, h, 1);
        int counting = 0;
        foreach (GameObject obj in Enemy)
        {
            EnemyData_isDead_2[counting] = obj.GetComponent<EnemyData>();
            EnemyArray[counting] = obj;
            counting++;
        }
        //counting �ʱ�ȭ ���ϰ� �Ѿ�Ƿ� �׷��� �̾~
        Enemy = roomManager.MakePrefebInRange(Enemy2, Enemy2Number, x, y, w, h, 1);
        foreach (GameObject obj in Enemy)
        {
            EnemyData_isDead_2[counting] = obj.GetComponent<EnemyData>();
            EnemyArray[counting] = obj;
            counting++;
        }

        Enemy = roomManager.MakePrefebInRange(Enemy3, Enemy3Number, x, y, w, h, 1);
        foreach (GameObject obj in Enemy)
        {
            EnemyData_isDead_2[counting] = obj.GetComponent<EnemyData>();
            EnemyArray[counting] = obj;
            counting++;
        }

        foreach (GameObject obj in EnemyArray)
        {
            obj.GetComponent<EnemyData>().Enemy_Damage += difficulty * 2;
            obj.GetComponent<EnemyData>().Enemy_Speed += difficulty / 4;
            obj.GetComponent<EnemyData>().Enemy_MaxHP += difficulty * 5;
        }

        roomManager.enterDo(number);
        roomCount++;
    }

    public void MakingRoom()//�濡 �÷��̾ ������ �� 
    {
        enterBoss.SetActive(true);
        enterBoss.transform.position = roomManager.TurnPixelToWorldPos(x, y, w, h);
        DestroyRoom();

            /*
        if (roomCount == 0)
        {
            MakeEnemy();
        }*/

    }
    public void DestroyRoom()//���� ��� ���� óġ���� ��
    {
        roomManager.outDo(number);

    }

    private void Update()
    {
        if (roomCount == 1)
        {

            int counting = 0;
            //foreach (bool b in EnemyData_isDead_2.Enemy_Die)
            for (int i = 0; i < EnemyData_isDead_2.Length; i++)
            {
                //Test
                bool b = EnemyData_isDead_2[i].Enemy_Die;


                if (b)
                {
                    counting++;
                }
            }
            if (counting == EnemyNumber)
            {
                DestroyRoom();
                roomCount++;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !inPlayer)
        {
            MakingRoom();
            inPlayer = true;
        }
    }
}
