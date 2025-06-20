using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class inventoryScreen_ : MonoBehaviour
{
    [Header("������ ������ (������ȣid �������!)")]
    public GameObject[] item_prefab;//������ ������ ���� (�̹��� ���̰��ϱ� ����), ��! ������ ������ȣ ������ �����ϱ�!
    [Header("������ ����â ���")]
    public GameObject InfomationScreen;
    public Image icon_ob;
    public Text name_ob;
    public Text description_ob;
    [Header("")]
    public GameObject content;
    GameObject[] sell = new GameObject[30];//�κ��丮 â �� ĭ�� ����
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