using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_WaterBoss : MonoBehaviour
{
    public int trap_number;

    public WaterBoss_DungeonManager waterBossManager;
    GameObject trap_fast;
    GameObject trap_slow;


    private void Start()
    {
        trap_fast = transform.GetChild(0).gameObject;
        trap_slow = transform.GetChild(1).gameObject;
    }


    private void LateUpdate()
    {
        if (waterBossManager.trap_effect[trap_number - 1] == 1)
        {
            trap_fast.SetActive(true);
            trap_slow.SetActive(false);
        }
        else if (waterBossManager.trap_effect[trap_number - 1] == 2)
        {
            trap_fast.SetActive(false);
            trap_slow.SetActive(true);
        }
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData_Manager.moveSpeed -= speed_effect;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyData>().Enemy_Speed += speed_effect;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData_Manager.moveSpeed += speed_effect;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyData>().Enemy_Speed -= speed_effect;
        }
    }
    */
}
