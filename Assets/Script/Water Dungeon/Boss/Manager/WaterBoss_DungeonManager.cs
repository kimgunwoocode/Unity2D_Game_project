using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBoss_DungeonManager : MonoBehaviour
{
    public GameObject Boss;
    public EnemyData EnemyData_Boss;
    public GameObject Door;

    public GameObject[] trap = new GameObject[9];
    public int[] trap_effect = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };// 0 : 아무것도 없음, 1 : fast_trap 활성, 2 : slow_trap 활성

    public int trap_count = 0;

    //float cool = 10f;

    GameObject player;

    int player_pos = 0;//Index값이 아니라 트랩 번호로 저장!
    int player_presentTrap = 0;//현재 트랩 번호, 트랩에서 이동할 때 바뀌는 것을 감지하기 위함
    int player_trapeffect = 0;//현재 트랩 버프/디버프 유무 (1 : fast trap, 2 : slow trap, 0 : 없음)

    int Boss_pos = 0;//Index값이 아니라 트랩 번호로 저장!
    int Boss_presentTrap = 0;//현재 트랩 번호, 트랩에서 이동할 때 바뀌는 것을 감지하기 위함
    int Boss_trapeffect = 0;//현재 트랩 버프/디버프 유무 (1 : fast trap, 2 : slow trap, 0 : 없음)

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

                //---------------------------원래 상태로 되돌리기-------
                if (player_trapeffect == 1)
                {
                    PlayerData_Manager.moveSpeed -= 0.8f;
                }
                else if (player_trapeffect == 2)
                {
                    PlayerData_Manager.moveSpeed += 0.8f;
                }
                //-------------------------------------------------------

                //-------------------------------효과 주기 (버프/디버프)---------
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

                //---------------------------원래 상태로 되돌리기-------
                if (Boss_trapeffect == 1)
                {
                    EnemyData_Boss.Enemy_Speed += 0.8f;
                }
                else if (Boss_trapeffect == 2)
                {
                    EnemyData_Boss.Enemy_Speed -= 0.8f;
                }
                //-------------------------------------------------------

                //-------------------------------효과 주기 (버프/디버프)---------
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
        //---------------------------원래 상태로 되돌리기-------
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


    public IEnumerator repeat_TrapSetting()//주기적으로 트랩 변경하는 코루틴
    {
        while (true)
        {
            setting_Trap();
            yield return new WaitForSeconds(10f);
        }
    }
    

    public void setting_Trap()//트랩 변경하는 함수 (코루틴에서 사용)
    {
        Reset_Trap();

        Change_Trap();

        Boss_presentTrap = 0;
        player_presentTrap = 0;
    }

    public void Delete_Trap()//모든 트랩 제거 (보스 등장 전, 보스 사망 후)
    {
        for (int num = 0; num < trap.Length; num++)
        {
            trap[num].SetActive(false);
        }
    }
    public void allslow_Trap()//트랩을 모두 slow trap으로 바뀍 (보스 스킬1)
    {
        for (int num = 0; num < trap_effect.Length; num++)
        {
            trap_effect[num] = 2;
        }
        Boss_presentTrap = 0;
        player_presentTrap = 0;
    }
    void Reset_Trap()//트랩을 모두 fast trap으로 초기화하기
    {
        for (int num = 0; num < trap_effect.Length; num++)
        {
            trap_effect[num] = 1;
        }
    }

    void Change_Trap()//트랩 랜덤으로 변경하기
    {
        trap_count = Random.Range(4, 6);//slow trap 개수 정하기
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
