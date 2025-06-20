using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAll : MonoBehaviour
{
    public GameObject UI;
    public GameObject player;

    private void Start()
    {
        UI = GameObject.Find("UI");
        player = GameObject.Find("Player");

        UI.SetActive(false);
        player.SetActive(false);
    }
}
