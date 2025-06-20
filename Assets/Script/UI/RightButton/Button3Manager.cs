using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button3Manager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject player;
    public Button1Manager AttackButton;

    public GameObject Sword_Pobj;
    public GameObject Bow_Pobj;

    public ArrowJoystic arrowjoystic;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(0.8627451f, 0.8627451f, 0.8627451f, 0.6470588f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6470588f);

        switch (PlayerData_Manager.Player_WeaponType)//원거리 공격일 때는 근접으로, 근접일 때는 원거리로 무기 전환
        {
            case 1:
                {
                    PlayerData_Manager.Player_WeaponType = 2;
                    Sword_Pobj.SetActive(false);
                    Bow_Pobj.SetActive(true);
                    arrowjoystic.enabled = true;
                    break;
                }
            case 2:
                {
                    PlayerData_Manager.Player_WeaponType = 1;
                    Sword_Pobj.SetActive(true);                    
                    Bow_Pobj.SetActive(false);
                    arrowjoystic.enabled = false;
                    break;
                }
        }
    }
}
