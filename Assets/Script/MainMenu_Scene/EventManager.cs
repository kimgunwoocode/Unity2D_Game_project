using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public PlayerData_Manager playerData;

    public GameObject HelpScreen;
    public GameObject QuitScreen;
    public RectTransform Quit_rect;

    private bool HelpScreenOpened = false;
    private bool QuitScreenOpened = false;

    private void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerData_Manager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (HelpScreenOpened)
            {
                exit_Help();
            }
            else if (QuitScreenOpened == false)
            {
                quit_button();
            }
        }
    }

    public void Start_button()
    {
        if (DontDestroy_Object.Second_DontDestroy == false)
            SceneManager.LoadScene("DontDestroyObject_Scene");
        else if (DontDestroy_Object.Second_DontDestroy == true)
            SceneManager.LoadScene(DontDestroy_Object.last_player_position);
    }

    public void Help_button()
    {
        HelpScreen.SetActive(true);
        HelpScreenOpened = true;
    }

    public void exit_Help()
    {
        HelpScreen.SetActive(false);
        HelpScreenOpened = false;
    }

    public void quit_button()
    {
        QuitScreen.SetActive(true);
        QuitScreenOpened = true;
        StartCoroutine("Open_QuitScreen");
    }

    IEnumerator Open_QuitScreen()
    {
        float scale_y = 0;

        while (true)
        {
            if (scale_y > 1)
            {
                yield break;
            }

            Quit_rect.localScale = new Vector2(1, scale_y);
            scale_y += 5 * Time.deltaTime;

            yield return null;
        }
    }

    public void Quit()
    {
        playerData.Save_Data();
        Application.Quit();
    }

    public void cancel_Quit()
    {
        StartCoroutine("Close_QuitScreen");
    }

    IEnumerator Close_QuitScreen()
    {
        float scale_y2 = 1;

        while (true)
        {
            Quit_rect.localScale = new Vector2(1, scale_y2);
            scale_y2 -= 5 * Time.deltaTime;

            if (scale_y2 <= 0)
            {
                QuitScreen.SetActive(false);
                QuitScreenOpened = false;
                yield break;
            }

            yield return null;
        }
    }
}
