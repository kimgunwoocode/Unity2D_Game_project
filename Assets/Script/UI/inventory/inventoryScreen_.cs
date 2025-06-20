using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class inventoryScreen_ : MonoBehaviour
{
    [Header("아이템 프리팹 (고유번호id 순서대로!)")]
    public GameObject[] item_prefab;//아이템 프리팹 저장 (이미지 보이게하기 위함), 꼭! 아이템 고유번호 순서로 저장하기!
    [Header("아이템 설명창 요소")]
    public GameObject InfomationScreen;
    public Image icon_ob;
    public Text name_ob;
    public Text description_ob;
    [Header("")]
    public GameObject content;
    GameObject[] sell = new GameObject[30];//인벤토리 창 한 칸씩 저장
    RectTransform[] sell_pos = new RectTransform[30];
    Text[] sell_count = new Text[30];

    private void Awake()
    {
        for (int i = 0; i < 30; i++)
        {
            sell[i] = content.transform.GetChild(i).gameObject;
            sell_count[i] = sell[i].transform.GetComponentInChildren<Text>();
            sell_pos[i] = sell[i].GetComponent<RectTransform>();
        }
    }
    
    private void OnEnable()
    {
        int i = 0;
        foreach (KeyValuePair<int, int> inven in PlayerData_Manager.Inventory)
        {
            GameObject item__ = Instantiate(item_prefab[inven.Key]);
            item__.transform.SetParent(sell[i].transform);
            item__.GetComponent<RectTransform>().position = sell_pos[i].position;
            sell_count[i].text = inven.Value.ToString();
            i++;
        }
    }

    private void OnDisable()
    {
        Close_ItemInfomation();
    }

    public void Open_ItemInfomation(Sprite icon,string name, string description)
    {
        InfomationScreen.SetActive(true);
        icon_ob.sprite = icon;
        name_ob.text = name;
        description_ob.text = description;
    }

    public void Close_ItemInfomation()
    {
        InfomationScreen.SetActive(false);
    }
}