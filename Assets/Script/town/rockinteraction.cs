using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class rockinteraction : MonoBehaviour
{
    public AudioSource audio;
    [TextArea]
    public string Text_contents1;//출력될 내용
    [TextArea]
    public string Text_contents2;//출력될 내용
    [TextArea]
    public string Text_contents3;//출력될 내용
    [TextArea]
    public string Text_contents4;//출력될 내용
    [TextArea]
    public string Text_contents5;//출력될 내용
    [TextArea]
    public string Text_contents_final;//출력될 내용

    public static int count = 1;

    public GameObject text_obj;//활성, 비활성 하기위해 오브젝트로 받아오기
    public Text text_;
    public GameObject rock_object;
    public SpriteRenderer sprite_;
    public BoxCollider2D collider_;
    public GameObject treasure_obj;


    [HideInInspector] public bool interacting = false; //상호작용 중인지 확인하는 변수

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

        count++;//다음 대사로 넘어가기


        Vector3 pos = gameObject.transform.position;
        text_obj.transform.position = new Vector3(pos.x, pos.y + 1.5f, pos.z - 0.1f);
        text_.text = "";
        text_obj.SetActive(true);
        foreach (char item in Text_contents)//한 글자씩 출력되는 효과
        {
            text_.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(Text_contents.Length / 12);//텍스트 길이에 비례하여 기다린 후에
        text_obj.SetActive(false);//텍스트 비활성화
        interacting = false;//상호작용 중이 끝남 (상호작용 사용 가능)
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
            foreach (char item in Text_contents_final)//한 글자씩 출력되는 효과
            {
                text_.text += item;
                yield return new WaitForSeconds(0.1f);
            }
            treasure_obj.SetActive(true);
            yield return new WaitForSeconds(Text_contents_final.Length / 16);//텍스트 길이에 비례하여 기다린 후에
            text_.text = "";
            Destroy(gameObject);
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
