using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallDoor_windBoss : MonoBehaviour
{
    public GameObject Boss;
    public GameObject wall_;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wall_.SetActive(true);
            Boss.SetActive(true);
        }
    }
}
