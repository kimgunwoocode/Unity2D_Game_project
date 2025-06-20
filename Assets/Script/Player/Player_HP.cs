using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{
    [Header("Player UI")]
    public Text Player_name;//플레이어 이름 텍스트
    public Text Player_LV;//플레이어 레벨 텍스트
    public Slider HP_bar;//플레이어 체력바 UI
    public Text HPText;//플레이어 체력 텍스트
    public GameObject gameover_Screen;

    public void playername_text()
    {
        Player_name.text = PlayerData_Manager.Player_Name;
    }

    private void Update()
    {
        ShowPlayerHP();

        if (PlayerData_Manager.Player_PresentHP <= 0)
        {
            gameover_Screen.SetActive(true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            PlayerData_Manager.Player_Die = true;
        }
    }

    public void ShowPlayerHP()//플레이어의 현재 체력 UI에 나타내는 함수
    {
        HPText.text = PlayerData_Manager.Player_PresentHP.ToString() + "/" + PlayerData_Manager.Player_MaxHP.ToString();
        HP_bar.value = (float)PlayerData_Manager.Player_PresentHP / PlayerData_Manager.Player_MaxHP;
    }
}
