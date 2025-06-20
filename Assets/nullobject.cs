using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class nullobject : MonoBehaviour
{
    public GameObject sponer;
    void Start()
    {
        sponer = GameObject.Find("SponeEnemy");
        sponer.GetComponent<SponerManager>().deadEnemy += 1;
        Destroy(gameObject);
    }

}
