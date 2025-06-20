using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadAttack2Manager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WindBoss_Manager.isInArea = true;
        Debug.Log("in");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        WindBoss_Manager.isInArea = false;
        Debug.Log("out");
    }
}
