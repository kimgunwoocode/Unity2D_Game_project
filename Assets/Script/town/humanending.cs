using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class humanending : MonoBehaviour
{
    public GameStart gameStart;

    [TextArea]
    public string Text_contents1;//��µ� ����
    [TextArea]
    public string Text_contents2;
    [TextArea]
    public string Text_contents3;

    public Text text_;

    public GameObject screen;
    public void closescreen() { screen.SetActive(false); }


    [HideInInspector] public bool interacting = false; //��ȣ�ۿ� ������ Ȯ���ϴ� ����

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

            foreach (char item in Text_contents)//�� ���ھ� ��µǴ� ȿ��
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
            Button1Manager.Button1_Type = 2;//��ȣ�ۿ� ��ư���� ����
            keyboard.Button1_Type = 2;
            StartCoroutine("OntriggerStay_");
        }
    }

    IEnumerator OntriggerStay_()//�浹 ���°� ���ӵǰ� ���� ��
    {
        while (true)
        {
            if (Button1Manager.Button1_Type == 3 || keyboard.Button1_Type == 3)//��ȣ�ۿ� ��ư Ŭ�� ��
            {
                Button1Manager.Button1_Type = 1;//���� ��ư���� ��ȯ
                keyboard.Button1_Type = 1;
                interacting = true;//��ȣ�ۿ� �� Ȱ��ȭ (��ȣ�ۿ� �۵��� ���� ��ȣ�ۿ� ���� �Ұ���)
                StartCoroutine("WriteText");//�۾����� �Լ� ȣ��
                yield break;//�ڷ�ƾ ����
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
