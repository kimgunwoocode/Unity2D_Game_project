using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static GameObject[] Trigger_obj;
    public static bool[] Trigger_enabled;

    private void Start()
    {
        int n = 0;
        foreach (bool trigger_ in Trigger_enabled)
        {
            if (trigger_ == true)
            {
                Trigger_obj[n].SetActive(true);
            }
            else if (trigger_ == false)
            {
                Trigger_obj[n].SetActive(false);
            }
            n++;
        }
    }
}
