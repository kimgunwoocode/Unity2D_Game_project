using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterDungeon1_Manager : MonoBehaviour
{
    public GameObject[] Door = new GameObject[5];
    public GameObject[] Enemie_ = new GameObject[18];//5, 8, 5

    public Text[] spawnCool_text = new Text[3];

    GameObject player;

    int dungeonDoor_count = 0;
    bool[] stage_SpawnCool = new bool[3] { true, true, true };

    public int stageenemy_count = 0;

    private void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(-40, 5, -1);
        Move_Door(false);
    }

    private void Update()
    {
        if (player.transform.position.x > 30)//stage3 입장
        {
            dungeonDoor_count = 3;
            if (stage_SpawnCool[2] == true)
            {
                StartCoroutine(stage_manager_fun());
            }
        }
        else if (player.transform.position.x > 0.6f && player.transform.position.x < 20.2)//stage2 입장
        {
            dungeonDoor_count = 2;
            if (stage_SpawnCool[1] == true)
            {
                StartCoroutine(stage_manager_fun());
            }

        }
        else if (player.transform.position.x > -25.4f && player.transform.position.x < -7.8f)//stage1 입장
        {
            dungeonDoor_count = 1;
            if (stage_SpawnCool[0] == true)
            {
                StartCoroutine(stage_manager_fun());
            }

        }
        else
        {
            dungeonDoor_count = 0;
        }

    }

    public void Move_Door(bool state)
    {
        Door[0].SetActive(state);
        Door[1].SetActive(state);
        Door[2].SetActive(state);
        Door[3].SetActive(state);
        Door[4].SetActive(state);
    }



    IEnumerator stage_manager_fun()
    {
        stage_SpawnCool[dungeonDoor_count - 1] = false;
        Move_Door(true);
        spawnCool_text[dungeonDoor_count - 1].text = "";

        //List<EnemyData> enemyData = new List<EnemyData>();

        int start_ = 0, end_ = 0;
        switch(dungeonDoor_count)
        {
            case 1:
                start_ = 0; end_ = 4; break;
            case 2:
                start_ = 5; end_ = 12; break;
            case 3:
                start_ = 13; end_ = 18; break;
        }
        for (int i = start_; i <= end_; i++)
        {
            GameObject enemy = Instantiate(Enemie_[i], Enemie_[i].transform.position, Quaternion.identity);
            enemy.AddComponent<Water_countEnemy>();
            enemy.SetActive(true);
            //enemyData.Add(enemy.GetComponent<EnemyData>());
        }

        while (true)
        {
            /*
            for (int I = enemyData.Count; I >= 0; I--)
            {
                if (enemyData[I].enabled == false)
                    enemyData.Remove(enemyData[I]);
            }

            if (enemyData.Count == 0)
            {
                Move_Door(false);
                StartCoroutine("spawn_cool", dungeonDoor_count);
                dungeonDoor_count = 0;
                yield break;
            }
            */

            switch (dungeonDoor_count)
            {
                case 1:
                    {
                        if (stageenemy_count == 5)
                        {
                            Move_Door(false);
                            StartCoroutine("spawn_cool", dungeonDoor_count);
                            dungeonDoor_count = 0;
                            stageenemy_count = 0;
                            yield break;
                        }
                        break;
                    }
                case 2:
                    {
                        if (stageenemy_count == 8)
                        {
                            Move_Door(false);
                            StartCoroutine("spawn_cool", dungeonDoor_count);
                            dungeonDoor_count = 0;
                            stageenemy_count = 0;
                            yield break;
                        }
                        break;
                    }
                case 3:
                    {
                        if (stageenemy_count == 5)
                        {
                            Move_Door(false);
                            StartCoroutine("spawn_cool", dungeonDoor_count);
                            dungeonDoor_count = 0;
                            stageenemy_count = 0;
                            yield break;
                        }
                        break;
                    }
            }

            yield return null;
        }
    }

    public IEnumerator spawn_cool(int stage_num)
    {
        float cool = 60f;

        while (true)
        {
            spawnCool_text[stage_num - 1].text = cool.ToString("F0");
            cool -= Time.deltaTime;
            if (cool <= 0)
            {
                spawnCool_text[stage_num - 1].text = "스폰 가능";
                break;
            }
            yield return null;
        }

        stage_SpawnCool[stage_num - 1] = true;
    }
}
