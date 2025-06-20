using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [TextArea]
    public string[] gameoverText;

    public Text text_;
    public GameObject respawn_text;
    public string respawn_name;//플레이어 부활 장소

    public IEnumerator print_text_()
    {
        int rand = Random.Range(0, gameoverText.Length);

        foreach (char item in gameoverText[rand])//한 글자씩 출력되는 효과
        {
            text_.text += item;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);
        respawn_text.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                respawn_text.SetActive(false);
                text_.text = "";
                gameObject.SetActive(false);
                PlayerData_Manager.Player_Die = false;
                PlayerData_Manager.Player_PresentHP = PlayerData_Manager.Player_MaxHP;
                SceneManager.LoadScene(respawn_name);
                yield break;

            }
        }
    }
}
