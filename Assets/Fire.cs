using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    EnemyData data;
    GameObject Boss;
    
    float time = 0;

    private void Awake()
    {
        
        Boss = GameObject.Find("FireBoss");
        data = Boss.GetComponent<EnemyData>();
        
    }

    private void OnEnable()
    {
        
        data = Boss.GetComponent<EnemyData>();
        
    }

    private void Update()
    {
        
        time += Time.deltaTime;
        if (time >= 3)
        {
           Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && (time >= 0.5))
        {
            PlayerData_Manager.Player_PresentHP -= data.Enemy_Damage;
            Destroy(gameObject);
        }

    }
}
