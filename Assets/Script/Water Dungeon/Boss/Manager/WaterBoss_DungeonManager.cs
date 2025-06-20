using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBoss_DungeonManager : MonoBehaviour
{
    public GameObject Boss;
    public EnemyData EnemyData_Boss;
    public GameObject Door;

    public GameObject[] trap = new GameObject[9];
    public int[] trap_effect = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };// 0 : �ƹ��͵� ����, 1 : fast_trap Ȱ��, 2 : slow_trap Ȱ��

    public int trap_count = 0;

    //float cool = 10f;

    GameObject player;

    int player_pos = 0;//Index���� �ƴ϶� Ʈ�� ��ȣ�� ����!
    int player_presentTrap = 0;//���� Ʈ�� ��ȣ, Ʈ������ �̵��� �� �ٲ�� ���� �����ϱ� ����
    int player_trapeffect = 0;//���� Ʈ�� ����/����� ���� (1 : fast trap, 2 : slow trap, 0 : ����)

    int Boss_pos = 0;//Index���� �ƴ϶� Ʈ�� ��ȣ�� ����!
    int Boss_presentTrap = 0;//���� Ʈ�� ��ȣ, Ʈ������ �̵��� �� �ٲ�� ���� �����ϱ� ����
    int Boss_trapeffect = 0;//���� Ʈ�� ����/����� ���� (1 : fast trap, 2 : slow trap, 0 : ����)

    private void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(0, -20f, -1);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Door.SetActive(true);
            Boss.SetActive(true);
        }
    }

    public IEnumerator Trap_effect_player_Update()
    {
        while (true)
        {
            if (player.transform.position.x < -3)
            {
                if (player.transform.position.y < -3)
                {
                    player_pos = 7;
                }
                else if (player.transform.position.y < 3)
                {
                    player_pos = 4;
                }
                else
                {
                    player_pos = 1;
                }
            }
            else if (player.transform.position.x < 3)
            {
                if (player.transform.position.y < -3)
                {
                    player_pos = 8;
                }
                else if (player.transform.position.y < 3)
                {
                    player_pos = 5;
                }
                else
                {
                    player_pos = 2;
                }
            }
            else
            {
                if (player.transform.position.y < -3)
                {
                    player_pos = 9;
                }
                else if (player.transform.position.y < 3)
                {
                    player_pos = 6;
                }
                else
                {
                    player_pos = 3;
                }
            }

            if (player_presentTrap != player_pos)
            {
                player_presentTrap = player_pos;

                //---------------------------���� ���·� �ǵ�����-------
                if (player_trapeffect == 1)
                {
                    PlayerData_Manager.moveSpeed -= 0.8f;
                }
                else if (player_trapeffect == 2)
                {
                    PlayerData_Manager.moveSpeed += 0.8f;
                }
                //-------------------------------------------------------

                //-------------------------------ȿ�� �ֱ� (����/�����)---------
                if (trap_effect[player_presentTrap - 1] == 1)
                {
                    PlayerData_Manager.moveSpeed += 0.8f;
                    player_trapeffect = 1;
                }
                else if (trap_effect[player_presentTrap - 1] == 2)
                {
                    PlayerData_Manager.moveSpeed -= 0.8f;
                    player_trapeffect = 2;
                }
                //--------------------------------------------------------------
            }

            yield return null;
        }
    }

    public IEnumerator Trap_effect_Boss_Update()
    {
        while (true)
        {
            if (Boss.transform.position.x < -3)
            {
                if (Boss.transform.position.y < -3)
                {
                    Boss_pos = 7;
                }
                else if (Boss.transform.position.y < 3)
                {
                    Boss_pos = 4;
                }
                else
                {
                    Boss_pos = 1;
                }
            }
            else if (Boss.transform.position.x < 3)
            {
                if (Boss.transform.position.y < -3)
                {
                    Boss_pos = 8;
                }
                else if (Boss.transform.position.y < 3)
                {
                    Boss_pos = 5;
                }
                else
                {
                    Boss_pos = 2;
                }
            }
            else
            {
                if (Boss.transform.position.y < -3)
                {
                    Boss_pos = 9;
                }
                else if (Boss.transform.position.y < 3)
                {
                    Boss_pos = 6;
                }
                else
                {
                    Boss_pos = 3;
                }
            }

            if (Boss_presentTrap != Boss_pos)
            {
                Boss_presentTrap = Boss_pos;

                //---------------------------���� ���·� �ǵ�����-------
                if (Boss_trapeffect == 1)
                {
                    EnemyData_Boss.Enemy_Speed += 0.8f;
                }
                else if (Boss_trapeffect == 2)
                {
                    EnemyData_Boss.Enemy_Speed -= 0.8f;
                }
                //-------------------------------------------------------

                //-------------------------------ȿ�� �ֱ� (����/�����)---------
                if (trap_effect[player_presentTrap - 1] == 1)
                {
                    EnemyData_Boss.Enemy_Speed -= 0.8f;
                    Boss_trapeffect = 1;
                }
                else if (trap_effect[player_presentTrap - 1] == 2)
                {
                    EnemyData_Boss.Enemy_Speed += 0.8f;
                    Boss_trapeffect = 2;
                }
                //--------------------------------------------------------------
            }

            yield return null;
        }
    }

    public void delete_trapEffect()
    {
        //---------------------------���� ���·� �ǵ�����-------
        if (player_trapeffect == 1)
        {
            PlayerData_Manager.moveSpeed -= 0.8f;
        }
        else if (player_trapeffect == 2)
        {
            PlayerData_Manager.moveSpeed += 0.8f;
        }
        //-------------------------------------------------------
    }


    public IEnumerator repeat_TrapSetting()//�ֱ������� Ʈ�� �����ϴ� �ڷ�ƾ
    {
        while (true)
        {
            setting_Trap();
            yield return new WaitForSeconds(10f);
        }
    }
    

    public void setting_Trap()//Ʈ�� �����ϴ� �Լ� (�ڷ�ƾ���� ���)
    {
        Reset_Trap();

        Change_Trap();

        Boss_presentTrap = 0;
        player_presentTrap = 0;
    }

    public void Delete_Trap()//��� Ʈ�� ���� (���� ���� ��, ���� ��� ��)
    {
        for (int num = 0; num < trap.Length; num++)
        {
            trap[num].SetActive(false);
        }
    }
    public void allslow_Trap()//Ʈ���� ��� slow trap���� �م� (���� ��ų1)
    {
        for (int num = 0; num < trap_effect.Length; num++)
        {
            trap_effect[num] = 2;
        }
        Boss_presentTrap = 0;
        player_presentTrap = 0;
    }
    void Reset_Trap()//Ʈ���� ��� fast trap���� �ʱ�ȭ�ϱ�
    {
        for (int num = 0; num < trap_effect.Length; num++)
        {
            trap_effect[num] = 1;
        }
    }

    void Change_Trap()//Ʈ�� �������� �����ϱ�
    {
        trap_count = Random.Range(4, 6);//slow trap ���� ���ϱ�
        //print(trap_count);

        for (int num = 0; num < trap_effect.Length; num++)
        {
            if (Random.Range(0, 3) != 1)
            {
                trap_effect[num] = 2;
                trap_count--;

                if (trap_count == 0)
                {
                    break;
                }
            }
        }

        if (trap_count != 0)
        {
            int i = 8;
            while (trap_count != 0)
            {
                if (trap_effect[i] != 2)
                {
                    trap_effect[i] = 2;
                }
                i--;
                trap_count--;
            }
        }

    }
}
