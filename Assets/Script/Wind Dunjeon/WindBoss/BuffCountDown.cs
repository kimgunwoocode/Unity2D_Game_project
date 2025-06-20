using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffCountDown : MonoBehaviour
{
    public bool isBuffed;

    void Start()
    {
        isBuffed = false;
    }

    void Update()
    {
        if (isBuffed == false)
        {
            PlayerData_Manager.moveSpeed = PlayerData_Manager.moveSpeed * 1.5f;
            isBuffed = true;
        }
    }
}
