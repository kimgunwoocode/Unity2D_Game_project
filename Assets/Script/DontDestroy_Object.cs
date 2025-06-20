using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy_Object : MonoBehaviour
{
    public string Next_SceneName;
    public GameObject Player_;
    public GameObject UI_;
    public GameObject GameManager;

    public static bool Second_DontDestroy = false;
    public static string last_player_position = "Town";//���������� �÷��̾ �־��� �� �����ϱ�

    private void Awake()
    {
        DontDestroyOnLoad(Player_);
        DontDestroyOnLoad(UI_);

        SceneManager.LoadScene(Next_SceneName);
    }
}
