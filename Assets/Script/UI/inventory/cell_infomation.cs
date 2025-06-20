using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cell_infomation : MonoBehaviour
{
    public int id;
    public Image sprite_this;
    inventoryScreen_ inventory_;
    private void Start()
    {
        inventory_ = GameObject.Find("Inventory Screen").GetComponent<inventoryScreen_>();
    }

    public void item_infomation()
    {
        inventory_.Open_ItemInfomation(sprite_this.sprite, itemData.item[id][0], itemData.item[id][1]);
    }
}
