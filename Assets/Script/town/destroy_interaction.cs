using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_interaction : MonoBehaviour
{
    public static bool treasure_enable = true;

    void Start()
    {
        if (treasure_enable == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        treasure_enable = false;
    }
}
