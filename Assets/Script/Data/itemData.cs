using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemData : MonoBehaviour
{
    public static Dictionary<int, string[]> item = new Dictionary<int, string[]>()
    {
        { 1, new string[2] {"������1", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 2, new string[2] {"������2", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 3, new string[2] {"������3", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 4, new string[2] {"������4", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 5, new string[2] {"������5", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 6, new string[2] {"������6", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 7, new string[2] {"������7", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 8, new string[2] {"������8", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 9, new string[2] {"������9", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 10, new string[2] {"������10", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 11, new string[2] {"������11", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 12, new string[2] {"������12", "����� �������̴�. �����ڰ� ������ ���� �������̴�" } },
        { 13, new string[2] {"������ ����", "����� ����� ��ٷ��� ������ �����̴�. �� ������ â���ſ��� �����ָ� ����� ������ ��������?!!"}},
        { 14, new string[2] {"�׳� ����", "����� �����̴�. �ƹ� ȿ�� ����."} }
    }
    ;

    public static void AddItem(int id)
    {
        if (PlayerData_Manager.Inventory.ContainsKey(id) || PlayerData_Manager.Inventory == null)
        {
            PlayerData_Manager.Inventory[id]++;
        }
        else
        {
            PlayerData_Manager.Inventory.Add(id, 1);
        }
    }
}
