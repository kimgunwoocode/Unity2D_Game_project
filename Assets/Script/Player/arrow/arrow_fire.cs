using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_fire : MonoBehaviour
{
    public Vector2 arrow_movetarget;
    public GameObject player;
    public GameObject attack;

    public bool fire = false;

    private void Start()
    {
        StartCoroutine(Start_());
    }
    IEnumerator Start_()
    {
        while (true)
        {
            if (fire == false)
            {
                gameObject.transform.position = attack.transform.position;
                gameObject.transform.rotation = attack.transform.rotation;
            }
            else if (fire == true)
            {
                gameObject.transform.position = attack.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(arrow_movetarget);
                gameObject.GetComponent<Rigidbody2D>().velocity = arrow_movetarget;
                yield break;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fire == true)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyData>().Enemy_PresentHP -= PlayerData_Manager.PlayerDamage;
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
}
