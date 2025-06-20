
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class treasure : MonoBehaviour
{
    [TextArea]
    public string Text_contents;//��µ� ����

    public GameObject text_obj;//Ȱ��, ��Ȱ�� �ϱ����� ������Ʈ�� �޾ƿ���
    public Text text_;
    public SpriteRenderer sprite_;
    public BoxCollider2D boxCollider_;

    [HideInInspector] public bool interacting = false; //��ȣ�ۿ� ������ Ȯ���ϴ� ����



    IEnumerator WriteText()
    {
        Destroy(sprite_);
        Destroy(boxCollider_);

        if (PlayerData_Manager.Inventory.ContainsKey(13) || PlayerData_Manager.Inventory == null)
        {
            PlayerData_Manager.Inventory[13]++;
        }
        else
        {
            PlayerData_Manager.Inventory.Add(13, 1);
        }

        Vector3 pos = gameObject.transform.position;
        text_obj.transform.position = new Vector3(pos.x, pos.y + 1.3f, pos.z - 0.1f);
        text_.text = "";
        text_obj.SetActive(true);
        foreach (char item in Text_contents)//�� ���ھ� ��µǴ� ȿ��
        {
            text_.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(Text_contents.Length / 10);//�ؽ�Ʈ ���̿� ����Ͽ� ��ٸ� �Ŀ�
        text_obj.SetActive(false);//�ؽ�Ʈ ��Ȱ��ȭ
        interacting = false;//��ȣ�ۿ� ���� ���� (��ȣ�ۿ� ��� ����)
        Destroy(gameObject);
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
