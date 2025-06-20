using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public GameObject tutorial_screen;
    public InputField inputfield;
    Player_HP player_hp_script;

    public GameObject end_door;
    public GameObject ending_human;

    public GameObject[] clear_Text;

    private void Awake()
    {
        player_hp_script = GameObject.Find("Player").GetComponent<Player_HP>();
        if (PlayerData_Manager.Player_Name == "PlayerName_")
        {
            tutorial_screen.SetActive(true);
        }
        else
        {
            enter_name();
        }

        int clear_count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (PlayerData_Manager.dungeon_clear[i] == true)
            {
                clear_Text[i].SetActive(true);
                clear_count++;
            }
            print(PlayerData_Manager.dungeon_clear[i]);
        }

        if (clear_count >= 4)
        {
            Game_clear_openEnding();
        }
    }
    public void Game_clear_openEnding()
    {
        end_door.SetActive(true);
        ending_human.SetActive(false);
    }
    public void enter_name()
    {
        PlayerData_Manager.Player_Name = inputfield.text;
        player_hp_script.playername_text();
        tutorial_screen.SetActive(false);
    }
}
