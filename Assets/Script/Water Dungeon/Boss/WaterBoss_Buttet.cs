using UnityEngine;

public class WaterBoss_Buttet : MonoBehaviour
{
    public EnemyData data;
    GameObject player;
    Rigidbody2D rigid;

    private void Awake()
    {
        player = GameObject.Find("Player");
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rigid.velocity = ((Vector2)(player.transform.position - gameObject.transform.position).normalized) * 7f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData_Manager.Player_PresentHP -= data.Enemy_Damage;
            collision.gameObject.GetComponent<PlayerSpeed_Manager>().SpeedEffect_cool(0.2f, 1f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
