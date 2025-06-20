using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_manager : MonoBehaviour
{
    public EnemyData enemyData;
    public Animator animator;
    public Rigidbody2D rigid;
    public AudioSource audio;

    public GameObject player;
    public GameObject sponer;

    public GameObject home;
    public GameObject Attack_obj;

    bool stop; public void stop_flase() { stop = false; }
    bool cool;

    Vector3 playerPos;

    void Start()
    {
        player = GameObject.Find("Player");
        sponer = GameObject.Find("SponeEnemy");
        home.SetActive(true);
        home.transform.SetParent(null);
        stop = false;
        cool = false;
        animator.SetBool("Move", false);
    }

    void Update()
    {
        float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//플레이어와의 거리
        if (stop == false)
        {
            if (player.transform.position.x > gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(4, 4, 1);
            }
            else if (player.transform.position.x < gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(-4, 4, 1);
            }

            if (dis <= 8)
            {
                animator.SetBool("Move", true);
                rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemyData.Enemy_Speed;//플레이어 방향으로 몬스터 이동
            }
            else
            {
                rigid.velocity = ((Vector2)(home.transform.position - gameObject.transform.position).normalized) * enemyData.Enemy_Speed;//원래 위치로 돌아가기 방향으로 몬스터 이동
                
                if (Vector2.Distance((Vector2)home.transform.position, (Vector2)gameObject.transform.position) <= 0.01f)//다시 돌아오면 체력 회복
                {
                    enemyData.Enemy_PresentHP = enemyData.Enemy_MaxHP;
                    animator.SetBool("Move", false);
                }
            }
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }

        if (dis <= 2 && cool == false)
        {
            StartCoroutine(Attack());
        }

        if (enemyData.Enemy_Die == true)//죽을 시 Die명령어
        {
            stop = true;
            animator.SetTrigger("Die");
        }

        if (enemyData.Enemy_Hitted == true)
        {
            stop = true;
            animator.SetTrigger("Hitted");
            enemyData.Enemy_Hitted = false;
        }
    }


    public IEnumerator Attack()
    {
        stop = true;
        cool = true;
        animator.SetTrigger("Attack");
        Attack_obj.SetActive(true);
        yield return new WaitForSeconds(0.11f);
        stop = false;
        Attack_obj.SetActive(false);
        yield return new WaitForSeconds(3f);
        cool = false;
        yield break;
    }


    void Die()
    {
        Wind_DropItem();
        Destroy(home);
        Destroy(gameObject);
        sponer.GetComponent<SponerManager>().deadEnemy += 1;
    }


    void Wind_DropItem()
    {
        int type = Random.Range(1, 4);


        switch (type)
        {
            case 1:
                {
                    int num = 4;
                    itemData.AddItem(num);
                    break;
                }
            case 2:
                {
                    int num = 8;
                    itemData.AddItem(num);
                    break;
                }
            case 3:
                {
                    int num = 12;
                    itemData.AddItem(num);
                    break;
                }
        }
    }

    public void AttackSound()
    {
        audio.enabled = false;
        audio.enabled = true;
    }
}
