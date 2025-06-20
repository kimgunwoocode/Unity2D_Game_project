using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class rockinteraction : MonoBehaviour
{
    public AudioSource audio;
    [TextArea]
    public string Text_contents1;//��µ� ����
    [TextArea]
    public string Text_contents2;//��µ� ����
    [TextArea]
    public string Text_contents3;//��µ� ����
    [TextArea]
    public string Text_contents4;//��µ� ����
    [TextArea]
    public string Text_contents5;//��µ� ����
    [TextArea]
    public string Text_contents_final;//��µ� ����

    public static int count = 1;

    public GameObject text_obj;//Ȱ��, ��Ȱ�� �ϱ����� ������Ʈ�� �޾ƿ���
    public Text text_;
    public GameObject rock_object;
    public SpriteRenderer sprite_;
    public BoxCollider2D collider_;
    public GameObject treasure_obj;


    [HideInInspector] public bool interacting = false; //��ȣ�ۿ� ������ Ȯ���ϴ� ����

    private void Start()
    {
        text_obj.SetActive(false);
        if (count >= 6)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WriteText()
    {
        string Text_contents = "error";
        switch (count)
        {
            case 1:
                {
                    audio.Play();
                    Text_contents = Text_contents1;
                    break;
                }
            case 2:
                {
                    audio.Play();
                    Text_contents = Text_contents2;
                    gameObject.transform.Rotate(0, 0, 10);
                    break;
                }
            case 3:
                {
                    Text_contents = Text_contents3;
                    break;
                }
            case 4:
                {
                    audio.Play();
                    Text_contents = Text_contents4;
                    gameObject.transform.Rotate(0, 0, -15);
                    break;
                }
            case 5:
                {
                    Text_contents = Text_contents5;
                    break;
                }
        }

        count++;//���� ���� �Ѿ��


        Vector3 pos = gameObject.transform.position;
        text_obj.transform.position = new Vector3(pos.x, pos.y + 1.5f, pos.z - 0.1f);
        text_.text = "";
        text_obj.SetActive(true);
        foreach (char item in Text_contents)//�� ���ھ� ��µǴ� ȿ��
        {
            text_.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(Text_contents.Length / 12);//�ؽ�Ʈ ���̿� ����Ͽ� ��ٸ� �Ŀ�
        text_obj.SetActive(false);//�ؽ�Ʈ ��Ȱ��ȭ
        interacting = false;//��ȣ�ۿ� ���� ���� (��ȣ�ۿ� ��� ����)
        collider_.enabled = false;
        yield return null;
        collider_.enabled = true;


        if (count == 6)
        {
            Destroy(sprite_);
            Destroy(collider_);

            Vector3 pos_ = gameObject.transform.position;
            text_obj.transform.position = new Vector3(pos_.x, pos_.y + 1.5f, pos_.z - 0.1f);
            text_.text = "";
            text_obj.SetActive(true);
            foreach (char item in Text_contents_final)//�� ���ھ� ��µǴ� ȿ��
            {
                text_.text += item;
                yield return new WaitForSeconds(0.1f);
            }
            treasure_obj.SetActive(true);
            yield return new WaitForSeconds(Text_contents_final.Length / 16);//�ؽ�Ʈ ���̿� ����Ͽ� ��ٸ� �Ŀ�
            text_.text = "";
            Destroy(gameObject);
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
