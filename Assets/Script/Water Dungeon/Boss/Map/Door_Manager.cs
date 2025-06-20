using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_Manager : MonoBehaviour
{
    public string EnterScene_Name;
    public Vector2 Next_PlayerPosition_;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Button1Manager.Button1_Type = 2;
            keyboard.Button1_Type = 2;
            StartCoroutine("OntriggerStay_");
        }
    }

    IEnumerator OntriggerStay_()
    {
        while(true)
        {
            if (Button1Manager.Button1_Type == 3 || keyboard.Button1_Type == 3)
            {
                Button1Manager.Button1_Type = 1;
                keyboard.Button1_Type = 1;
                SceneManager.LoadScene(EnterScene_Name);
                PlayerPosition_SceneLoding_Manager.PlayerPosition_NextScene = Next_PlayerPosition_;
                yield break;
            }
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Button1Manager.Button1_Type = 1;
            keyboard.Button1_Type = 1;
            StopCoroutine("OntriggerStay_");
        }
    }
}
