using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDB
{
    public string Player_Name_DATA;//플레이어 이름
    public string Player_Name_data;//플레이어 이름

    //public int Player_Level_DATA;//플레이어 레벨

    //public bool[] Item_index_DATA;//아이템 도감, 해방 여부

    public List<int> Inventory_keys_DATA;
    public List<int> Inventory_values_DATA;
    public List<int> Inventory_keys;
    public List<int> Inventory_values;

    //public Dictionary<int, int> Inventory_DATA = new Dictionary<int, int>();//현재 인벤토리 안에 있는 아이템 저장, [아이템 고유번호(키값), 아이템 개수] 형태의 리스트
    //public int Sword_DATA, Bow_DATA, Artifact_DATA;

    public bool[] dugeon_clear_DATA;

    public bool treasure_enable_DATA;
    public int count_rock_DATA;
    public bool trigger_human_DATA;
}
