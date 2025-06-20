using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManager : MonoBehaviour
{
    public EnemyData enemyData;
    public Animator animator;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;

    public GameObject bullet;
    public GameObject player;
    public GameObject sponer;
    public AudioClip attack1Audio;
    public AudioClip attack2Audio;

    bool stop;
    bool cool;

    Vector3 playerPos;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sponer = GameObject.Find("SponeEnemy");
        stop = false;
        cool = false;
    }

    void Update()
    {
        float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);//플레이어와의 거리
        if (stop == false)
        {
            if (player.transform.position.x > gameObject.transform.position.x)
            {
                sprite.flipX = false;
            }
            else if (player.transform.position.x < gameObject.transform.position.x)
            {
                sprite.flipX = true;
            }

            rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * enemyData.Enemy_Speed;//플레이어 방향으로 몬스터 이동

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
            animator.SetTrigger("Hit");
            enemyData.Enemy_Hitted = false;
        }   
        
        if (dis > 2)
        {
            stop = false;
        }
        else if (dis <= 2)
        {
            stop = true;
        }
    }

    public void Damage()
    {
        PlayerData_Manager.Player_PresentHP -= enemyData.Enemy_Damage;
    }

    IEnumerator Attack()
    {
        stop = true;
        cool = true;

        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(3f);
        cool = false;

        yield break;
    }

    void Shoot1()
    {
        GameObject wind = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D bulletRigid = wind.GetComponent<Rigidbody2D>();
        SpriteRenderer bulletSprite = wind.GetComponent<SpriteRenderer>();
        AudioSource audio1 = wind.GetComponent<AudioSource>();
        wind.GetComponent<BatAttackManager>().bat = this;
        audio1.clip = attack1Audio;
        audio1.enabled = true;

        playerPos = player.transform.position;

        bulletRigid.velocity = ((Vector2)(playerPos - wind.transform.position).normalized) * enemyData.Enemy_Speed * 1.5f;//플레이어 방향으로 이동

        if (player.transform.position.x < gameObject.transform.position.x)
        {
            bulletSprite.flipX = true;
        }
    }
    void Shoot2()
    {
        GameObject wind = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D bulletRigid = wind.GetComponent<Rigidbody2D>();
        SpriteRenderer bulletSprite = wind.GetComponent<SpriteRenderer>();
        AudioSource audio2 = wind.GetComponent<AudioSource>();
        wind.GetComponent<BatAttackManager>().bat = this;
        audio2.clip = attack2Audio;
        audio2.enabled = true;
        bulletRigid.velocity = ((Vector2)(playerPos - wind.transform.position).normalized) * enemyData.Enemy_Speed * 1.5f;//플레이어 방향으로 이동

        if (player.transform.position.x < gameObject.transform.position.x)
        {
            bulletSprite.flipX = true;
        }
    }

    void Die()
    {
        Wind_DropItem();
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
                    int num = 1;
                    itemData.AddItem(num);
                    break;
                }
            case 2:
                {
                    int num = 5;
                    itemData.AddItem(num);
                    break;
                }
            case 3:
                {
                    int num = 9;
                    itemData.AddItem(num);
                    break;
                }
        }
    }
}
