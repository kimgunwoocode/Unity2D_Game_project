using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2Manager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            WindBoss_Manager.isInArea = true;
            Debug.Log("in");
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            WindBoss_Manager.isInArea = false;
            Debug.Log("out");
        }
    }
}
