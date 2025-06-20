using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackManager : MonoBehaviour
{
    [HideInInspector]
    public BatManager bat;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bat.Damage();
        }
    }
}
