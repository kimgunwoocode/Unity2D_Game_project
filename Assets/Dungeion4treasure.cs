using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeion4treasure : MonoBehaviour
{
    public treasure treasure;

    private void Awake()
    {
        if (PlayerData_Manager.dungeon_clear[3] == true)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (treasure.interacting == true)
        {
            PlayerData_Manager.dungeon_clear[3] = true;
            this.enabled = false;
        }
    }
}
