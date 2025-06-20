using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHuman_Skill : MonoBehaviour
{
    float time_ = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (time_ <= 0)
        {
            if (collision.gameObject.tag == "Player")//충돌한 오브젝트가 플레이어 일 때
            {
                collision.gameObject.GetComponent<PlayerSpeed_Manager>().SpeedEffect_cool(1.01f, 1.8f);
                time_ = 2f;
            }
            else
            {
                time_ = 0.3f;
            }
        }

        time_ -= Time.deltaTime;
    }
}
