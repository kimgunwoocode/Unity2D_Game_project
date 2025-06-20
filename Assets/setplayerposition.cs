using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setplayerposition : MonoBehaviour
{
    public Vector2 position;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(position.x, position.y, -1f);
    }
}
