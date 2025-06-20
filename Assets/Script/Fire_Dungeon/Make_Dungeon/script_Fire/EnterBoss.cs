using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoss : MonoBehaviour
{
    
    public Vector2 Next_PlayerPosition_;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Button1Manager.Button1_Type = 2;
            keyboard.Button1_Type = 2;
            StartCoroutine("OntriggerStay_");
        }
    }

    IEnumerator OntriggerStay_()
    {
        while (true)
        {
            if (Button1Manager.Button1_Type == 3 || keyboard.Button1_Type == 3)
            {
                player.transform.position = new Vector3(-200, -224, 0);
                yield break;
            }
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Button1Manager.Button1_Type = 1;
            keyboard.Button1_Type = 1;
            StopCoroutine("OntriggerStay_");
        }
    }
}
