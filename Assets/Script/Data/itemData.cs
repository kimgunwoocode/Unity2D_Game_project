using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemData : MonoBehaviour
{
    public static Dictionary<int, string[]> item = new Dictionary<int, string[]>()
    {
        { 1, new string[2] {"아이템1", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 2, new string[2] {"아이템2", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 3, new string[2] {"아이템3", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 4, new string[2] {"아이템4", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 5, new string[2] {"아이템5", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 6, new string[2] {"아이템6", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 7, new string[2] {"아이템7", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 8, new string[2] {"아이템8", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 9, new string[2] {"아이템9", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 10, new string[2] {"아이템10", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 11, new string[2] {"아이템11", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 12, new string[2] {"아이템12", "평범한 아이템이다. 제작자가 실험삼아 만든 아이템이다" } },
        { 13, new string[2] {"전설의 보물", "용맹한 당신을 기다려온 전설의 보물이다. 이 세계의 창조신에게 보여주면 뜻밖의 선물이 있을지도?!!"}},
        { 14, new string[2] {"그냥 포션", "평범한 포션이다. 아무 효과 없다."} }
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
