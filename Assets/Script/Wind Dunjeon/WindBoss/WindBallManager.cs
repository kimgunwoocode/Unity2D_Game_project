using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindBallManager : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1");
        if (collision.gameObject.tag == "Player")
        {
            WindBoss_Manager.isAteBall = true;
            Destroy(gameObject);
        }
    }
}
