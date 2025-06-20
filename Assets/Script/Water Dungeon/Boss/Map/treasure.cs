
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class treasure : MonoBehaviour
{
    [TextArea]
    public string Text_contents;//출력될 내용

    public GameObject text_obj;//활성, 비활성 하기위해 오브젝트로 받아오기
    public Text text_;
    public SpriteRenderer sprite_;
    public BoxCollider2D boxCollider_;

    [HideInInspector] public bool interacting = false; //상호작용 중인지 확인하는 변수



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
        foreach (char item in Text_contents)//한 글자씩 출력되는 효과
        {
            text_.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(Text_contents.Length / 10);//텍스트 길이에 비례하여 기다린 후에
        text_obj.SetActive(false);//텍스트 비활성화
        interacting = false;//상호작용 중이 끝남 (상호작용 사용 가능)
        Destroy(gameObject);
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
