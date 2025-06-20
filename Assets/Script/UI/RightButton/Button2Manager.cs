using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button2Manager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject player;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(0.8627451f, 0.8627451f, 0.8627451f, 0.6470588f);
        //GameObject.Find("skill test").GetComponent<animation_test>().Skill_test_function();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6470588f);
    }
}
