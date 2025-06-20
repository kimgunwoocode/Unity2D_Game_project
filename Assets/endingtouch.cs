using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endingtouch : MonoBehaviour
{
    public Text text1;
    public Text text2;

    public GameObject text_obj1;
    public GameObject text_obj2;

    private int qhanfrotn = 0;
    private void Start()
    {
        qhanfrotn = PlayerData_Manager.Inventory[13];
        text__();
    }
    public void text__()
    {
        text1.text = "È¹µæÇÑ º¸¹° : " + qhanfrotn.ToString();
        if (qhanfrotn >= 7)
        {
            text_obj1.SetActive(false);
            text_obj2.SetActive(true);
        }
        else
        {
            text_obj2.SetActive(false);
            text_obj1.SetActive(true);

            text2.text = "³²Àº °³¼ö : " + (7 - qhanfrotn).ToString();
        }
    }
    public void return_()
    {
        SceneManager.LoadScene("DontDestroyObject_Scene");
    }
}
