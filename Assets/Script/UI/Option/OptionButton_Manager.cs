using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionButton_Manager : MonoBehaviour
{
    //필요한 UI화면 : 장비 화면 (장비 정보 확인, 장비 교체), 인벤토리 화면 (보유 아이템 확인, 아이템 정보 확인, 아이템 사용), 메인화면으로 돌아가기, 던전 포기, 

    public GameObject InventoryScreen;
    [HideInInspector] public bool Opende_InventoryScreen = false;
    public GameObject OptionScreen;
    [HideInInspector] public bool Opende_OptionScreen = false;

    public GameObject helpScreen;

    public void Open_InventoryScreen()
    {
        if (Opende_InventoryScreen)
        {
            InventoryScreen.SetActive(false);
            Opende_InventoryScreen = false;
        }
        else if (!Opende_InventoryScreen)
        {
            if (Opende_OptionScreen)
            {
                OptionScreen.SetActive(false);
                Time.timeScale = 1;
                Opende_OptionScreen = false;
            }
            InventoryScreen.SetActive(true);
            Opende_InventoryScreen = true;
        }
    }

    public void Open_OptionScreen()
    { 
        if (Opende_OptionScreen)
        {
            OptionScreen.SetActive(false);
            Time.timeScale = 1;
            Opende_OptionScreen = false;
        }
        else if (!Opende_OptionScreen)
        {
            if (Opende_InventoryScreen)
            {
                InventoryScreen.SetActive(false);
                Time.timeScale = 1;
                Opende_InventoryScreen = false;
            }
            OptionScreen.SetActive(true);
            Time.timeScale = 0;
            Opende_OptionScreen = true;
        }
    }

    //-----------------------------------------------button 인스펙터에 들어갈 함수-------------------------------
    public void Goto_MainMenu()
    {
        Opende_OptionScreen = false;
        OptionScreen.SetActive(false);
        DontDestroy_Object.Second_DontDestroy = true;
        DontDestroy_Object.last_player_position = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Open_soundOption()
    {

    }

    public void Open_HelpScreen()
    {
        helpScreen.SetActive(true);
    }

    public void GoTo_Town()
    {
        Opende_OptionScreen = false;
        OptionScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Town");
    }

    //-------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Opende_InventoryScreen)
            {
                Open_InventoryScreen();
            }
            else
            {
                Open_OptionScreen();
            }
        }
    }
}
