using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDB
{
    public string Player_Name_DATA;//�÷��̾� �̸�
    public string Player_Name_data;//�÷��̾� �̸�

    //public int Player_Level_DATA;//�÷��̾� ����

    //public bool[] Item_index_DATA;//������ ����, �ع� ����

    public List<int> Inventory_keys_DATA;
    public List<int> Inventory_values_DATA;
    public List<int> Inventory_keys;
    public List<int> Inventory_values;

    //public Dictionary<int, int> Inventory_DATA = new Dictionary<int, int>();//���� �κ��丮 �ȿ� �ִ� ������ ����, [������ ������ȣ(Ű��), ������ ����] ������ ����Ʈ
    //public int Sword_DATA, Bow_DATA, Artifact_DATA;

    public bool[] dugeon_clear_DATA;

    public bool treasure_enable_DATA;
    public int count_rock_DATA;
    public bool trigger_human_DATA;
}
