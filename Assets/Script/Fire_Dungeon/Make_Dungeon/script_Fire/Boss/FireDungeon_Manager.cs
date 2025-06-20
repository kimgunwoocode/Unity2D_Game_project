using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDungeon_Manager : MonoBehaviour
{
    public GameObject Boss;
    public EnemyData EnemyData_Boss;
    public GameObject Door;//입구 -->트리거 부딪치면 ㅁ문 열기
    public GameObject Hp;

   

    //float cool = 10f;

    GameObject player;

   



    private float time=0f;

    private void Start()
    {
        player = GameObject.Find("Player");
        Hp.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Door.SetActive(true);
            Boss.SetActive(true);
            Hp.SetActive(true);
        }
    }


    private void Update()
    {
        time += Time.deltaTime;
        
        
    }
    

    


}
