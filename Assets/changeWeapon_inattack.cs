using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeWeapon_inattack : MonoBehaviour
{
    public GameObject[] sword;
    public GameObject[] bow;

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Stage1_Water" || scene.name == "Boss_Water")
        {
            sword[0].SetActive(true);
            sword[1].SetActive(false);
            sword[2].SetActive(false);
            sword[3].SetActive(false);
            sword[4].SetActive(false);
            bow[0].SetActive(true);
            bow[1].SetActive(false);
            bow[2].SetActive(false);
            bow[3].SetActive(false);
            bow[4].SetActive(false);
        }
        if (scene.name == "Stage1_Wind" || scene.name == "Boss_Wind")
        {
            sword[0].SetActive(false);
            sword[1].SetActive(true);
            sword[2].SetActive(false);
            sword[3].SetActive(false);
            sword[4].SetActive(false);
            bow[0].SetActive(false);
            bow[1].SetActive(true);
            bow[2].SetActive(false);
            bow[3].SetActive(false);
            bow[4].SetActive(false);
        }
        if (scene.name == "FireDungeon")
        {
            sword[0].SetActive(false);
            sword[1].SetActive(false);
            sword[2].SetActive(true);
            sword[3].SetActive(false);
            sword[4].SetActive(false);
            bow[0].SetActive(false);
            bow[1].SetActive(false);
            bow[2].SetActive(true);
            bow[3].SetActive(false);
            bow[4].SetActive(false);
        }
        if (scene.name == "Dungeon4")
        {
            sword[0].SetActive(false);
            sword[1].SetActive(false);
            sword[2].SetActive(false);
            sword[3].SetActive(true);
            sword[4].SetActive(false);
            bow[0].SetActive(false);
            bow[1].SetActive(false);
            bow[2].SetActive(false);
            bow[3].SetActive(true);
            bow[4].SetActive(false);
        }
        else
        {

        }
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
