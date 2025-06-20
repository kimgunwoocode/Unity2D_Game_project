using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class humantreasureinteraction : MonoBehaviour
{
    [TextArea]
    public string Text_contents;//��µ� ����
    [TextArea]
    public string success_text;
    [TextArea]
    public string failure_text;

    public Text text_;

    public InputField inputfield_;
    public GameObject inputScreen;
    public GameObject screen;
    public void closescreen() { screen.SetActive(false); }

    public GameObject interaction_text;
    public BoxCollider2D collider_;
    public GameObject treasure_obj;


    public static bool trigger_ = true;


    [HideInInspector] public bool interacting = false; //��ȣ�ۿ� ������ Ȯ���ϴ� ����


    private void Start()
    {
        if (trigger_ == false)
        {
            collider_.enabled = false;
            interaction_text.SetActive(true);
        }
    }

    IEnumerator WriteText()
    {
        text_.text = "";
        screen.SetActive(true);

        foreach (char item in Text_contents)//�� ���ھ� ��µǴ� ȿ��
        {
            text_.text += item;
            yield return new WaitForSeconds(0.06f);
        }

        while (true)
        {
            /*
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    inputScreen.SetActive(true);
                    break;
                }
            }
            else */if (Input.GetMouseButtonDown(0))
            {
                inputScreen.SetActive(true);
                break;
            }

            yield return null;
        }

        yield break;
    }
    public void check_question_fun()
    {
        StartCoroutine(check_question());
    }
    public IEnumerator check_question()
    {
        inputScreen.SetActive(false);
        text_.text = "";
        if (inputfield_.text == "0621" || inputfield_.text == "1019" || inputfield_.text == "0920" || inputfield_.text == "0523")
        {
            foreach (char item in success_text)//�� ���ھ� ��µǴ� ȿ��
            {
                text_.text += item;
                yield return new WaitForSeconds(0.06f);
            }

            treasure_obj.SetActive(true);
            collider_.enabled = false;
            interaction_text.SetActive(true);
            trigger_ = false;
        }
        else
        {
            foreach (char item in failure_text)//�� ���ھ� ��µǴ� ȿ��
            {
                text_.text += item;
                yield return new WaitForSeconds(0.06f);
            }

        }

        while (true)
        {
            /*
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    interacting = false;
                    screen.SetActive(false);
                    break;
                }
            }
            else */if (Input.GetMouseButtonDown(0))
            {
                interacting = false;
                screen.SetActive(false);
                break;
            }
            yield return null;
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
