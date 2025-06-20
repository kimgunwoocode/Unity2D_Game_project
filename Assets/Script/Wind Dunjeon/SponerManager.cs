using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponerManager : MonoBehaviour
{
    public int deadEnemy;
    public GameObject[] door;
    public GameObject treasure;

    public GameObject bat;
    public GameObject windBomb;

    bool enough = false;
    bool enough2 = false;

    void Start()
    {
        GameObject.FindWithTag("Player").transform.position = new Vector3(-19, 0, -1);
        deadEnemy = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Player")
        {
            if (deadEnemy == 0)
            {
                transform.position = new Vector3(transform.position.x + 23, 0, 0);

                Instantiate(bat, new Vector3(4, 4, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(bat, new Vector3(4, -4, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(bat, new Vector3(6, 2, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(bat, new Vector3(6, -2, -1), new Quaternion(0, 0, 0, 0));

                door[0].SetActive(true);
            }
            else if (deadEnemy == 4)
            {
                transform.position = new Vector3(transform.position.x + 23, 0, 0);

                Instantiate(windBomb, new Vector3(27, 4, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(windBomb, new Vector3(27, -4, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(windBomb, new Vector3(29, 2, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(windBomb, new Vector3(29, -2, -1), new Quaternion(0, 0, 0, 0));

                door[2].SetActive(true);
            }
            else if (deadEnemy == 12)
            {
                transform.position = new Vector3(transform.position.x + 23, 0, 0);

                Instantiate(bat, new Vector3(49, 1, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(bat, new Vector3(49, -1, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(windBomb, new Vector3(51, 3, -1), new Quaternion(0, 0, 0, 0));
                Instantiate(windBomb, new Vector3(51, -3, -1), new Quaternion(0, 0, 0, 0));

                door[4].SetActive(true);
            }
        }
    }

    void Update()
    {
        if (deadEnemy == 4)
        {
            door[1].SetActive(false);
        }
        else if (deadEnemy == 8 && enough == false)
        {
            Instantiate(windBomb, new Vector3(19, 4, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(windBomb, new Vector3(19, -4, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(windBomb, new Vector3(17, 2, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(windBomb, new Vector3(17, -2, -1), new Quaternion(0, 0, 0, 0));

            enough = true;
        }
        else if (deadEnemy == 12)
        {
            door[3].SetActive(false);
        }
        else if (deadEnemy == 16 && enough2 == false)
        {
            Instantiate(windBomb, new Vector3(49, 4, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(windBomb, new Vector3(49, -4, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(windBomb, new Vector3(40, 0, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(bat, new Vector3(51, 0, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(bat, new Vector3(41, 2, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(bat, new Vector3(41, -2, -1), new Quaternion(0, 0, 0, 0));

            enough2 = true;
        }
        else if (deadEnemy == 22)
        {
            door[5].SetActive(true);
            if (treasure != null)
               treasure.SetActive(true);
        }
    }
}
