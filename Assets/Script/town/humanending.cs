using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class humanending : MonoBehaviour
{
    public GameStart gameStart;

    [TextArea]
    public string Text_contents1;//출력될 내용
    [TextArea]
    public string Text_contents2;
    [TextArea]
    public string Text_contents3;

    public Text text_;

    public GameObject screen;
    public void closescreen() { screen.SetActive(false); }


    [HideInInspector] public bool interacting = false; //상호작용 중인지 확인하는 변수

    private void Update()
    {
        if (interacting)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.T))
                {
                    if (Input.GetKey(KeyCode.O))
                    {
                        if (Input.GetKeyDown(KeyCode.M))
                        {
                            closescreen();
                            gameStart.Game_clear_openEnding();
                        }
                    }
                }
            }
        }
    }
    IEnumerator WriteText()
    {
        text_.text = "";
        screen.SetActive(true);

        string Text_contents = "";

        int num = 1;
        for (int i = 0; i < 3; i++)
        {
            text_.text = "";
            switch (num)
            {
                case 1:
                    {
                        Text_contents = Text_contents1;
                        break;
                    }
                case 2:
                    {
                        Text_contents = Text_contents2;
                        break;
                    }
                case 3:
                    {
                        Text_contents = Text_contents3;
                        break;
                    }
                case 4:
                    {
                        closescreen();
                        yield break;
                    }
            }

            foreach (char item in Text_contents)//한 글자씩 출력되는 효과
            {
                text_.text += item;
                yield return new WaitForSeconds(0.05f);
            }

            while (true)
            {
                /*
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        num++;
                        if (num == 4)
                        {
                            closescreen();
                            yield break;
                        }
                        break;
                    }
                }
                else */if(Input.GetMouseButtonDown(0))
                {
                    num++;
                    if (num == 4)
                    {
                        interacting = false;
                        closescreen();
                        yield break;
                    }
                    break;
                }
                yield return null;
            }
        }
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && interacting == false)
        {
            Button1Manager.Button1_Type = 2;//상호작용 버튼으로 변경
            keyboard.Button1_Type = 2;
            StartCoroutine("OntriggerStay_");
        }
    }

    IEnumerator OntriggerStay_()//충돌 상태가 지속되고 있을 때
    {
        while (true)
        {
            if (Button1Manager.Button1_Type == 3 || keyboard.Button1_Type == 3)//상호작용 버튼 클릭 시
            {
                Button1Manager.Button1_Type = 1;//공격 버튼으로 전환
                keyboard.Button1_Type = 1;
                interacting = true;//상호작용 중 활성화 (상호작용 작동할 때는 상호작용 시작 불가능)
                StartCoroutine("WriteText");//글씨쓰기 함수 호출
                yield break;//코루틴 종료
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
