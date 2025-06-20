using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    public Interaction_Text interaction_;
    public void heal_fun()
    {
        PlayerData_Manager.Player_PresentHP = PlayerData_Manager.Player_MaxHP;
    }
    private void Update()
    {
        if (interaction_.interacting == true && PlayerData_Manager.Player_PresentHP < PlayerData_Manager.Player_MaxHP)
        {
            heal_fun();
        }
    }
}
