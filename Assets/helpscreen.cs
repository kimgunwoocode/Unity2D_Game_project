using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpscreen : MonoBehaviour
{
    public void close___()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            close___();
        }
    }
}
