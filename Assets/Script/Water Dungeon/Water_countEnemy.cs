using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_countEnemy : MonoBehaviour
{
    public EnemyData enemydata;
    public WaterDungeon1_Manager dungeonManager;

    void Start()
    {
        enemydata = gameObject.GetComponent<EnemyData>();
        dungeonManager = GameObject.Find("Manager").GetComponent<WaterDungeon1_Manager>();
    }

    void Update()
    {
        if (enemydata.Enemy_Die)
        {
            dungeonManager.stageenemy_count++;
            Destroy(this);
        }
    }
}
