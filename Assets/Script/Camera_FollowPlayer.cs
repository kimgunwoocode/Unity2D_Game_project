using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_FollowPlayer : MonoBehaviour
{
    GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
