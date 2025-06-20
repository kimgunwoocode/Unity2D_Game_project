using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBomb_Boom : MonoBehaviour
{
    public WindBomb_Manager windBomb;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("1");
            windBomb.isInArea = true;
            //Debug.Log("in");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            windBomb.isInArea = false;
            //Debug.Log("out");
        }
    }
}
