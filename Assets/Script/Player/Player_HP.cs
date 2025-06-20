using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{
    [Header("Player UI")]
    public Text Player_name;//�÷��̾� �̸� �ؽ�Ʈ
    public Text Player_LV;//�÷��̾� ���� �ؽ�Ʈ
    public Slider HP_bar;//�÷��̾� ü�¹� UI
    public Text HPText;//�÷��̾� ü�� �ؽ�Ʈ
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

    public void ShowPlayerHP()//�÷��̾��� ���� ü�� UI�� ��Ÿ���� �Լ�
    {
        HPText.text = PlayerData_Manager.Player_PresentHP.ToString() + "/" + PlayerData_Manager.Player_MaxHP.ToString();
        HP_bar.value = (float)PlayerData_Manager.Player_PresentHP / PlayerData_Manager.Player_MaxHP;
    }
}
